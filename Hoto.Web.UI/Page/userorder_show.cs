using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using Hoto.Common;

namespace Hoto.Web.UI.Page
{
    public partial class userorder_show : Web.UI.UserPage
    {
        protected int id;
        protected Hoto.Model.orders model;
        protected Hoto.Model.payment payModel;

        /// <summary>
        /// 重写虚方法,此方法在Init事件执行
        /// </summary>
        protected override void InitPage()
        {
            id = HotoRequest.GetQueryInt("id");
            Hoto.BLL.orders bll = new Hoto.BLL.orders();
            if (!bll.Exists(id))
            {
                HttpContext.Current.Response.Redirect(config.webpath + "error.aspx?msg=" + HotoUtils.UrlEncode("出错啦，您要浏览的页面不存在或已删除啦！"));
                return;
            }
            model = bll.GetModel(id);
            payModel = new Hoto.BLL.payment().GetModel(model.payment_id);
            if (payModel == null)
            {
                payModel = new Hoto.Model.payment();
            }
        }

    }
}
