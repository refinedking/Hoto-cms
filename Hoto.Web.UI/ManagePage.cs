using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Hoto.Common;

namespace Hoto.Web.UI
{
    public class ManagePage : System.Web.UI.Page
    {
        protected internal Hoto.Model.siteconfig siteConfig;

        public ManagePage()
        {
            this.Load += new EventHandler(ManagePage_Load);
            siteConfig = new Hoto.BLL.siteconfig().loadConfig(HotoUtils.GetXmlMapPath("Configpath"));
        }

        private void ManagePage_Load(object sender, EventArgs e)
        {
            //判断管理员是否登录
            if (!IsAdminLogin())
            {
                Response.Write("<script>parent.location.href='" + siteConfig.webpath + siteConfig.webmanagepath + "/login.aspx'</script>");
                Response.End();
            }
        }

        #region 管理员============================================
        /// <summary>
        /// 判断管理员是否已经登录(解决Session超时问题)
        /// </summary>
        public bool IsAdminLogin()
        {
            //如果Session为Null
            if (Session[HotoKeys.SESSION_ADMIN_INFO] != null)
            {
                return true;
            }
            else
            {
                //检查Cookies
                string adminname = HotoUtils.GetCookie("AdminName", "DTcms"); //解密用户名
                string adminpwd = HotoUtils.GetCookie("AdminPwd", "DTcms");
                if (adminname != "" && adminpwd != "")
                {
                    Hoto.BLL.manager bll = new Hoto.BLL.manager();
                    Hoto.Model.manager model = bll.GetModel(adminname, adminpwd);
                    if (model != null)
                    {
                        Session[HotoKeys.SESSION_ADMIN_INFO] = model;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 取得管理员信息
        /// </summary>
        public Hoto.Model.manager GetAdminInfo()
        {
            if (IsAdminLogin())
            {
                Hoto.Model.manager model = Session[HotoKeys.SESSION_ADMIN_INFO] as Hoto.Model.manager;
                if (model != null)
                {
                    return model;
                }
            }
            return null;
        }

        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="channel_id">频道ID</param>
        /// <param name="action_type">操作类型</param>
        public void ChkAdminLevel(int channel_id, string action_type)
        {
            Hoto.Model.manager model = GetAdminInfo();
            Hoto.BLL.manager_role bll = new Hoto.BLL.manager_role();
            bool result = bll.Exists(model.role_id, channel_id, action_type);
            if (!result)
            {
                string msbox = "parent.f_errorTab(\"错误提示\", \"您没有管理该页面的权限，请勿尝试非法进入！\")";
                //ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox.ToString(), true);  //修正BUG
                Response.Write("<script type=\"text/javascript\">" + msbox + "</script>");
                Response.End();
            }
        }

        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="channel_name">栏目名称</param>
        /// <param name="action_type">操作类型</param>
        public void ChkAdminLevel(string channel_name, string action_type)
        {
            Hoto.Model.manager model = GetAdminInfo();
            Hoto.BLL.manager_role bll = new Hoto.BLL.manager_role();
            bool result = bll.Exists(model.role_id, channel_name, action_type);
            if (!result)
            {
                string msbox = "parent.f_errorTab(\"错误提示\", \"您没有管理该页面的权限，请勿尝试非法进入！\")";
                //ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox.ToString(), true);  //修正BUG
                Response.Write("<script type=\"text/javascript\">" + msbox + "</script>");
                Response.End();
            }
        }

        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="channel_name">栏目名称</param>
        /// <param name="action_type">操作类型</param>
        /// <returns>bool</returns>
        public bool IsAdminLevel(string channel_name, string action_type)
        {
            Hoto.Model.manager model = GetAdminInfo();
            Hoto.BLL.manager_role bll = new Hoto.BLL.manager_role();
            return bll.Exists(model.role_id, channel_name, action_type);
        }

        #endregion

        #region JS提示============================================

        /// <summary>
        /// 添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        protected void JscriptMsg(string msgtitle, string url, string msgcss)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }

        /// <summary>
        /// 带回传函数的添加编辑删除提示
        /// </summary>
        /// <param name="msgtitle">提示文字</param>
        /// <param name="url">返回地址</param>
        /// <param name="msgcss">CSS样式</param>
        /// <param name="callback">JS回调函数</param>
        protected void JscriptMsg(string msgtitle, string url, string msgcss, string callback)
        {
            string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\", " + callback + ")";
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        }
        #endregion

    }
}
