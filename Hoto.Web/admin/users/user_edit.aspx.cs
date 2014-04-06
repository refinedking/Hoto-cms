using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    public partial class user_edit : Web.UI.ManagePage
    {
        protected string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                this.id = DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!new BLL.users().Exists(this.id))
                {
                    JscriptMsg("信息不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                TreeBind("is_lock=0"); //绑定类别
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }

            }
        }

        #region 绑定类别=================================
        private void TreeBind(string strWhere)
        {
            BLL.user_groups bll = new BLL.user_groups();
            DataTable dt = bll.GetList(0, strWhere, "grade asc,id asc").Tables[0];

            this.ddlGroupId.Items.Clear();
            this.ddlGroupId.Items.Add(new ListItem("请选择组别...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                this.ddlGroupId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(_id);

            ddlGroupId.SelectedValue = model.group_id.ToString();
            rblIsLock.SelectedValue = model.is_lock.ToString();
            txtUserName.Enabled = false;
            txtUserName.Text = model.user_name;
            hidUserName.Value = model.user_name;
            txtPassword.Attributes["value"] = model.password;
            txtEmail.Text = model.email;
            txtNickName.Text = model.nick_name;
            txtAvatar.Text = model.avatar;
            rblSex.SelectedValue = model.sex;
            if (model.birthday != null)
            {
                txtBirthday.Text = model.birthday.GetValueOrDefault().ToString("yyyy-M-d");
            }
            txtTelphone.Text = model.telphone;
            txtMobile.Text = model.mobile;
            txtQQ.Text = model.qq;
            txtAddress.Text = model.address;
            txtAmount.Text = model.amount.ToString();
            txtPoint.Text = model.point.ToString();
            txtExp.Text = model.exp.ToString();
            lblRegTime.Text = model.reg_time.ToString();
            lblRegIP.Text = model.reg_ip.ToString();
            //查找最近登录信息
            Model.user_login_log logModel = new BLL.user_login_log().GetLastModel(model.user_name);
            if (logModel != null)
            {
                lblLastTime.Text = logModel.login_time.ToString();
                lblLastIP.Text = logModel.login_ip;
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.users model = new Model.users();
            BLL.users bll = new BLL.users();

            model.group_id = int.Parse(ddlGroupId.SelectedValue);
            model.is_lock = int.Parse(rblIsLock.SelectedValue);
            model.user_name = txtUserName.Text.Trim();
            model.password = DESEncrypt.Encrypt(txtPassword.Text);
            model.email = txtEmail.Text;
            model.nick_name = txtNickName.Text;
            model.avatar = txtAvatar.Text;
            model.sex = rblSex.SelectedValue;
            DateTime _birthday;
            if (DateTime.TryParse(txtBirthday.Text.Trim(), out _birthday))
            {
                model.birthday = _birthday;
            }
            model.telphone = txtTelphone.Text.Trim();
            model.mobile = txtMobile.Text.Trim();
            model.qq = txtQQ.Text;
            model.address = txtAddress.Text.Trim();
            model.amount = decimal.Parse(txtAmount.Text.Trim());
            model.point = int.Parse(txtPoint.Text.Trim());
            model.exp = int.Parse(txtExp.Text.Trim());
            model.reg_time = DateTime.Now;
            model.reg_ip = DTRequest.GetIP();

            if (bll.Add(model) < 1)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = true;
            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(_id);

            model.group_id = int.Parse(ddlGroupId.SelectedValue);
            model.is_lock = int.Parse(rblIsLock.SelectedValue);
            if (txtPassword.Text.Trim() != model.password)
            {
                model.password = DESEncrypt.Encrypt(txtPassword.Text.Trim());
            }
            model.email = txtEmail.Text;
            model.nick_name = txtNickName.Text;
            model.avatar = txtAvatar.Text;
            model.sex = rblSex.SelectedValue;
            DateTime _birthday;
            if (DateTime.TryParse(txtBirthday.Text.Trim(), out _birthday))
            {
                model.birthday = _birthday;
            }
            model.telphone = txtTelphone.Text.Trim();
            model.mobile = txtMobile.Text.Trim();
            model.qq = txtQQ.Text;
            model.address = txtAddress.Text.Trim();
            model.amount = decimal.Parse(txtAmount.Text.Trim());
            model.point = int.Parse(txtPoint.Text.Trim());
            model.exp = int.Parse(txtExp.Text.Trim());

            if (!bll.Update(model))
            {
                result = false;
            }
            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("users", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改用户成功啦！", "user_list.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("users", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (new BLL.users().Exists(txtUserName.Text.Trim()))
                {
                    JscriptMsg("用户名已存在啦！", "", "Error");
                    return;
                }
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", "", "Error");
                    return;
                }
                JscriptMsg("添加用户成功啦！", "user_list.aspx", "Success");
            }
        }

    }
}