using System;
using System.Data;
using System.Collections.Generic;

namespace Hoto.BLL
{
    /// <summary>
    /// 积分记录日志
    /// </summary>
    public partial class point_log
    {
        private readonly DAL.point_log dal = new DAL.point_log();
        public point_log()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        public int Add(int user_id, string user_name, int value, string remark)
        {
            Model.point_log model = new Model.point_log();
            model.user_id = user_id;
            model.user_name = user_name;
            model.value = value;
            model.remark = remark;
            int result = dal.Add(model);
            if (value > 0 && result > 0)
            {
                new BLL.users().Upgrade(user_id);
            }
            return result;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.point_log GetModel(int id)
        {
            return dal.GetModel(id);
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