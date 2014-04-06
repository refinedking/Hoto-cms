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
	/// 内容模型
	/// </summary>
	public partial class article
	{
		#region  Method
        /// <summary>
        /// 得到最前的内容页
        /// </summary>
        public string GetCallIndex()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 call_index from view_article_content");
            strSql.Append(" order by sort_id asc,id desc");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj != null)
            {
                return obj.ToString();
            }
            return null;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ContentExists(string call_index)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(0) from view_article_content");
            strSql.Append(" where call_index=@call_index ");
            SqlParameter[] parameters = {
					new SqlParameter("@call_index", SqlDbType.NVarChar,50)};
            parameters[0].Value = call_index;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 修改内容副表一列数据
        /// </summary>
        public void UpdateContentField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_article_content set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.article_content model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_article(");
            strSql.Append("channel_id,category_id,title,link_url,img_url,seo_title,seo_keywords,seo_description,content,sort_id,click,is_lock,user_id,add_time)");
            strSql.Append(" values (");
            strSql.Append("@channel_id,@category_id,@title,@link_url,@img_url,@seo_title,@seo_keywords,@seo_description,@content,@sort_id,@click,@is_lock,@user_id,@add_time)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@category_id", SqlDbType.Int,4),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@link_url", SqlDbType.NVarChar,255),
					new SqlParameter("@img_url", SqlDbType.NVarChar,255),
					new SqlParameter("@seo_title", SqlDbType.NVarChar,255),
					new SqlParameter("@seo_keywords", SqlDbType.NVarChar,255),
					new SqlParameter("@seo_description", SqlDbType.NVarChar,255),
					new SqlParameter("@content", SqlDbType.NText),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@click", SqlDbType.Int,4),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.channel_id;
            parameters[1].Value = model.category_id;
            parameters[2].Value = model.title;
            parameters[3].Value = model.link_url;
            parameters[4].Value = model.img_url;
            parameters[5].Value = model.seo_title;
            parameters[6].Value = model.seo_keywords;
            parameters[7].Value = model.seo_description;
            parameters[8].Value = model.content;
            parameters[9].Value = model.sort_id;
            parameters[10].Value = model.click;
            parameters[11].Value = model.is_lock;
            parameters[12].Value = model.user_id;
            parameters[13].Value = model.add_time;
            parameters[14].Direction = ParameterDirection.Output;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //副表信息
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("insert into dt_article_content(");
            strSql2.Append("id,call_index,is_msg,is_red)");
            strSql2.Append(" values (");
            strSql2.Append("@id,@call_index,@is_msg,@is_red)");
            SqlParameter[] parameters2 = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@call_index", SqlDbType.NVarChar,50),
					new SqlParameter("@is_msg", SqlDbType.TinyInt,1),
					new SqlParameter("@is_red", SqlDbType.TinyInt,1)};
            parameters2[0].Direction = ParameterDirection.InputOutput;
            parameters2[1].Value = model.call_index;
            parameters2[2].Value = model.is_msg;
            parameters2[3].Value = model.is_red;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            //顶和踩
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("insert into dt_article_diggs(");
            strSql3.Append("id,digg_good,digg_bad)");
            strSql3.Append(" values (");
            strSql3.Append("@id,@digg_good,@digg_bad)");
            SqlParameter[] parameters3 = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@digg_good", SqlDbType.Int,4),
					new SqlParameter("@digg_bad", SqlDbType.Int,4)};
            parameters3[0].Direction = ParameterDirection.InputOutput;
            parameters3[1].Value = model.digg_good;
            parameters3[2].Value = model.digg_bad;
            cmd = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd);

            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[14].Value;
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.article_content model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_article set ");
            strSql.Append("channel_id=@channel_id,");
            strSql.Append("category_id=@category_id,");
            strSql.Append("title=@title,");
            strSql.Append("link_url=@link_url,");
            strSql.Append("img_url=@img_url,");
            strSql.Append("seo_title=@seo_title,");
            strSql.Append("seo_keywords=@seo_keywords,");
            strSql.Append("seo_description=@seo_description,");
            strSql.Append("content=@content,");
            strSql.Append("sort_id=@sort_id,");
            strSql.Append("click=@click,");
            strSql.Append("is_lock=@is_lock,");
            strSql.Append("user_id=@user_id,");
            strSql.Append("add_time=@add_time");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@category_id", SqlDbType.Int,4),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@link_url", SqlDbType.NVarChar,255),
					new SqlParameter("@img_url", SqlDbType.NVarChar,255),
					new SqlParameter("@seo_title", SqlDbType.NVarChar,255),
					new SqlParameter("@seo_keywords", SqlDbType.NVarChar,255),
					new SqlParameter("@seo_description", SqlDbType.NVarChar,255),
					new SqlParameter("@content", SqlDbType.NText),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@click", SqlDbType.Int,4),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.channel_id;
            parameters[1].Value = model.category_id;
            parameters[2].Value = model.title;
            parameters[3].Value = model.link_url;
            parameters[4].Value = model.img_url;
            parameters[5].Value = model.seo_title;
            parameters[6].Value = model.seo_keywords;
            parameters[7].Value = model.seo_description;
            parameters[8].Value = model.content;
            parameters[9].Value = model.sort_id;
            parameters[10].Value = model.click;
            parameters[11].Value = model.is_lock;
            parameters[12].Value = model.user_id;
            parameters[13].Value = model.add_time;
            parameters[14].Value = model.id;

            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //修改副表
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("update dt_article_content set ");
            strSql2.Append("id=@id,");
            strSql2.Append("call_index=@call_index,");
            strSql2.Append("is_msg=@is_msg,");
            strSql2.Append("is_red=@is_red");
            strSql2.Append(" where id=@id ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@call_index", SqlDbType.NVarChar,50),
					new SqlParameter("@is_msg", SqlDbType.TinyInt,1),
					new SqlParameter("@is_red", SqlDbType.TinyInt,1)};
            parameters2[0].Value = model.id;
            parameters2[1].Value = model.call_index;
            parameters2[2].Value = model.is_msg;
            parameters2[3].Value = model.is_red;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            //修改顶和踩
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("update dt_article_diggs set ");
            strSql3.Append("digg_good=@digg_good,");
            strSql3.Append("digg_bad=@digg_bad");
            strSql3.Append(" where id=@id ");
            SqlParameter[] parameters3 = {
					            new SqlParameter("@digg_good", SqlDbType.Int,4),
					            new SqlParameter("@digg_bad", SqlDbType.Int,4),
                                new SqlParameter("@id", SqlDbType.Int,4)};
            parameters3[0].Value = model.digg_good;
            parameters3[1].Value = model.digg_bad;
            parameters3[2].Value = model.id;
            cmd = new CommandInfo(strSql3.ToString(), parameters3);
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
        public Model.article_content GetContentModel(int id)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,channel_id,category_id,title,link_url,img_url,seo_title,seo_keywords,seo_description,content,sort_id,click,is_lock,user_id,add_time,call_index,is_msg,is_red,digg_good,digg_bad from view_article_content ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

			Model.article_content model=new Model.article_content();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["channel_id"] != null && ds.Tables[0].Rows[0]["channel_id"].ToString() != "")
                {
                    model.channel_id = int.Parse(ds.Tables[0].Rows[0]["channel_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["category_id"] != null && ds.Tables[0].Rows[0]["category_id"].ToString() != "")
                {
                    model.category_id = int.Parse(ds.Tables[0].Rows[0]["category_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["title"] != null && ds.Tables[0].Rows[0]["title"].ToString() != "")
                {
                    model.title = ds.Tables[0].Rows[0]["title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["link_url"] != null && ds.Tables[0].Rows[0]["link_url"].ToString() != "")
                {
                    model.link_url = ds.Tables[0].Rows[0]["link_url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["img_url"] != null && ds.Tables[0].Rows[0]["img_url"].ToString() != "")
                {
                    model.img_url = ds.Tables[0].Rows[0]["img_url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["seo_title"] != null && ds.Tables[0].Rows[0]["seo_title"].ToString() != "")
                {
                    model.seo_title = ds.Tables[0].Rows[0]["seo_title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["seo_keywords"] != null && ds.Tables[0].Rows[0]["seo_keywords"].ToString() != "")
                {
                    model.seo_keywords = ds.Tables[0].Rows[0]["seo_keywords"].ToString();
                }
                if (ds.Tables[0].Rows[0]["seo_description"] != null && ds.Tables[0].Rows[0]["seo_description"].ToString() != "")
                {
                    model.seo_description = ds.Tables[0].Rows[0]["seo_description"].ToString();
                }
                if (ds.Tables[0].Rows[0]["content"] != null && ds.Tables[0].Rows[0]["content"].ToString() != "")
                {
                    model.content = ds.Tables[0].Rows[0]["content"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sort_id"] != null && ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["click"] != null && ds.Tables[0].Rows[0]["click"].ToString() != "")
                {
                    model.click = int.Parse(ds.Tables[0].Rows[0]["click"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_lock"] != null && ds.Tables[0].Rows[0]["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(ds.Tables[0].Rows[0]["is_lock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_id"] != null && ds.Tables[0].Rows[0]["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(ds.Tables[0].Rows[0]["user_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"] != null && ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["call_index"] != null && ds.Tables[0].Rows[0]["call_index"].ToString() != "")
                {
                    model.call_index = ds.Tables[0].Rows[0]["call_index"].ToString();
                }
                if (ds.Tables[0].Rows[0]["is_msg"].ToString() != "")
                {
                    model.is_msg = int.Parse(ds.Tables[0].Rows[0]["is_msg"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_red"].ToString() != "")
                {
                    model.is_red = int.Parse(ds.Tables[0].Rows[0]["is_red"].ToString());
                }
                if (ds.Tables[0].Rows[0]["digg_good"].ToString() != "")
                {
                    model.digg_good = int.Parse(ds.Tables[0].Rows[0]["digg_good"].ToString());
                }
                if (ds.Tables[0].Rows[0]["digg_bad"].ToString() != "")
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_content GetContentModel(string call_index)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from view_article_content");
            strSql.Append(" where call_index=@call_index");
            SqlParameter[] parameters = {
					new SqlParameter("@call_index", SqlDbType.NVarChar,50)};
            parameters[0].Value = call_index;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetContentModel(Convert.ToInt32(obj));
            }
            return null;
        }

		/// <summary>
		/// 获得前几行数据
		/// </summary>
        public DataSet GetContentList(int Top, string strWhere, string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
            strSql.Append(" id,channel_id,category_id,title,link_url,img_url,seo_title,seo_keywords,seo_description,content,sort_id,click,is_lock,user_id,add_time,call_index,is_msg,is_red,digg_good,digg_bad ");
            strSql.Append(" FROM view_article_content ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetContentList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM view_article_content");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(HotoPagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(HotoPagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

		#endregion  Method
	}
}

