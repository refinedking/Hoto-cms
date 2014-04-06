using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Hoto.Common;

namespace Hoto.BLL
{
    public class url_rewrite
    {
        private readonly DAL.url_rewrite dal = new DAL.url_rewrite();

        /// <summary>
        /// 增加节点
        /// </summary>
        public bool Add(Model.url_rewrite model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 修改节点
        /// </summary>
        public bool Edit(Model.url_rewrite model)
        {
            return dal.Edit(model);
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        public bool Remove(string attrName, string attrValue)
        {
            return dal.Remove(attrName, attrValue);
        }

        /// <summary>
        /// 批量删除节点
        /// </summary>
        public bool Remove(XmlNodeList xnList)
        {
            return dal.Remove(xnList);
        }

        /// <summary>
        /// 导入节点
        /// </summary>
        public bool Import(XmlNodeList xnList)
        {
            return dal.Import(xnList);
        }

        /// <summary>
        /// 取得节点配制信息
        /// </summary>
        public Model.url_rewrite GetInfo(string attrValue)
        {
            return dal.GetInfo(attrValue);
        }

        /// <summary>
        /// 取得节点配制信息
        /// </summary>
        public Model.url_rewrite GetInfo(string channel, string type)
        {
            Hashtable ht = GetList();
            foreach (DictionaryEntry item in ht)
            {
                Model.url_rewrite model = item.Value as Model.url_rewrite;
                if (model != null)
                {
                    if (model.channel == channel && model.type == type)
                    {
                        return model;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 返回URL映射列表
        /// </summary>
        public Hashtable GetList()
        {
            Hashtable ht = HotoCacheHelper.Get<Hashtable>(HotoKeys.CACHE_SITE_URLS);
            if (ht == null)
            {
                HotoCacheHelper.Insert(HotoKeys.CACHE_SITE_URLS, dal.GetList(), HotoUtils.GetXmlMapPath(HotoKeys.FILE_URL_XML_CONFING));
                ht = HotoCacheHelper.Get<Hashtable>(HotoKeys.CACHE_SITE_URLS);
            }
            return ht;
        }

        /// <summary>
        /// 返回URL映射列表
        /// </summary>
        public List<Model.url_rewrite> GetList(string channel)
        {
            return dal.GetList(channel);
        }
    }
}
