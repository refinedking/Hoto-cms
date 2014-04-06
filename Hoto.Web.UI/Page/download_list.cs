using System;
using System.Collections.Generic;
using System.Text;
using Hoto.Common;

namespace Hoto.Web.UI.Page
{
    public partial class download_list : Web.UI.BasePage
    {
        protected int page;         //当前页码
        protected int category_id;  //类别ID
        protected int totalcount;   //OUT数据总数

        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            page = HotoRequest.GetQueryInt("page", 1);
            category_id = HotoRequest.GetQueryInt("category_id");
        }
    }
}
