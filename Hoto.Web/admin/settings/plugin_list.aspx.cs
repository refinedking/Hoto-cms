using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class plugin_list : DTcms.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_plugin", DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind();
            }
        }

        #region 插件列表绑定
        private void RptBind()
        {
            List<Model.plugin> lt = new List<Model.plugin>();
            BLL.plugin bll = new BLL.plugin();

            this.rptList.DataSource = bll.GetList(Utils.GetMapPath("../../plugins/"));
            this.rptList.DataBind();
        }
        #endregion

        #region 生成插件模板
        private void MarkTemplates(string dirName)
        {
            BLL.url_rewrite bll = new BLL.url_rewrite();
            List<Model.url_rewrite> ls = bll.GetList("");
            //插件目录
            string pluginPath = Utils.GetMapPath("../../plugins/" + dirName + "/templet/");
            if (!Directory.Exists(pluginPath))
            {
                return;
            }
            DirectoryInfo dirInfo = new DirectoryInfo(pluginPath);
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                if (!file.Name.StartsWith("_") && file.Name.EndsWith(".html"))
                {
                    foreach (Model.url_rewrite model in ls)
                    {
                        if (file.Name.ToLower() == model.templet && !string.IsNullOrEmpty(model.inherit))
                        {
                            //生成模板文件
                            PageTemplate.GetTemplate(siteConfig.webpath, "plugins/" + dirName, "templet", model.templet, model.page, model.inherit, 0, 1);
                        }
                    }
                }
            }
        }
        #endregion

        #region 删除模板文件
        private void RemoveTemplates(string dirName)
        {
            //插件目录
            string pluginPath = Utils.GetMapPath("../../plugins/" + dirName + "/" + DTKeys.FILE_PLUGIN_XML_CONFING);
            XmlNodeList xnList = XmlHelper.ReadNodes(pluginPath, "plugin/urls");
            if (xnList.Count > 0)
            {
                foreach (XmlElement xe in xnList)
                {
                    if (xe.NodeType != XmlNodeType.Comment && xe.Name.ToLower() == "rewrite" && xe.Attributes["page"] != null)
                    {
                        if (xe.Attributes["name"] != null && xe.Attributes["path"] != null && xe.Attributes["pattern"] != null &&
                            xe.Attributes["page"] != null && xe.Attributes["querystring"] != null && xe.Attributes["templet"] != null &&
                            xe.Attributes["type"] != null && xe.Attributes["inherit"] != null)
                        {
                            //删除站点下的aspx文件
                            Utils.DeleteFile(siteConfig.webpath + "aspx/" + xe.Attributes["page"].Value);
                        }
                    }
                }
            }
        }
        #endregion


        //安装插件
        protected void btnInstall_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_plugin", DTEnums.ActionEnum.Add.ToString()); //检查权限
            //插件目录
            string pluginPath = Utils.GetMapPath("../../plugins/");
            BLL.plugin bll = new BLL.plugin();
            //查找列表
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                string currDirName = ((HiddenField)rptList.Items[i].FindControl("hidDirName")).Value;
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    //是否未安装
                    Model.plugin model = bll.GetInfo(pluginPath + currDirName + @"\");
                    if (model.isload == 0)
                    {
                        //安装DLL
                        string currPath = pluginPath + currDirName + @"\bin\";
                        if (Directory.Exists(currPath))
                        {
                            string[] file = Directory.GetFiles(currPath);
                            foreach (string f in file)
                            {
                                FileInfo info = new FileInfo(f);
                                //复制DLL文件
                                if (info.Extension.ToLower() == ".dll")
                                {
                                    //移动到站点目录下
                                    string newFile = Utils.GetMapPath(siteConfig.webpath + @"bin\" + info.Name);
                                    File.Copy(info.FullName, newFile, true);
                                }
                            }
                        }
                        //执行SQL语句
                        bll.ExeSqlStr(pluginPath + currDirName + @"\", @"plugin/install");
                        //添加URL映射
                        bll.AppendNodes(pluginPath + currDirName + @"\", @"plugin/urls");
                        //生成模板
                        MarkTemplates(currDirName);
                        //修改plugins节点
                        bll.UpdateNodeValue(pluginPath + currDirName + @"\", @"plugin/isload", "1");
                    }
                }
            }
            JscriptMsg("插件安装成功啦！", "plugin_list.aspx", "Success", "parent.loadPluginsNav");

        }

        //卸载插件
        protected void btnUninstall_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_plugin", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            //插件目录
            string pluginPath = Utils.GetMapPath("../../plugins/");
            BLL.plugin bll = new BLL.plugin();
            //查找列表
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                string currDirName = ((HiddenField)rptList.Items[i].FindControl("hidDirName")).Value;
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    //是否已安装
                    Model.plugin model = bll.GetInfo(pluginPath + currDirName + @"\");
                    if (model.isload == 1)
                    {
                        string currPath = pluginPath + currDirName + @"/bin/";
                        if (Directory.Exists(currPath))
                        {
                            string[] file = Directory.GetFiles(currPath);
                            foreach (string f in file)
                            {
                                FileInfo info = new FileInfo(f);
                                //复制DLL文件
                                if (info.Extension.ToLower() == ".dll")
                                {
                                    //删除站点目录下DLL文件
                                    string newFile = Utils.GetMapPath(siteConfig.webpath + @"bin/" + info.Name);
                                    if (File.Exists(newFile))
                                    {
                                        File.Delete(newFile);
                                    }
                                }
                            }

                        }
                        //执行SQL语句
                        bll.ExeSqlStr(pluginPath + currDirName + @"\", @"plugin/uninstall");
                        //删除URL映射
                        bll.RemoveNodes(pluginPath + currDirName + @"\", @"plugin/urls");
                        //删除站点目录下的aspx文件
                        RemoveTemplates(currDirName);
                        //修改plugins节点
                        bll.UpdateNodeValue(pluginPath + currDirName + @"\", @"plugin/isload", "0");
                    }
                }
            }
            JscriptMsg("插件卸载成功啦！", "plugin_list.aspx", "Success", "parent.loadPluginsNav");

        }

        //生成模板
        protected void lbtnRemark_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_plugin", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            //插件目录
            string pluginPath = Utils.GetMapPath("../../plugins/");
            BLL.plugin bll = new BLL.plugin();
            //查找列表
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                string currDirName = ((HiddenField)rptList.Items[i].FindControl("hidDirName")).Value;
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    //是否安装
                    Model.plugin model = bll.GetInfo(pluginPath + currDirName + @"\");
                    if (model.isload == 1)
                    {
                        //生成模板
                        MarkTemplates(currDirName);
                    }
                    else
                    {
                        JscriptMsg("该插件尚未安装！", "plugin_list.aspx", "Error");
                    }
                }
            }
            JscriptMsg("生成模板成功啦！", "plugin_list.aspx", "Success");
        }

    }

}