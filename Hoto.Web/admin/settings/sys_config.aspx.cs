using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class sys_config : Web.UI.ManagePage
    {
        string defaultpassword = "0|0|0|0"; //默认显示密码
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_config", DTEnums.ActionEnum.View.ToString()); //检查权限
                ShowInfo();
            }
        }

        #region 赋值操作=================================
        private void ShowInfo()
        {
            BLL.siteconfig bll = new BLL.siteconfig();
            Model.siteconfig model = bll.loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
            webname.Text = model.webname;
            webcompany.Text = model.webcompany;
            weburl.Text = model.weburl;
            webtel.Text = model.webtel;
            webfax.Text = model.webfax;
            webmail.Text = model.webmail;
            webcrod.Text = model.webcrod;
            webtitle.Text = model.webtitle;
            webkeyword.Text = model.webkeyword;
            webdescription.Text = model.webdescription;
            webcopyright.Text = model.webcopyright;
            webpath.Text = model.webpath;
            webmanagepath.Text = model.webmanagepath;
            webstatus.Text = model.webstatus.ToString();
            webclosereason.Text = model.webclosereason;
            webcountcode.Text = model.webcountcode;

            staticstatus.SelectedValue = model.staticstatus.ToString();
            staticextension.Text = model.staticextension;
            memberstatus.SelectedValue = model.memberstatus.ToString();
            commentstatus.SelectedValue = model.commentstatus.ToString();
            logstatus.SelectedValue = model.logstatus.ToString();

            emailstmp.Text = model.emailstmp;
            emailport.Text = model.emailport.ToString();
            emailfrom.Text = model.emailfrom;
            emailusername.Text = model.emailusername;
            if (!string.IsNullOrEmpty(model.emailpassword))
            {
                emailpassword.Attributes["value"] = defaultpassword;
            }
            emailnickname.Text = model.emailnickname;

            attachpath.Text = model.attachpath;
            attachextension.Text = model.attachextension;
            attachsave.SelectedValue = model.attachsave.ToString();
            attachfilesize.Text = model.attachfilesize.ToString();
            attachimgsize.Text = model.attachimgsize.ToString();
            attachimgmaxheight.Text = model.attachimgmaxheight.ToString();
            attachimgmaxwidth.Text = model.attachimgmaxwidth.ToString();
            thumbnailheight.Text = model.thumbnailheight.ToString();
            thumbnailwidth.Text = model.thumbnailwidth.ToString();
            watermarktype.SelectedValue = model.watermarktype.ToString();
            watermarkposition.Text = model.watermarkposition.ToString();
            watermarkimgquality.Text = model.watermarkimgquality.ToString();
            watermarkpic.Text = model.watermarkpic;
            watermarktransparency.Text = model.watermarktransparency.ToString();
            watermarktext.Text = model.watermarktext;
            watermarkfont.Text = model.watermarkfont;
            watermarkfontsize.Text = model.watermarkfontsize.ToString();
        }
        #endregion

        /// <summary>
        /// 保存配置信息
        /// </summary>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_config", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.siteconfig bll = new BLL.siteconfig();
            Model.siteconfig model = bll.loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
            try
            {
                model.webname = webname.Text;
                model.webcompany = webcompany.Text;
                model.weburl = weburl.Text;
                model.webtel = webtel.Text;
                model.webfax = webfax.Text;
                model.webmail = webmail.Text;
                model.webcrod = webcrod.Text;
                model.webtitle = webtitle.Text;
                model.webkeyword = webkeyword.Text;
                model.webdescription = Utils.DropHTML(webdescription.Text);
                model.webcopyright = webcopyright.Text;
                model.webpath = webpath.Text;
                model.webmanagepath = webmanagepath.Text;
                model.webstatus = int.Parse(webstatus.Text.Trim());
                model.webclosereason = webclosereason.Text;
                model.webcountcode = webcountcode.Text;

                model.staticstatus = int.Parse(staticstatus.SelectedValue);
                model.staticextension = staticextension.Text;
                model.memberstatus = int.Parse(memberstatus.SelectedValue);
                model.commentstatus = int.Parse(commentstatus.SelectedValue);
                model.logstatus = int.Parse(logstatus.SelectedValue);

                model.emailstmp = emailstmp.Text;
                model.emailport = int.Parse(emailport.Text.Trim());
                model.emailfrom = emailfrom.Text;
                model.emailusername = emailusername.Text;
                //判断密码是否更改
                if (emailpassword.Text.Trim() != defaultpassword)
                {
                    model.emailpassword = DESEncrypt.Encrypt(emailpassword.Text, model.sysencryptstring);
                }
                model.emailnickname = emailnickname.Text;

                model.attachpath = attachpath.Text;
                model.attachextension = attachextension.Text;
                model.attachsave = int.Parse(attachsave.SelectedValue);
                model.attachfilesize = int.Parse(attachfilesize.Text.Trim());
                model.attachimgsize = int.Parse(attachimgsize.Text.Trim());
                model.attachimgmaxheight = int.Parse(attachimgmaxheight.Text.Trim());
                model.attachimgmaxwidth = int.Parse(attachimgmaxwidth.Text.Trim());
                model.thumbnailheight = int.Parse(thumbnailheight.Text.Trim());
                model.thumbnailwidth = int.Parse(thumbnailwidth.Text.Trim());
                model.watermarktype = int.Parse(watermarktype.SelectedValue);
                model.watermarkposition = int.Parse(watermarkposition.Text.Trim());
                model.watermarkimgquality = int.Parse(watermarkimgquality.Text.Trim());
                model.watermarkpic = watermarkpic.Text;
                model.watermarktransparency = int.Parse(watermarktransparency.Text.Trim());
                model.watermarktext = watermarktext.Text;
                model.watermarkfont = watermarkfont.Text;
                model.watermarkfontsize = int.Parse(watermarkfontsize.Text.Trim());

                bll.saveConifg(model, Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
                JscriptMsg("修改系统信息成功啦！", "sys_config.aspx", "Success");
            }
            catch
            {
                JscriptMsg("文件写入失败，请检查是否有权限！", "", "Error");
            }
        }
    }
}