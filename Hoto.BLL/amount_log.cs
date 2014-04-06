using System;
using System.Data;
using System.Collections.Generic;

namespace Hoto.BLL
{
    /// <summary>
    /// 预存款记录日志
    /// </summary>
    public partial class amount_log
    {
        private readonly DAL.amount_log dal = new DAL.amount_log();
        public amount_log()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(int user_id, string user_name, string type, decimal value, string remark)
        {
            Model.amount_log model = new Model.amount_log();
            model.user_id = user_id;
            model.user_name = user_name;
            model.type = type;
            model.value = value;
            model.remark = remark;
            return dal.Add(model);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(int user_id, string user_name, string type, decimal value, string remark, int status)
        {
            Model.amount_log model = new Model.amount_log();
            model.user_id = user_id;
            model.user_name = user_name;
            model.type = type;
            model.value = value;
            model.remark = remark;
            model.status = status;
            model.complete_time = DateTime.Now;
            return dal.Add(model);
        }

        /// <summary>
        /// 增加一条数据(带订单号)
        /// </summary>
        public int Add(int user_id, string user_name, string type, string order_no, int payment_id, decimal value, string remark, int status)
        {
            Model.amount_log model = new Model.amount_log();
            model.user_id = user_id;
            model.user_name = user_name;
            model.type = type;
            model.order_no = order_no;
            model.payment_id = payment_id;
            model.value = value;
            model.remark = remark;
            model.status = status;
            model.complete_time = DateTime.Now;
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.amount_log model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }
        public bool Delete(int id, string user_name)
        {
            return dal.Delete(id, user_name);
        }
        
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.amount_log GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 根据订单号返回一个实体
        /// </summary>
        public Model.amount_log GetModel(string order_no)
        {
            return dal.GetModel(order_no);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion  Method
    }
}