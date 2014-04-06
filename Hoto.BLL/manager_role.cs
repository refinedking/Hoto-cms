using System;
using System.Data;
using System.Collections.Generic;

namespace Hoto.BLL
{
    /// <summary>
    /// 管理角色
    /// </summary>
    public partial class manager_role
    {
        private readonly Hoto.DAL.manager_role dal = new Hoto.DAL.manager_role();
        public manager_role()
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
        /// 返回角色名称
        /// </summary>
        public string GetTitle(int id)
        {
            return dal.GetTitle(id);
        }

        /// <summary>
        /// 检查是否有权限
        /// </summary>
        public bool Exists(int role_id, int channel_id, string action_type)
        {
            Model.manager_role model = GetModel(role_id);
            if (model != null)
            {
                if (model.role_type == 1)
                {
                    return true;
                }
                Model.manager_role_value modelt = model.manager_role_values.Find(p => p.channel_id == channel_id && p.action_type == action_type);
                if (modelt != null)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 检查是否有权限
        /// </summary>
        public bool Exists(int role_id, string channel_name, string action_type)
        {
            Model.manager_role model = GetModel(role_id);
            if (model != null)
            {
                if (model.role_type == 1)
                {
                    return true;
                }
                Model.manager_role_value modelt = model.manager_role_values.Find(p => p.channel_name == channel_name && p.action_type == action_type);
                if (modelt != null)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Hoto.Model.manager_role model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Hoto.Model.manager_role model)
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Hoto.Model.manager_role GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        #endregion  Method
    }
}