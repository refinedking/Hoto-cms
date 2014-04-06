using System;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.orders
{
    public partial class payment_edit : Web.UI.ManagePage
    {
        private int id = 0;
        protected Model.payment model = new Model.payment();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.id = DTRequest.GetQueryInt("id");
            if (this.id == 0)
            {
                JscriptMsg("传输参数不正确！", "back", "Error");
                return;
            }
            if (!new BLL.payment().Exists(this.id))
            {
                JscriptMsg("支付方式不存在或已删除！", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
                ShowInfo(this.id);
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.payment bll = new BLL.payment();
            model = bll.GetModel(_id);
            txtTitle.Text = model.title;
            rblType.SelectedValue = model.type.ToString();
            rblIsLock.SelectedValue = model.is_lock.ToString();
            txtSortId.Text = model.sort_id.ToString();
            rblPoundageType.SelectedValue = model.poundage_type.ToString();
            txtPoundageAmount.Text = model.poundage_amount.ToString();
            //txtApiPath.Text = model.api_path;
            txtImgUrl.Text = model.img_url;
            txtRemark.Text = model.remark;
            if (model.api_path.ToLower() == "alipay")
            {
                //支付宝
                XmlDocument doc = XmlHelper.LoadXmlDoc(Utils.GetMapPath("~/xmlconfig/alipay.config"));
                txtAlipayPartner.Text = doc.SelectSingleNode(@"Root/partner").InnerText;
                txtAlipayKey.Text = doc.SelectSingleNode(@"Root/key").InnerText;
                txtAlipaySellerEmail.Text = doc.SelectSingleNode(@"Root/seller_email").InnerText;
            }
            else if (model.api_path.ToLower() == "tenpay")
            {
                //财付通
                XmlDocument doc1 = XmlHelper.LoadXmlDoc(Utils.GetMapPath("~/xmlconfig/tenpay.config"));
                txtTenpayBargainorId.Text = doc1.SelectSingleNode(@"Root/bargainor_id").InnerText;
                txtTenpayKey.Text = doc1.SelectSingleNode(@"Root/tenpay_key").InnerText;
            }
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = true;
            BLL.payment bll = new BLL.payment();
            Model.payment model = bll.GetModel(_id);

            model.title = txtTitle.Text.Trim();
            model.type = int.Parse(rblType.SelectedValue);
            model.is_lock = int.Parse(rblIsLock.SelectedValue);
            model.sort_id = int.Parse(txtSortId.Text.Trim());
            model.poundage_type = int.Parse(rblPoundageType.SelectedValue);
            model.poundage_amount = decimal.Parse(txtPoundageAmount.Text.Trim());
            //model.api_path = txtApiPath.Text.Trim();
            model.img_url = txtImgUrl.Text.Trim();
            model.remark = txtRemark.Text;
            if (model.api_path.ToLower() == "alipay")
            {
                //支付宝
                string alipayFilePath = Utils.GetMapPath("~/xmlconfig/alipay.config");
                XmlHelper.UpdateNodeInnerText(alipayFilePath, @"Root/partner", txtAlipayPartner.Text);
                XmlHelper.UpdateNodeInnerText(alipayFilePath, @"Root/key", txtAlipayKey.Text);
                XmlHelper.UpdateNodeInnerText(alipayFilePath, @"Root/seller_email", txtAlipaySellerEmail.Text);
            }
            else if (model.api_path.ToLower() == "tenpay")
            {
                //财付通
                string tenpayFilePath = Utils.GetMapPath("~/xmlconfig/tenpay.config");
                XmlHelper.UpdateNodeInnerText(tenpayFilePath, @"Root/bargainor_id", txtTenpayBargainorId.Text);
                XmlHelper.UpdateNodeInnerText(tenpayFilePath, @"Root/tenpay_key", txtTenpayKey.Text);
            }

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
            ChkAdminLevel("payment", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            if (!DoEdit(this.id))
            {
                JscriptMsg("保存过程中发生错误啦！", "", "Error");
                return;
            }
            JscriptMsg("修改配置成功啦！", "payment_list.aspx", "Success");
        }
    }
}