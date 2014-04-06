using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using Hoto.Common;

namespace Hoto.Web.UI.Page
{
    public partial class error : Web.UI.BasePage
    {
        protected string msg = string.Empty;

        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            msg = HotoRequest.GetQueryString("msg");
        }

    }
}
