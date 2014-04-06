using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.orders
{
    public partial class order_print : Web.UI.ManagePage
    {
        private int id = 0;
        protected Model.orders model = new Model.orders();
        protected Model.manager managerModel;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.id = DTRequest.GetQueryInt("id");
            if (this.id == 0)
            {
                JscriptMsg("传输参数不正确！", "back", "Error");
                return;
            }
            if (!new BLL.orders().Exists(this.id))
            {
                JscriptMsg("信息不存在或已被删除！", "back", "Error");
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
            BLL.orders bll = new BLL.orders();
            model = bll.GetModel(_id);
            managerModel = GetAdminInfo();
            this.rptList.DataSource = model.order_goods;
            this.rptList.DataBind();
        }
        #endregion
    }
}