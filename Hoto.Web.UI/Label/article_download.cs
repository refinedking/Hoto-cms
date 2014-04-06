using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Hoto.Web.UI
{
    public partial class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 下载列表
        /// </summary>
        /// <param name="channel_id">频道ID</param>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_download_list(int channel_id, int category_id, int top, string strwhere)
        {
            DataTable dt = new DataTable();
            if (channel_id > 0)
            {
                string _where = "channel_id=" + channel_id;
                if (category_id > 0)
                {
                    _where += " and category_id in(select id from dt_category where channel_id=" + channel_id + " and class_list like '%," + category_id + ",%')";
                }
                if (!string.IsNullOrEmpty(strwhere))
                {
                    _where += " and " + strwhere;
                }
                dt = new Hoto.BLL.article().GetDownloadList(top, _where, "sort_id asc,add_time desc").Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 下载列表
        /// </summary>
        /// <param name="channel_id">频道ID</param>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_download_list(int channel_id, int category_id, int top, string strwhere, string orderby)
        {
            DataTable dt = new DataTable();
            if (channel_id > 0)
            {
                string _where = "channel_id=" + channel_id;
                if (category_id > 0)
                {
                    _where += " and category_id in(select id from dt_category where channel_id=" + channel_id + " and class_list like '%," + category_id + ",%')";
                }
                if (!string.IsNullOrEmpty(strwhere))
                {
                    _where += " and " + strwhere;
                }
                dt = new Hoto.BLL.article().GetDownloadList(top, _where, orderby).Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 下载分页列表
        /// </summary>
        /// <param name="channel_id">频道ID</param>
        /// <param name="page_size">页面大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DateTable</returns>
        protected DataTable get_download_list(int channel_id, int category_id, int page_size, int page_index, string strwhere, out int totalcount)
        {
            DataTable dt = new DataTable();
            if (channel_id > 0)
            {
                string _where = "channel_id=" + channel_id;
                if (category_id > 0)
                {
                    _where += " and category_id in(select id from dt_category where channel_id=" + channel_id + " and class_list like '%," + category_id + ",%')";
                }
                if (!string.IsNullOrEmpty(strwhere))
                {
                    _where += " and " + strwhere;
                }
                dt = new Hoto.BLL.article().GetDownloadList(page_size, page_index, _where, "sort_id asc,add_time desc", out totalcount).Tables[0];
            }
            else
            {
                totalcount = 0;
            }
            return dt;
        }
    }
}
