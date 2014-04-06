using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using Hoto.Common;

namespace Hoto.Web.UI.Page
{
    public partial class download_show : Web.UI.BasePage
    {
        protected int id;
        protected Hoto.Model.article_download model = new Hoto.Model.article_download();

        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            id = HotoRequest.GetQueryInt("id");
            Hoto.BLL.article bll = new Hoto.BLL.article();
            if (!bll.Exists(id))
            {
                HttpContext.Current.Response.Redirect(config.webpath + "error.aspx?msg=" + HotoUtils.UrlEncode("出错啦，您要浏览的页面不存在或已删除啦！"));
                return;
            }
            model = bll.GetDownloadModel(id);
            //浏览数+1
            bll.UpdateField(id, "click=click+1");
            //跳转URL
            if (model.link_url != null)
                model.link_url = model.link_url.Trim();
            if (!string.IsNullOrEmpty(model.link_url))
            {
                HttpContext.Current.Response.Redirect(model.link_url);
            }
        }
    }
}
