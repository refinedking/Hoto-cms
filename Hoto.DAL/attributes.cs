using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Hoto.DBUtility;

namespace Hoto.DAL
{
	/// <summary>
	/// 扩展属性
	/// </summary>
	public partial class attributes
	{
		public attributes()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from dt_attributes");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Hoto.Model.attributes model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into dt_attributes(");
			strSql.Append("channel_id,title,remark,type,default_value,sort_id,add_time)");
			strSql.Append(" values (");
			strSql.Append("@channel_id,@title,@remark,@type,@default_value,@sort_id,@add_time)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@type", SqlDbType.Int,4),
					new SqlParameter("@default_value", SqlDbType.NVarChar,500),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@add_time", SqlDbType.DateTime)};
			parameters[0].Value = model.channel_id;
			parameters[1].Value = model.title;
			parameters[2].Value = model.remark;
			parameters[3].Value = model.type;
			parameters[4].Value = model.default_value;
			parameters[5].Value = model.sort_id;
			parameters[6].Value = model.add_time;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(Hoto.Model.attributes model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update dt_attributes set ");
			strSql.Append("channel_id=@channel_id,");
			strSql.Append("title=@title,");
			strSql.Append("remark=@remark,");
			strSql.Append("type=@type,");
			strSql.Append("default_value=@default_value,");
			strSql.Append("sort_id=@sort_id,");
			strSql.Append("add_time=@add_time");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@type", SqlDbType.Int,4),
					new SqlParameter("@default_value", SqlDbType.NVarChar,500),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.channel_id;
			parameters[1].Value = model.title;
			parameters[2].Value = model.remark;
			parameters[3].Value = model.type;
			parameters[4].Value = model.default_value;
			parameters[5].Value = model.sort_id;
			parameters[6].Value = model.add_time;
			parameters[7].Value = model.id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
            List<CommandInfo> sqllist = new List<CommandInfo>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_attribute_value ");
            strSql.Append(" where attribute_id=@attribute_id");
            SqlParameter[] parameters = {
					new SqlParameter("@attribute_id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from dt_attributes ");
            strSql2.Append(" where id=@id");
            SqlParameter[] parameters2 = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters2[0].Value = id;

            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
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
		public Hoto.Model.attributes GetModel(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,channel_id,title,remark,type,default_value,sort_id,add_time from dt_attributes ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			Hoto.Model.attributes model=new Hoto.Model.attributes();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["id"]!=null && ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["channel_id"]!=null && ds.Tables[0].Rows[0]["channel_id"].ToString()!="")
				{
					model.channel_id=int.Parse(ds.Tables[0].Rows[0]["channel_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["title"]!=null && ds.Tables[0].Rows[0]["title"].ToString()!="")
				{
					model.title=ds.Tables[0].Rows[0]["title"].ToString();
				}
				if(ds.Tables[0].Rows[0]["remark"]!=null && ds.Tables[0].Rows[0]["remark"].ToString()!="")
				{
					model.remark=ds.Tables[0].Rows[0]["remark"].ToString();
				}
				if(ds.Tables[0].Rows[0]["type"]!=null && ds.Tables[0].Rows[0]["type"].ToString()!="")
				{
					model.type=int.Parse(ds.Tables[0].Rows[0]["type"].ToString());
				}
				if(ds.Tables[0].Rows[0]["default_value"]!=null && ds.Tables[0].Rows[0]["default_value"].ToString()!="")
				{
					model.default_value=ds.Tables[0].Rows[0]["default_value"].ToString();
				}
				if(ds.Tables[0].Rows[0]["sort_id"]!=null && ds.Tables[0].Rows[0]["sort_id"].ToString()!="")
				{
					model.sort_id=int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["add_time"]!=null && ds.Tables[0].Rows[0]["add_time"].ToString()!="")
				{
					model.add_time=DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
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
			strSql.Append("select id,channel_id,title,remark,type,default_value,sort_id,add_time ");
			strSql.Append(" FROM dt_attributes ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" order by sort_id asc,add_time desc");
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,channel_id,title,remark,type,default_value,sort_id,add_time ");
			strSql.Append(" FROM dt_attributes ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  Method
	}
}

