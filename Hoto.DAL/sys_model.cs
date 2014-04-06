using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using Hoto.DBUtility;
using Hoto.Common;

namespace Hoto.DAL
{
	/// <summary>
	/// 系统模型
	/// </summary>
    public partial class sys_model
    {
        public sys_model()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_sys_model");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据,及其子表数据
        /// </summary>
        public int Add(Model.sys_model model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_sys_model(");
            strSql.Append("title,sort_id,inherit_index,inherit_list,inherit_detail,is_sys)");
            strSql.Append(" values (");
            strSql.Append("@title,@sort_id,@inherit_index,@inherit_list,@inherit_detail,@is_sys)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@inherit_index", SqlDbType.NVarChar,255),
					new SqlParameter("@inherit_list", SqlDbType.NVarChar,255),
					new SqlParameter("@inherit_detail", SqlDbType.NVarChar,255),
					new SqlParameter("@is_sys", SqlDbType.TinyInt,1),
                    new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.sort_id;
            parameters[2].Value = model.inherit_index;
            parameters[3].Value = model.inherit_list;
            parameters[4].Value = model.inherit_detail;
            parameters[5].Value = model.is_sys;
            parameters[6].Direction = ParameterDirection.Output;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //模型菜单
            if (model.sys_model_navs != null)
            {
                StringBuilder strSql2;
                foreach (Model.sys_model_nav models in model.sys_model_navs)
                {
                    strSql2 = new StringBuilder();
                    strSql2.Append("insert into dt_sys_model_nav(");
                    strSql2.Append("model_id,title,nav_url,sort_id)");
                    strSql2.Append(" values (");
                    strSql2.Append("@model_id,@title,@nav_url,@sort_id)");
                    SqlParameter[] parameters2 = {
						new SqlParameter("@model_id", SqlDbType.Int,4),
						new SqlParameter("@title", SqlDbType.NVarChar,100),
						new SqlParameter("@nav_url", SqlDbType.NVarChar,255),
						new SqlParameter("@sort_id", SqlDbType.Int,4)};
                    parameters2[0].Direction = ParameterDirection.InputOutput;
                    parameters2[1].Value = models.title;
                    parameters2[2].Value = models.nav_url;
                    parameters2[3].Value = models.sort_id;

                    cmd = new CommandInfo(strSql2.ToString(), parameters2);
                    sqllist.Add(cmd);
                }
            }
            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[6].Value;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.sys_model model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update dt_sys_model set ");
                        strSql.Append("title=@title,");
                        strSql.Append("sort_id=@sort_id,");
                        strSql.Append("inherit_index=@inherit_index,");
                        strSql.Append("inherit_list=@inherit_list,");
                        strSql.Append("inherit_detail=@inherit_detail,");
                        strSql.Append("is_sys=@is_sys");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					            new SqlParameter("@title", SqlDbType.NVarChar,100),
					            new SqlParameter("@sort_id", SqlDbType.Int,4),
					            new SqlParameter("@inherit_index", SqlDbType.NVarChar,255),
					            new SqlParameter("@inherit_list", SqlDbType.NVarChar,255),
					            new SqlParameter("@inherit_detail", SqlDbType.NVarChar,255),
					            new SqlParameter("@is_sys", SqlDbType.TinyInt,1),
					            new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters[0].Value = model.title;
                        parameters[1].Value = model.sort_id;
                        parameters[2].Value = model.inherit_index;
                        parameters[3].Value = model.inherit_list;
                        parameters[4].Value = model.inherit_detail;
                        parameters[5].Value = model.is_sys;
                        parameters[6].Value = model.id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);

                        //删除已删除的菜单
                        new DAL.sys_model_nav().DeleteList(conn, trans, model.sys_model_navs, model.id);

                        //添加/修改菜单
                        if (model.sys_model_navs != null)
                        {
                            StringBuilder strSql2;
                            foreach (Model.sys_model_nav models in model.sys_model_navs)
                            {
                                strSql2 = new StringBuilder();
                                if (models.id > 0)
                                {
                                    strSql2.Append("update dt_sys_model_nav set ");
                                    strSql2.Append("model_id=@model_id,");
                                    strSql2.Append("title=@title,");
                                    strSql2.Append("nav_url=@nav_url,");
                                    strSql2.Append("sort_id=@sort_id");
                                    strSql2.Append(" where id=@id");
                                    SqlParameter[] parameters2 = {
                                            new SqlParameter("@model_id", SqlDbType.Int,4),
					                        new SqlParameter("@title", SqlDbType.NVarChar,100),
					                        new SqlParameter("@nav_url", SqlDbType.NVarChar,255),
					                        new SqlParameter("@sort_id", SqlDbType.Int,4),
					                        new SqlParameter("@id", SqlDbType.Int,4)};
                                    parameters2[0].Value = models.model_id;
                                    parameters2[1].Value = models.title;
                                    parameters2[2].Value = models.nav_url;
                                    parameters2[3].Value = models.sort_id;
                                    parameters2[4].Value = models.id;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
                                }
                                else
                                {
                                    strSql2.Append("insert into dt_sys_model_nav(");
                                    strSql2.Append("model_id,title,nav_url,sort_id)");
                                    strSql2.Append(" values (");
                                    strSql2.Append("@model_id,@title,@nav_url,@sort_id)");
                                    SqlParameter[] parameters2 = {
					                        new SqlParameter("@model_id", SqlDbType.Int,4),
					                        new SqlParameter("@title", SqlDbType.NVarChar,100),
					                        new SqlParameter("@nav_url", SqlDbType.NVarChar,255),
					                        new SqlParameter("@sort_id", SqlDbType.Int,4)};
                                    parameters2[0].Value = models.model_id;
                                    parameters2[1].Value = models.title;
                                    parameters2[2].Value = models.nav_url;
                                    parameters2[3].Value = models.sort_id;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
                                }
                            }
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 删除一条数据，及子表所有相关数据
        /// </summary>
        public bool Delete(int id)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from dt_sys_model_nav ");
            strSql2.Append(" where model_id=@model_id ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@model_id", SqlDbType.Int,4)};
            parameters2[0].Value = id;

            CommandInfo cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_sys_model ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            cmd = new CommandInfo(strSql.ToString(), parameters);
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
        public Model.sys_model GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,title,sort_id,inherit_index,inherit_list,inherit_detail,is_sys from dt_sys_model ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.sys_model model = new Model.sys_model();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region  父表信息
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["title"] != null && ds.Tables[0].Rows[0]["title"].ToString() != "")
                {
                    model.title = ds.Tables[0].Rows[0]["title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sort_id"] != null && ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["inherit_index"] != null && ds.Tables[0].Rows[0]["inherit_index"].ToString() != "")
                {
                    model.inherit_index = ds.Tables[0].Rows[0]["inherit_index"].ToString();
                }
                if (ds.Tables[0].Rows[0]["inherit_list"] != null && ds.Tables[0].Rows[0]["inherit_list"].ToString() != "")
                {
                    model.inherit_list = ds.Tables[0].Rows[0]["inherit_list"].ToString();
                }
                if (ds.Tables[0].Rows[0]["inherit_detail"] != null && ds.Tables[0].Rows[0]["inherit_detail"].ToString() != "")
                {
                    model.inherit_detail = ds.Tables[0].Rows[0]["inherit_detail"].ToString();
                }
                if (ds.Tables[0].Rows[0]["is_sys"] != null && ds.Tables[0].Rows[0]["is_sys"].ToString() != "")
                {
                    model.is_sys = int.Parse(ds.Tables[0].Rows[0]["is_sys"].ToString());
                }
                #endregion  父表信息end

