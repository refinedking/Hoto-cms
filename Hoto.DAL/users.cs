using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using Hoto.DBUtility;
using Hoto.Common;

namespace Hoto.DAL
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public partial class users
    {
        public users()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_users");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        public bool Exists(string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_users");
            strSql.Append(" where user_name=@user_name ");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 检查同一IP注册间隔(小时)内是否存在
        /// </summary>
        public bool Exists(string reg_ip, int regctrl)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_users");
            strSql.Append(" where reg_ip=@reg_ip and DATEDIFF(hh,reg_time,getdate())<@regctrl ");
            SqlParameter[] parameters = {
					new SqlParameter("@reg_ip", SqlDbType.NVarChar,30),
                    new SqlParameter("@regctrl", SqlDbType.Int,4)};
            parameters[0].Value = reg_ip;
            parameters[1].Value = regctrl;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 检查Email是否存在
        /// </summary>
        public bool ExistsEmail(string email)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from dt_users");
            strSql.Append(" where email=@email ");
            SqlParameter[] parameters = {
					new SqlParameter("@email", SqlDbType.NVarChar,100)};
            parameters[0].Value = email;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into dt_users(");
            strSql.Append("group_id,user_name,password,email,nick_name,avatar,sex,birthday,telphone,mobile,qq,address,safe_question,safe_answer,amount,point,exp,is_lock,reg_time,reg_ip)");
            strSql.Append(" values (");
            strSql.Append("@group_id,@user_name,@password,@email,@nick_name,@avatar,@sex,@birthday,@telphone,@mobile,@qq,@address,@safe_question,@safe_answer,@amount,@point,@exp,@is_lock,@reg_time,@reg_ip)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@group_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@password", SqlDbType.NVarChar,100),
					new SqlParameter("@email", SqlDbType.NVarChar,50),
					new SqlParameter("@nick_name", SqlDbType.NVarChar,100),
					new SqlParameter("@avatar", SqlDbType.NVarChar,255),
					new SqlParameter("@sex", SqlDbType.NVarChar,20),
					new SqlParameter("@birthday", SqlDbType.DateTime),
					new SqlParameter("@telphone", SqlDbType.NVarChar,50),
					new SqlParameter("@mobile", SqlDbType.NVarChar,20),
					new SqlParameter("@qq", SqlDbType.NVarChar,30),
					new SqlParameter("@address", SqlDbType.NVarChar,255),
					new SqlParameter("@safe_question", SqlDbType.NVarChar,255),
					new SqlParameter("@safe_answer", SqlDbType.NVarChar,255),
					new SqlParameter("@amount", SqlDbType.Decimal,5),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@exp", SqlDbType.Int,4),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					new SqlParameter("@reg_time", SqlDbType.DateTime),
					new SqlParameter("@reg_ip", SqlDbType.NVarChar,30)};
            parameters[0].Value = model.group_id;
            parameters[1].Value = model.user_name;
            parameters[2].Value = model.password;
            parameters[3].Value = model.email;
            parameters[4].Value = model.nick_name;
            parameters[5].Value = model.avatar;
            parameters[6].Value = model.sex;
            parameters[7].Value = model.birthday;
            parameters[8].Value = model.telphone;
            parameters[9].Value = model.mobile;
            parameters[10].Value = model.qq;
            parameters[11].Value = model.address;
            parameters[12].Value = model.safe_question;
            parameters[13].Value = model.safe_answer;
            parameters[14].Value = model.amount;
            parameters[15].Value = model.point;
            parameters[16].Value = model.exp;
            parameters[17].Value = model.is_lock;
            parameters[18].Value = model.reg_time;
            parameters[19].Value = model.reg_ip;

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
        /// 修改一列数据
        /// </summary>
        public int UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_users set " + strValue);
            strSql.Append(" where id=" + id);
            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dt_users set ");
            strSql.Append("group_id=@group_id,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("password=@password,");
            strSql.Append("email=@email,");
            strSql.Append("nick_name=@nick_name,");
            strSql.Append("avatar=@avatar,");
            strSql.Append("sex=@sex,");
            strSql.Append("birthday=@birthday,");
            strSql.Append("telphone=@telphone,");
            strSql.Append("mobile=@mobile,");
            strSql.Append("qq=@qq,");
            strSql.Append("address=@address,");
            strSql.Append("safe_question=@safe_question,");
            strSql.Append("safe_answer=@safe_answer,");
            strSql.Append("amount=@amount,");
            strSql.Append("point=@point,");
            strSql.Append("exp=@exp,");
            strSql.Append("is_lock=@is_lock,");
            strSql.Append("reg_time=@reg_time,");
            strSql.Append("reg_ip=@reg_ip");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@group_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@password", SqlDbType.NVarChar,100),
					new SqlParameter("@email", SqlDbType.NVarChar,50),
					new SqlParameter("@nick_name", SqlDbType.NVarChar,100),
					new SqlParameter("@avatar", SqlDbType.NVarChar,255),
					new SqlParameter("@sex", SqlDbType.NVarChar,20),
					new SqlParameter("@birthday", SqlDbType.DateTime),
					new SqlParameter("@telphone", SqlDbType.NVarChar,50),
					new SqlParameter("@mobile", SqlDbType.NVarChar,20),
					new SqlParameter("@qq", SqlDbType.NVarChar,30),
					new SqlParameter("@address", SqlDbType.NVarChar,255),
					new SqlParameter("@safe_question", SqlDbType.NVarChar,255),
					new SqlParameter("@safe_answer", SqlDbType.NVarChar,255),
					new SqlParameter("@amount", SqlDbType.Decimal,5),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@exp", SqlDbType.Int,4),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					new SqlParameter("@reg_time", SqlDbType.DateTime),
					new SqlParameter("@reg_ip", SqlDbType.NVarChar,30),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.group_id;
            parameters[1].Value = model.user_name;
            parameters[2].Value = model.password;
            parameters[3].Value = model.email;
            parameters[4].Value = model.nick_name;
            parameters[5].Value = model.avatar;
            parameters[6].Value = model.sex;
            parameters[7].Value = model.birthday;
            parameters[8].Value = model.telphone;
            parameters[9].Value = model.mobile;
            parameters[10].Value = model.qq;
            parameters[11].Value = model.address;
            parameters[12].Value = model.safe_question;
            parameters[13].Value = model.safe_answer;
            parameters[14].Value = model.amount;
            parameters[15].Value = model.point;
            parameters[16].Value = model.exp;
            parameters[17].Value = model.is_lock;
            parameters[18].Value = model.reg_time;
            parameters[19].Value = model.reg_ip;
            parameters[20].Value = model.id;

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
            //先取得用户Model
            Model.users model = GetModel(id);
            if (model == null)
            {
                return false;
            }
            List<CommandInfo> sqllist = new List<CommandInfo>();
            //删除登录日志
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from dt_user_login_log ");
            strSql.Append(" where user_id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //删除申请码
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from dt_user_code ");
            strSql1.Append(" where user_id=@id");
            SqlParameter[] parameters1 = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters1[0].Value = id;
            cmd = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd);

            //删除积分记录
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from dt_point_log ");
            strSql2.Append(" where user_id=@id");
            SqlParameter[] parameters2 = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters2[0].Value = id;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            //删除金额记录
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete from dt_amount_log ");
            strSql3.Append(" where user_id=@id");
            SqlParameter[] parameters3 = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters3[0].Value = id;
            cmd = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd);

            //删除短消息
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("delete from dt_user_message ");
            strSql4.Append(" where post_user_name=@post_user_name or accept_user_name=@accept_user_name");
            SqlParameter[] parameters4 = {
					new SqlParameter("@post_user_name", SqlDbType.NVarChar,100),
                    new SqlParameter("@accept_user_name", SqlDbType.NVarChar,100)};
            parameters4[0].Value = model.user_name;
            parameters4[1].Value = model.user_name;
            cmd = new CommandInfo(strSql4.ToString(), parameters4);
            sqllist.Add(cmd);

            //删除用户资料
            StringBuilder strSql5 = new StringBuilder();
            strSql5.Append("delete from dt_users ");
            strSql5.Append(" where id=@id");
            SqlParameter[] parameters5 = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters5[0].Value = id;
            cmd = new CommandInfo(strSql5.ToString(), parameters5);
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
        public Model.users GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,group_id,user_name,password,email,nick_name,avatar,sex,birthday,telphone,mobile,qq,address,safe_question,safe_answer,amount,point,exp,is_lock,reg_time,reg_ip from dt_users ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.users model = new Model.users();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["group_id"] != null && ds.Tables[0].Rows[0]["group_id"].ToString() != "")
                {
                    model.group_id = int.Parse(ds.Tables[0].Rows[0]["group_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["user_name"] != null && ds.Tables[0].Rows[0]["user_name"].ToString() != "")
                {
                    model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["password"] != null && ds.Tables[0].Rows[0]["password"].ToString() != "")
                {
                    model.password = ds.Tables[0].Rows[0]["password"].ToString();
                }
                if (ds.Tables[0].Rows[0]["email"] != null && ds.Tables[0].Rows[0]["email"].ToString() != "")
                {
                    model.email = ds.Tables[0].Rows[0]["email"].ToString();
                }
                if (ds.Tables[0].Rows[0]["nick_name"] != null && ds.Tables[0].Rows[0]["nick_name"].ToString() != "")
                {
                    model.nick_name = ds.Tables[0].Rows[0]["nick_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["avatar"] != null && ds.Tables[0].Rows[0]["avatar"].ToString() != "")
                {
                    model.avatar = ds.Tables[0].Rows[0]["avatar"].ToString();
                }
                if (ds.Tables[0].Rows[0]["sex"] != null && ds.Tables[0].Rows[0]["sex"].ToString() != "")
                {
                    model.sex = ds.Tables[0].Rows[0]["sex"].ToString();
                }
                if (ds.Tables[0].Rows[0]["birthday"] != null && ds.Tables[0].Rows[0]["birthday"].ToString() != "")
                {
                    model.birthday = DateTime.Parse(ds.Tables[0].Rows[0]["birthday"].ToString());
                }
                if (ds.Tables[0].Rows[0]["telphone"] != null && ds.Tables[0].Rows[0]["telphone"].ToString() != "")
                {
                    model.telphone = ds.Tables[0].Rows[0]["telphone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["mobile"] != null && ds.Tables[0].Rows[0]["mobile"].ToString() != "")
                {
                    model.mobile = ds.Tables[0].Rows[0]["mobile"].ToString();
                }
                if (ds.Tables[0].Rows[0]["qq"] != null && ds.Tables[0].Rows[0]["qq"].ToString() != "")
                {
                    model.qq = ds.Tables[0].Rows[0]["qq"].ToString();
                }
                if (ds.Tables[0].Rows[0]["address"] != null && ds.Tables[0].Rows[0]["address"].ToString() != "")
                {
                    model.address = ds.Tables[0].Rows[0]["address"].ToString();
                }
                if (ds.Tables[0].Rows[0]["safe_question"] != null && ds.Tables[0].Rows[0]["safe_question"].ToString() != "")
                {
                    model.safe_question = ds.Tables[0].Rows[0]["safe_question"].ToString();
                }
                if (ds.Tables[0].Rows[0]["safe_answer"] != null && ds.Tables[0].Rows[0]["safe_answer"].ToString() != "")
                {
                    model.safe_answer = ds.Tables[0].Rows[0]["safe_answer"].ToString();
                }
                if (ds.Tables[0].Rows[0]["amount"] != null && ds.Tables[0].Rows[0]["amount"].ToString() != "")
                {
                    model.amount = decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["point"] != null && ds.Tables[0].Rows[0]["point"].ToString() != "")
                {
                    model.point = int.Parse(ds.Tables[0].Rows[0]["point"].ToString());
                }
                if (ds.Tables[0].Rows[0]["exp"] != null && ds.Tables[0].Rows[0]["exp"].ToString() != "")
                {
                    model.exp = int.Parse(ds.Tables[0].Rows[0]["exp"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_lock"] != null && ds.Tables[0].Rows[0]["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(ds.Tables[0].Rows[0]["is_lock"].ToString());
                }
                if (ds.Tables[0].Rows[0]["reg_time"] != null && ds.Tables[0].Rows[0]["reg_time"].ToString() != "")
                {
                    model.reg_time = DateTime.Parse(ds.Tables[0].Rows[0]["reg_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["reg_ip"] != null && ds.Tables[0].Rows[0]["reg_ip"].ToString() != "")
                {
                    model.reg_ip = ds.Tables[0].Rows[0]["reg_ip"].ToString();
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public Model.users GetModel(string user_name, string password, int emaillogin)
        {
            StringBuilder strSql = new StringBuilder();
            if (emaillogin == 1)
            {
                strSql.Append("select id from dt_users");
                strSql.Append(" where (user_name=@user_name or email=@email) and password=@password and is_lock<3");
                SqlParameter[] parameters = {
					    new SqlParameter("@user_name", SqlDbType.NVarChar,100),
                        new SqlParameter("@email", SqlDbType.NVarChar,50),
                        new SqlParameter("@password", SqlDbType.NVarChar,100)};
                parameters[0].Value = user_name;
                parameters[1].Value = user_name;
                parameters[2].Value = password;
                object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
                if (obj != null)
                {
                    return GetModel(Convert.ToInt32(obj));
                }
            }
            else
            {
                strSql.Append("select id from dt_users");
                strSql.Append(" where user_name=@user_name and password=@password and is_lock<3");
                SqlParameter[] parameters = {
					    new SqlParameter("@user_name", SqlDbType.NVarChar,100),
                        new SqlParameter("@password", SqlDbType.NVarChar,100)};
                parameters[0].Value = user_name;
                parameters[1].Value = password;
                object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
                if (obj != null)
                {
                    return GetModel(Convert.ToInt32(obj));
                }
            }
            
            return null;
        }

        /// <summary>
        /// 根据用户名返回一个实体
        /// </summary>
        public Model.users GetModel(string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from dt_users");
            strSql.Append(" where user_name=@user_name and is_lock<3");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,group_id,user_name,password,email,nick_name,avatar,sex,birthday,telphone,mobile,qq,address,safe_question,safe_answer,amount,point,exp,is_lock,reg_time,reg_ip ");
            strSql.Append(" FROM dt_users ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM dt_users");
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