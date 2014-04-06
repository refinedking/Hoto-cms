using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Hoto.DBUtility;
using Hoto.Common;

namespace Hoto.DAL
{
	/// <summary>
	/// 图片相册
	/// </summary>
	public partial class article_albums
	{
		public article_albums()
		{}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.article_albums> GetList(int article_id)
        {
            List<Model.article_albums> modelList = new List<Model.article_albums>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,article_id,big_img,small_img,remark ");
            strSql.Append(" FROM dt_article_albums ");
            strSql.Append(" where article_id=" + article_id);
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];

            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.article_albums model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.article_albums();
                    if (dt.Rows[n]["id"] != null && dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["article_id"] != null && dt.Rows[n]["article_id"].ToString() != "")
                    {
                        model.article_id = int.Parse(dt.Rows[n]["article_id"].ToString());
                    }
                    if (dt.Rows[n]["big_img"] != null && dt.Rows[n]["big_img"].ToString() != "")
                    {
                        model.big_img = dt.Rows[n]["big_img"].ToString();
                    }
                    if (dt.Rows[n]["small_img"] != null && dt.Rows[n]["small_img"].ToString() != "")
                    {
                        model.small_img = dt.Rows[n]["small_img"].ToString();
                    }
                    if (dt.Rows[n]["remark"] != null && dt.Rows[n]["remark"].ToString() != "")
                    {
                        model.remark = dt.Rows[n]["remark"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 查找不存在的图片并删除已删除的图片及数据
        /// </summary>
        public void DeleteList(SqlConnection conn, SqlTransaction trans, List<Model.article_albums> models, int article_id)
        {
            StringBuilder idList = new StringBuilder();
            if (models != null)
            {
                foreach (Model.article_albums modelt in models)
                {
                    if (modelt.id > 0)
                    {
                        idList.Append(modelt.id + ",");
                    }
                }
            }
            string id_list = HotoUtils.DelLastChar(idList.ToString(), ",");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,big_img,small_img from dt_article_albums where article_id=" + article_id);
            if (!string.IsNullOrEmpty(id_list))
            {
                strSql.Append(" and id not in(" + id_list + ")");
            }
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int rows = DbHelperSQL.ExecuteSql(conn, trans, "delete from dt_article_albums where id=" + dr["id"].ToString()); //删除数据库
                if (rows > 0)
                {
                    HotoUtils.DeleteFile(dr["big_img"].ToString()); //删除原图
                    HotoUtils.DeleteFile(dr["small_img"].ToString()); //删除缩略图
                }
            }
        }

        /// <summary>
        /// 删除相册图片
        /// </summary>
        public void DeleteFile(List<Model.article_albums> models)
        {
            if (models != null)
            {
                foreach (Model.article_albums modelt in models)
                {
                    HotoUtils.DeleteFile(modelt.big_img);
                    HotoUtils.DeleteFile(modelt.small_img);
                }
            }
        }

	}
}

