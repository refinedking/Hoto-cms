using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.orders
{
    public partial class payment_list : Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("payment", DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind("sort_id asc,is_lock asc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _orderby)
        {
            BLL.payment bll = new BLL.payment();
            this.rptList.DataSource = bll.GetList(0, "", _orderby);
            this.rptList.DataBind();
        }
        #endregion

    }
}