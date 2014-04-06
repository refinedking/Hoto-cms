using System;
using System.Data;
using System.Collections.Generic;
using Hoto.Common;

namespace Hoto.BLL
{
    /// <summary>
    /// 下载附件
    /// </summary>
    public partial class download_attach
    {
        private readonly DAL.download_attach dal = new DAL.download_attach();
        public download_attach()
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
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.download_attach GetModel(int id)
        {
            return dal.GetModel(id);
        }

        //删除更新的旧文件
        public void DeleteFile(int id, string filePath)
        {
            Model.download_attach model = GetModel(id);
            if (model != null && model.file_path != filePath)
            {
                HotoUtils.DeleteFile(model.file_path);
            }
        }

        #endregion  Method

    }
}