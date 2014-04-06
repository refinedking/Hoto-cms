using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    public partial class user_config : Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("user_config", DTEnums.ActionEnum.View.ToString()); //检查权限
                ShowInfo();
            }
        }

        #region 赋值操作=================================
        private void ShowInfo()
        {
            BLL.userconfig bll = new BLL.userconfig();
            Model.userconfig model = bll.loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_USER_XML_CONFING));
            regstatus.SelectedValue = model.regstatus.ToString();
            regkeywords.Text = model.regkeywords;
            regverify.SelectedValue = model.regverify.ToString();
            regctrl.Text = model.regctrl.ToString();
            regemailexpired.Text = model.regemailexpired.ToString();
            regemailditto.SelectedValue = model.regemailditto.ToString();
            emaillogin.SelectedValue = model.emaillogin.ToString();
            regrules.SelectedValue = model.regrules.ToString();
            regrulestxt.Text = model.regrulestxt;
            regmsgstatus.SelectedValue = model.regmsgstatus.ToString();
            regmsgtxt.Text = model.regmsgtxt;

            invitecodeexpired.Text = model.invitecodeexpired.ToString();
            invitecodecount.Text = model.invitecodecount.ToString();
            invitecodenum.Text = model.invitecodenum.ToString();
            pointcashrate.Text = model.pointcashrate.ToString();
            pointinvitenum.Text = model.pointinvitenum.ToString();
        }
        #endregion

        /// <summary>
        /// 保存配置信息
        /// </summary>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("user_config", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.userconfig bll = new BLL.userconfig();
            Model.userconfig model = bll.loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_USER_XML_CONFING));
            try
            {
                model.regstatus = int.Parse(regstatus.SelectedValue);
                model.regkeywords = regkeywords.Text.Trim();
                model.regverify = int.Parse(regverify.SelectedValue);
                model.regctrl = int.Parse(regctrl.Text.Trim());
                model.regemailexpired = int.Parse(regemailexpired.Text.Trim());
                model.regemailditto = int.Parse(regemailditto.SelectedValue);
                model.emaillogin = int.Parse(emaillogin.SelectedValue);
                model.regrules = int.Parse(regrules.SelectedValue);
                model.regrulestxt = regrulestxt.Text;
                model.regmsgstatus = int.Parse(regmsgstatus.SelectedValue);
                model.regmsgtxt = regmsgtxt.Text;

                model.invitecodeexpired = int.Parse(invitecodeexpired.Text.Trim());
                model.invitecodecount = int.Parse(invitecodecount.Text.Trim());
                model.invitecodenum = int.Parse(invitecodenum.Text.Trim());
                model.pointcashrate = decimal.Parse(pointcashrate.Text.Trim());
                model.pointinvitenum = int.Parse(pointinvitenum.Text.Trim());

                bll.saveConifg(model, Utils.GetXmlMapPath(DTKeys.FILE_USER_XML_CONFING));
                JscriptMsg("修改用户配置成功啦！", "user_config.aspx", "Success");
            }
            catch
            {
                JscriptMsg("文件写入失败，请检查是否有权限！", "", "Error");
            }
        }

    }
}