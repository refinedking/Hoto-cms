using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Hoto.DBUtility;
namespace Hoto.DAL
{
	/// <summary>
	/// 数据访问类:系统频道
	/// </summary>
	public partial class sys_channel
	{
		public sys_channel()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_sys_channel");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}

        /// <summary>
        /// 检查名称是否存在
        /// </summary>
        public bool Exists(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_sys_channel");
            strSql.Append(" where name=@name");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,100)};
            parameters[0].Value = name;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Hoto.Model.sys_channel model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_sys_channel(");
            strSql.Append("model_id,name,title,sort_id)");
            strSql.Append(" values (");
            strSql.Append("@model_id,@name,@title,@sort_id)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@model_id", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.NVarChar,100),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@sort_id", SqlDbType.Int,4)};
            parameters[0].Value = model.model_id;
            parameters[1].Value = model.name;
            parameters[2].Value = model.title;
            parameters[3].Value = model.sort_id;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Hoto.Model.sys_channel model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_sys_channel set ");
            strSql.Append("model_id=@model_id,");
            strSql.Append("name=@name,");
            strSql.Append("title=@title,");
            strSql.Append("sort_id=@sort_id");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@model_id", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.NVarChar,100),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.model_id;
            parameters[1].Value = model.name;
            parameters[2].Value = model.title;
            parameters[3].Value = model.sort_id;
            parameters[4].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_sys_channel ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Hoto.Model.sys_channel GetModel(int id)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,model_id,name,title,sort_id from dt_sys_channel ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Hoto.Model.sys_channel model = new Hoto.Model.sys_channel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["model_id"] != null && ds.Tables[0].Rows[0]["model_id"].ToString() != "")
                {
                    model.model_id = int.Parse(ds.Tables[0].Rows[0]["model_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["name"] != null && ds.Tables[0].Rows[0]["name"].ToString() != "")
                {
                    model.name = ds.Tables[0].Rows[0]["name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["title"] != null && ds.Tables[0].Rows[0]["title"].ToString() != "")
                {
                    model.title = ds.Tables[0].Rows[0]["title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sort_id"] != null && ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select id,model_id,name,title,sort_id,model_title ");
            strSql.Append(" FROM view_sys_channel ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" order by sort_id asc,id desc");
            return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  Method
	}
}

