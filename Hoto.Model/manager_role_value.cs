using System;
namespace Hoto.Model
{
    /// <summary>
    /// 角色权限:实体类
    /// </summary>
    [Serializable]
    public partial class manager_role_value
    {
        public manager_role_value()
        { }
        #region Model
        private int _id;
        private int _role_id;
        private string _channel_name;
        private int _channel_id = 0;
        private string _action_type;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int role_id
        {
            set { _role_id = value; }
            get { return _role_id; }
        }
        /// <summary>
        /// 频道名称
        /// </summary>
        public string channel_name
        {
            set { _channel_name = value; }
            get { return _channel_name; }
        }
        /// <summary>
        /// 频道ID
        /// </summary>
        public int channel_id
        {
            set { _channel_id = value; }
            get { return _channel_id; }
        }
        /// <summary>
        /// 操作类型
        /// </summary>
        public string action_type
        {
            set { _action_type = value; }
            get { return _action_type; }
        }
        #endregion Model

    }
}