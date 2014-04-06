using System;
using System.Collections.Generic;
using System.Text;

namespace Hoto.BLL
{
    public partial class plugin
    {
        private readonly DAL.plugin dal = new DAL.plugin();

        public plugin() { }

        /// <summary>
        /// 返回插件列表
        /// </summary>
        /// <param name="dirPath">站点下的插件路径(物理路径)</param>
        public List<Model.plugin> GetList(string dirPath)
        {
            return dal.GetList(dirPath);
        }

        /// <summary>
        /// 返回插件说明信息
        /// </summary>
        /// <param name="dirPath">插件目录路径(不包含文件名)</param>
        /// <returns>插件配置信息</returns>
        public Model.plugin GetInfo(string dirPath)
        {
            return dal.GetInfo(dirPath);
        }

        /// <summary>
        /// 修改插件节点数据
        /// </summary>
        /// <param name="dirPath">插件目录路径</param>
        /// <param name="node">节点</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool UpdateNodeValue(string dirPath, string node, string value)
        {
            return dal.UpdateNodeValue(dirPath, node, value);
        }

        /// <summary>
        /// 执行插件SQL语句
        /// </summary>
        /// <param name="dirPath">插件目录路径</param>
        /// <param name="xPath">节点</param>
        public bool ExeSqlStr(string dirPath, string xPath)
        {
            return dal.ExeSqlStr(dirPath, xPath);
        }

        /// <summary>
        /// 添加URL映射节点
        /// </summary>
        /// <param name="dirPath">插件目录路径</param>
        /// <param name="xPath">节点</param>
        public bool AppendNodes(string dirPath, string xPath)
        {
            return dal.AppendNodes(dirPath, xPath);
        }

        /// <summary>
        /// 删除URL映射节点
        /// </summary>
        /// <param name="dirPath">插件目录路径</param>
        /// <param name="xPath">节点</param>
        public bool RemoveNodes(string dirPath, string xPath)
        {
            return dal.RemoveNodes(dirPath, xPath);
        }

    }
}