                #region  子表信息
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select id,model_id,title,nav_url,sort_id from dt_sys_model_nav ");
                strSql2.Append(" where model_id=@model_id ");
                SqlParameter[] parameters2 = {
					new SqlParameter("@model_id", SqlDbType.Int,4)};
                parameters2[0].Value = id;

                DataSet ds2 = DbHelperSQL.Query(strSql2.ToString(), parameters2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    #region  子表字段信息
                    int i = ds2.Tables[0].Rows.Count;
                    List<Model.sys_model_nav> models = new List<Model.sys_model_nav>();
                    Model.sys_model_nav modelt;
                    for (int n = 0; n < i; n++)
                    {
                        modelt = new Model.sys_model_nav();
                        if (ds2.Tables[0].Rows[n]["id"] != null && ds2.Tables[0].Rows[n]["id"].ToString() != "")
                        {
                            modelt.id = int.Parse(ds2.Tables[0].Rows[n]["id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["model_id"] != null && ds2.Tables[0].Rows[n]["model_id"].ToString() != "")
                        {
                            modelt.model_id = int.Parse(ds2.Tables[0].Rows[n]["model_id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["title"] != null && ds2.Tables[0].Rows[n]["title"].ToString() != "")
                        {
                            modelt.title = ds2.Tables[0].Rows[n]["title"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["nav_url"] != null && ds2.Tables[0].Rows[n]["nav_url"].ToString() != "")
                        {
                            modelt.nav_url = ds2.Tables[0].Rows[n]["nav_url"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["sort_id"] != null && ds2.Tables[0].Rows[n]["sort_id"].ToString() != "")
                        {
                            modelt.sort_id = int.Parse(ds2.Tables[0].Rows[n]["sort_id"].ToString());
                        }
                        models.Add(modelt);
                    }
                    model.sys_model_navs = models;
                    #endregion  子表字段信息end
                }
                #endregion  子表信息end

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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,title,sort_id,inherit_index,inherit_list,inherit_detail,is_sys ");
            strSql.Append(" FROM dt_sys_model ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by sort_id asc,id desc");
            return DbHelperSQL.Query(strSql.ToString());
        }


        #endregion  Method
    }

    /// <summary>
    /// 系统模型菜单=============================================================
    /// </summary>
    public partial class sys_model_nav
    {
        public sys_model_nav()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_sys_model_nav");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,model_id,title,nav_url,sort_id ");
            strSql.Append(" FROM dt_sys_model_nav ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 查找不存在的菜单并删除
        /// </summary>
        public void DeleteList(SqlConnection conn, SqlTransaction trans, List<Model.sys_model_nav> models, int model_id)
        {
            StringBuilder idList = new StringBuilder();
            if (models != null)
            {
                foreach (Model.sys_model_nav modelt in models)
                {
                    if (modelt.id > 0)
                    {
                        idList.Append(modelt.id + ",");
                    }
                }
            }
            string id_list = HotoUtils.DelLastChar(idList.ToString(), ",");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_sys_model_nav where model_id=" + model_id);
            if (!string.IsNullOrEmpty(id_list))
            {
                strSql.Append(" and id not in(" + id_list + ")");
            }
            DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString());
        }

        #endregion  Method
    }
}

