using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Hoto.DBUtility;

namespace Hoto.DAL
{
	/// <summary>
	/// 信息顶和踩
	/// </summary>
	public partial class article_diggs
	{
        public article_diggs()
		{}
		#region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_article_diggs");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        /// <param name="id">主表ID</param>
        /// <param name="strValue">修改字段=值</param>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_article_diggs set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_diggs GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,digg_good,digg_bad from dt_article_diggs ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)			};
            parameters[0].Value = id;

            Model.article_diggs model = new Model.article_diggs();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["digg_good"] != null && ds.Tables[0].Rows[0]["digg_good"].ToString() != "")
                {
                    model.digg_good = int.Parse(ds.Tables[0].Rows[0]["digg_good"].ToString());
                }
                if (ds.Tables[0].Rows[0]["digg_bad"] != null && ds.Tables[0].Rows[0]["digg_bad"].ToString() != "")
                {
                    model.digg_bad = int.Parse(ds.Tables[0].Rows[0]["digg_bad"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

		#endregion  Method
	}
}

