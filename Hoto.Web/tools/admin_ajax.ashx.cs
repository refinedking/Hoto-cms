using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.SessionState;
using DTcms.Web.UI;
using DTcms.Common;

namespace DTcms.Web.tools
{
    /// <summary>
    /// 管理后台AJAX处理页
    /// </summary>
    public class admin_ajax : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = DTRequest.GetQueryString("action");

            switch (action)
            {
                case "sys_channel_load": //加载频道管理菜单
                    sys_channel_load(context);
                    break;
                case "plugins_nav_load": //加载插件管理菜单
                    plugins_nav_load(context);
                    break;
                case "sys_channel_validate": //验证频道名称是否重复
                    sys_channel_validate(context);
                    break;
                case "sys_urlrewrite_validate": //验证URL重写是否重复
                    sys_urlrewrite_validate(context);
                    break;
                case "validate_username": //验证会员用户名是否重复
                    validate_username(context);
                    break;
            }

        }

        #region 加载频道管理菜单================================
        private void sys_channel_load(HttpContext context)
        {
            StringBuilder strTxt = new StringBuilder();
            BLL.sys_channel bll = new BLL.sys_channel();
            DataTable dt = bll.GetList("").Tables[0];
            strTxt.Append("[");
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                Model.manager admin_info = new ManagePage().GetAdminInfo();
                if (!new BLL.manager_role().Exists(admin_info.role_id, Convert.ToInt32(dr["id"]), DTEnums.ActionEnum.View.ToString()))
                {
                    continue;
                }
                BLL.sys_model bll2 = new BLL.sys_model();
                Model.sys_model model2 = bll2.GetModel(Convert.ToInt32(dr["model_id"]));
                strTxt.Append("{");
                strTxt.Append("\"text\":\"" + dr["title"] + "\",");
                strTxt.Append("\"isexpand\":\"false\",");
                strTxt.Append("\"children\":[");
                if (model2.sys_model_navs != null)
                {
                    int j = 1;
                    foreach (Model.sys_model_nav nav in model2.sys_model_navs)
                    {
                        strTxt.Append("{");
                        strTxt.Append("\"text\":\"" + nav.title + "\",");
                        strTxt.Append("\"url\":\"" + nav.nav_url + "?channel_id=" + dr["id"] + "\""); //此处要优化，加上nav.nav_url网站目录标签替换
                        strTxt.Append("}");
                        if (j < model2.sys_model_navs.Count)
                        {
                            strTxt.Append(",");
                        }
                        j++;
                    }
                }
                strTxt.Append("]");
                strTxt.Append("}");
                strTxt.Append(",");
                i++;
            }
            string newTxt = Utils.DelLastChar(strTxt.ToString(), ",") + "]";
            context.Response.Write(newTxt);
            return;
        }
        #endregion

        #region 加载插件管理菜单================================
        private void plugins_nav_load(HttpContext context)
        {
            BLL.plugin bll = new BLL.plugin();
            DirectoryInfo dirInfo = new DirectoryInfo(Utils.GetMapPath("../plugins/"));
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                Model.plugin aboutInfo = bll.GetInfo(dir.FullName + @"\");
                if (aboutInfo.isload == 1 && File.Exists(dir.FullName + @"\admin\index.aspx"))
                {
                    context.Response.Write("<li><a class=\"l-link\" href=\"javascript:f_addTab('plugin_" + dir.Name
                        + "','" + aboutInfo.name + "','../../plugins/" + dir.Name + "/admin/index.aspx')\">" + aboutInfo.name + "</a></li>\n");
                }
            }
            return;
        }
        #endregion

        #region 验证频道名称是否重复============================
        private void sys_channel_validate(HttpContext context)
        {
            string channelname = DTRequest.GetFormString("channelname");
            string oldname = DTRequest.GetFormString("oldname");
            if (string.IsNullOrEmpty(channelname))
            {
                context.Response.Write("false");
                return;
            }
            //检查是否与站点根目录下的目录同名
            Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
            DirectoryInfo dirInfo = new DirectoryInfo(Utils.GetMapPath(siteConfig.webpath));
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                if (channelname.ToLower() == dir.Name)
                {
                    context.Response.Write("false");
                    return;
                }
            }
            //检查是否修改操作
            if (channelname == oldname)
            {
                context.Response.Write("true");
                return;
            }
            //检查Key是否与已存在
            BLL.sys_channel bll = new BLL.sys_channel();
            if (bll.Exists(channelname))
            {
                context.Response.Write("false");
                return;
            }
            context.Response.Write("true");
            return;
        }
        #endregion

        #region 验证URL重写是否重复=============================
        private void sys_urlrewrite_validate(HttpContext context)
        {
            string rewritekey = DTRequest.GetFormString("rewritekey");
            string oldkey = DTRequest.GetFormString("oldkey");
            if (string.IsNullOrEmpty(rewritekey))
            {
                context.Response.Write("false1");
                return;
            }
            //检查是否修改操作
            if (rewritekey.ToLower() == oldkey.ToLower())
            {
                context.Response.Write("true");
                return;
            }
            //检查站点URL配置文件节点是否重复
            List<Model.url_rewrite> ls = new BLL.url_rewrite().GetList("");
            foreach (Model.url_rewrite model in ls)
            {
                if (model.name.ToLower() == rewritekey.ToLower())
                {
                    context.Response.Write("false2");
                    return;
                }
            }
            context.Response.Write("true");
            return;
        }
        #endregion

        #region 验证用户名是否可用==============================
        private void validate_username(HttpContext context)
        {
            string username = DTRequest.GetFormString("username");
            string oldusername = DTRequest.GetFormString("oldusername");
            //如果为Null，退出
            if (string.IsNullOrEmpty(username))
            {
                context.Response.Write("false");
                return;
            }
            Model.userconfig userConfig = new BLL.userconfig().loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_USER_XML_CONFING));
            //过滤注册用户名字符
            string[] strArray = userConfig.regkeywords.Split(',');
            foreach (string s in strArray)
            {
                if (s.ToLower() == username.ToLower())
                {
                    context.Response.Write("false");
                    return;
                }
            }
            //检查是否修改操作
            if (username == oldusername)
            {
                context.Response.Write("true");
                return;
            }
            BLL.users bll = new BLL.users();
            //查询数据库
            if (bll.Exists(username.Trim()))
            {
                context.Response.Write("false");
                return;
            }
            context.Response.Write("true");
            return;
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}