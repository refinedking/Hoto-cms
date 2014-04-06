using System;
using System.IO;
using System.Data;
using System.Xml;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class templet_list : DTcms.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_templet", DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind();
            }
        }

        #region 数据绑定===============================================
        private void RptBind()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("skinname", Type.GetType("System.String"));
            dt.Columns.Add("name", Type.GetType("System.String"));
            dt.Columns.Add("img", Type.GetType("System.String"));
            dt.Columns.Add("author", Type.GetType("System.String"));
            dt.Columns.Add("createdate", Type.GetType("System.String"));
            dt.Columns.Add("version", Type.GetType("System.String"));
            dt.Columns.Add("fordntver", Type.GetType("System.String"));

            DirectoryInfo dirInfo = new DirectoryInfo(Utils.GetMapPath("../../templates/"));
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                DataRow dr = dt.NewRow();
                Model.template model = GetInfo(dir.FullName);
                if (model != null)
                {
                    dr["skinname"] = dir.Name;  //文件夹名称
                    dr["name"] = model.name;    // 模板名称
                    dr["img"] = "../../templates/" + dir.Name + "/about.png";   // 模板图片
                    dr["author"] = model.author;    //作者
                    dr["createdate"] = model.createdate;    //创建日期
                    dr["version"] = model.version;  //模板版本
                    dr["fordntver"] = model.fordntver;  //适用的版本
                    dt.Rows.Add(dr);
                }
            }
            this.rptList.DataSource = dt;
            this.rptList.DataBind();
        }
        #endregion

        #region 读取模板配置信息========================================
        /// <summary>
        /// 从模板说明文件中获得模板说明信息
        /// </summary>
        /// <param name="xmlPath">模板路径(不包含文件名)</param>
        /// <returns>模板说明信息</returns>
        private Model.template GetInfo(string xmlPath)
        {
            Model.template model = new Model.template();
            ///存放关于信息的文件 about.xml是否存在,不存在返回空串
            if (!File.Exists(xmlPath + @"\about.xml"))
            {
                return null;
            }
            try
            {
                XmlNodeList xnList = XmlHelper.ReadNodes(xmlPath + @"\about.xml", "about");
                foreach (XmlNode n in xnList)
                {
                    if (n.NodeType != XmlNodeType.Comment && n.Name.ToLower() == "template")
                    {
                        model.name = n.Attributes["name"] != null ? n.Attributes["name"].Value.ToString() : "";
                        model.author = n.Attributes["author"] != null ? n.Attributes["author"].Value.ToString() : "";
                        model.createdate = n.Attributes["createdate"] != null ? n.Attributes["createdate"].Value.ToString() : "";
                        model.version = n.Attributes["version"] != null ? n.Attributes["version"].Value.ToString() : "";
                        model.fordntver = n.Attributes["fordntver"] != null ? n.Attributes["fordntver"].Value.ToString() : "";
                    }
                }
            }
            catch
            {
                return null;
            }
            return model;
        }
        #endregion

        #region 全部生成模板============================================
        /// <summary>
        /// 生成全部模板
        /// </summary>
        private void MarkTemplates(string skinName)
        {
            //取得ASP目录下的所有文件
            DirectoryInfo dirFile = new DirectoryInfo(Utils.GetMapPath(siteConfig.webpath + "aspx/"));
            //获得URL映射列表
            BLL.url_rewrite bll = new BLL.url_rewrite();
            List<Model.url_rewrite> ls = bll.GetList("");
            //删除不属于URL映射表里的文件，防止冗余
            foreach (FileInfo file in dirFile.GetFiles())
            {
                //检查文件
                Model.url_rewrite model2 = ls.Find(p => p.page.ToLower() == file.Name.ToLower());
                if (model2 == null)
                {
                    file.Delete();
                }
            }
            //遍历站点目录的templates文件夹下的模板文件
            DirectoryInfo dirInfo = new DirectoryInfo(Utils.GetMapPath(siteConfig.webpath + @"templates/" + skinName));
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                if (!file.Name.StartsWith("_") && file.Name.EndsWith(".html"))
                {
                    //查找相对应的继承类名
                    foreach (Model.url_rewrite model in ls)
                    {
                        if (file.Name.ToLower()==model.templet && !string.IsNullOrEmpty(model.inherit))
                        {
                            //检查频道ID
                            int channelId = Utils.StrToInt(model.channel, 0);
                            //生成模板文件
                            PageTemplate.GetTemplate(siteConfig.webpath, "templates", skinName, model.templet, model.page, model.inherit, channelId, 1);
                        }
                    }
                }
            }
        }
        #endregion

        //启用模板
        protected void lbtnStart_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_templet", DTEnums.ActionEnum.Add.ToString()); //检查权限
            BLL.siteconfig bll = new BLL.siteconfig();
            Model.siteconfig model = siteConfig;
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                string skinName = ((HiddenField)rptList.Items[i].FindControl("hideSkinName")).Value;
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    //判是否当前模板
                    if (skinName.ToLower() == siteConfig.templateskin)
                    {
                        JscriptMsg("该模板已是当前模板啦！", "back", "Warning");
                        return;
                    }
                    model.templateskin = skinName.ToLower();
                    //修改配置文件
                    bll.saveConifg(model, Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
                    //重新生成模板
                    MarkTemplates(skinName);
                    JscriptMsg("模板启用并全部生成成功啦！", "templet_list.aspx", "Success");
                    return;
                }
            }
        }

        //生成模板
        protected void lbtnRemark_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_templet", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                string skinName = ((HiddenField)rptList.Items[i].FindControl("hideSkinName")).Value;
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    //判是否当前模板
                    if (skinName.ToLower() != siteConfig.templateskin)
                    {
                        JscriptMsg("该模板不是当前模板，生成失败！", "back", "Error");
                        return;
                    }
                    //重新生成模板
                    MarkTemplates(skinName);
                    JscriptMsg("模板已全部重新生成啦！", "templet_list.aspx", "Success");
                    return;
                }
            }
        }

        //管理模板
        protected void lbtnManage_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                string skinName = ((HiddenField)rptList.Items[i].FindControl("hideSkinName")).Value;
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    Response.Redirect("templet_file_list.aspx?skin=" + Utils.UrlEncode(skinName));
                }
            }
        }
    }
}