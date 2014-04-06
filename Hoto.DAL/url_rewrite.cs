using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Hoto.Common;

namespace Hoto.DAL
{
    public class url_rewrite
    {
        #region 增、删、改操作=================================================
        /// <summary>
        /// 增加节点
        /// </summary>
        public bool Add(Model.url_rewrite model)
        {
            try
            {
                string filePath = HotoUtils.GetXmlMapPath(HotoKeys.FILE_URL_XML_CONFING);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode("urls");
                XmlElement xe = doc.CreateElement("rewrite");
                if (!string.IsNullOrEmpty(model.name))
                    xe.SetAttribute("name", model.name);
                if (!string.IsNullOrEmpty(model.path))
                    xe.SetAttribute("path", model.path);
                if (!string.IsNullOrEmpty(model.pattern))
                    xe.SetAttribute("pattern", model.pattern);
                if (!string.IsNullOrEmpty(model.page))
                    xe.SetAttribute("page", model.page);
                if (!string.IsNullOrEmpty(model.querystring))
                    xe.SetAttribute("querystring", model.querystring);
                if (!string.IsNullOrEmpty(model.templet))
                    xe.SetAttribute("templet", model.templet);
                if (!string.IsNullOrEmpty(model.channel))
                    xe.SetAttribute("channel", model.channel.ToString());
                if (!string.IsNullOrEmpty(model.type))
                    xe.SetAttribute("type", model.type);
                if (!string.IsNullOrEmpty(model.inherit))
                    xe.SetAttribute("inherit", model.inherit);
                xn.AppendChild(xe);
                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 修改节点
        /// </summary>
        public bool Edit(Model.url_rewrite model)
        {
            string filePath = HotoUtils.GetXmlMapPath(HotoKeys.FILE_URL_XML_CONFING);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            XmlNodeList xnList = xn.ChildNodes;
            if (xnList.Count > 0)
            {
                foreach (XmlElement xe in xnList)
                {
                    if (xe.Attributes["name"].Value.ToLower() == model.name.ToLower())
                    {
                        if (!string.IsNullOrEmpty(model.path))
                            xe.SetAttribute("path", model.path);
                        else if (xe.Attributes["path"] != null)
                            xe.Attributes["path"].RemoveAll();
                        if (!string.IsNullOrEmpty(model.pattern))
                            xe.SetAttribute("pattern", model.pattern);
                        else if (xe.Attributes["pattern"] != null)
                            xe.Attributes["pattern"].RemoveAll();
                        if (!string.IsNullOrEmpty(model.page))
                            xe.SetAttribute("page", model.page);
                        else if (xe.Attributes["page"] != null)
                            xe.Attributes["page"].RemoveAll();
                        if (!string.IsNullOrEmpty(model.querystring))
                            xe.SetAttribute("querystring", model.querystring);
                        else if (xe.Attributes["querystring"] != null)
                            xe.Attributes["querystring"].RemoveAll();
                        if (!string.IsNullOrEmpty(model.templet))
                            xe.SetAttribute("templet", model.templet);
                        else if (xe.Attributes["templet"] != null)
                            xe.Attributes["templet"].RemoveAll();
                        if (!string.IsNullOrEmpty(model.channel))
                            xe.SetAttribute("channel", model.channel);
                        else if (xe.Attributes["channel"] != null)
                            xe.Attributes["channel"].RemoveAll();
                        if (!string.IsNullOrEmpty(model.type))
                            xe.SetAttribute("type", model.type);
                        else if (xe.Attributes["type"] != null)
                            xe.Attributes["type"].RemoveAll();
                        if (!string.IsNullOrEmpty(model.inherit))
                            xe.SetAttribute("inherit", model.inherit);
                        else if (xe.Attributes["inherit"] != null)
                            xe.Attributes["inherit"].RemoveAll();
                        doc.Save(filePath);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        public bool Remove(string attrName, string attrValue)
        {
            string filePath = HotoUtils.GetXmlMapPath(HotoKeys.FILE_URL_XML_CONFING);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            XmlNodeList xnList = xn.ChildNodes;
            if (xnList.Count > 0)
            {
                for (int i = xnList.Count - 1; i >= 0; i--)
                {
                    XmlElement xe = (XmlElement)xnList.Item(i);
                    if (xe.Attributes[attrName].Value.ToLower() == attrValue.ToLower())
                    {
                        xn.RemoveChild(xe);
                    }
                }
                doc.Save(filePath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 批量删除节点
        /// </summary>
        public bool Remove(XmlNodeList xnList)
        {
            try
            {
                string filePath = HotoUtils.GetXmlMapPath(HotoKeys.FILE_URL_XML_CONFING);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode("urls");
                foreach (XmlElement xe in xnList)
                {
                    for (int i = xn.ChildNodes.Count - 1; i >= 0; i--)
                    {
                        XmlElement xe2 = (XmlElement)xn.ChildNodes.Item(i);
                        if (xe2.Attributes["name"].Value.ToLower() == xe.Attributes["name"].Value.ToLower())
                        {
                            xn.RemoveChild(xe2);
                        }
                    }
                }
                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 导入节点
        /// </summary>
        public bool Import(XmlNodeList xnList)
        {
            try
            {
                string filePath = HotoUtils.GetXmlMapPath(HotoKeys.FILE_URL_XML_CONFING);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode("urls");
                foreach (XmlElement xe in xnList)
                {
                    if (xe.NodeType != XmlNodeType.Comment && xe.Name.ToLower() == "rewrite")
                    {
                        if (xe.Attributes["name"] != null)
                        {
                            XmlNode n = doc.ImportNode(xe, true);
                            xn.AppendChild(n);
                        }
                    }
                }
                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 扩展方法=====================================================
        /// <summary>
        /// 取得节点配制信息
        /// </summary>
        public Model.url_rewrite GetInfo(string attrValue)
        {
            Model.url_rewrite model = new Model.url_rewrite();
            string filePath = HotoUtils.GetXmlMapPath(HotoKeys.FILE_URL_XML_CONFING);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            XmlNodeList xnList = xn.ChildNodes;
            if (xnList.Count > 0)
            {
                foreach (XmlElement xe in xnList)
                {
                    if (xe.Attributes["name"].Value.ToLower() == attrValue.ToLower())
                    {
                        if (xe.Attributes["name"] != null)
                            model.name = xe.Attributes["name"].Value;
                        if (xe.Attributes["path"] != null)
                            model.path = xe.Attributes["path"].Value;
                        if (xe.Attributes["pattern"] != null)
                            model.pattern = xe.Attributes["pattern"].Value;
                        if (xe.Attributes["page"] != null)
                            model.page = xe.Attributes["page"].Value;
                        if (xe.Attributes["querystring"] != null)
                            model.querystring = xe.Attributes["querystring"].Value;
                        if (xe.Attributes["templet"] != null)
                            model.templet = xe.Attributes["templet"].Value;
                        if (xe.Attributes["channel"] != null)
                            model.channel = xe.Attributes["channel"].Value;
                        if (xe.Attributes["type"] != null)
                            model.type = xe.Attributes["type"].Value;
                        if (xe.Attributes["inherit"] != null)
                            model.inherit = xe.Attributes["inherit"].Value;
                        return model;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 取得URL配制列表
        /// </summary>
        public Hashtable GetList()
        {
            Hashtable ht = new Hashtable();
            List<Model.url_rewrite> ls = GetList("");
            foreach (Model.url_rewrite model in ls)
            {
                if (!ht.Contains(model.name))
                {
                    model.page = model.page.Replace("^", "&");
                    model.querystring = model.querystring.Replace("^", "&");
                    ht.Add(model.name, model);
                }
            }
            return ht;
        }

        /// <summary>
        /// 取得URL配制列表
        /// </summary>
        public List<Model.url_rewrite> GetList(string channel)
        {
            List<Model.url_rewrite> ls = new List<Model.url_rewrite>();
            string filePath = HotoUtils.GetXmlMapPath(HotoKeys.FILE_URL_XML_CONFING);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            foreach (XmlElement xe in xn.ChildNodes)
            {
                if (xe.NodeType != XmlNodeType.Comment && xe.Name.ToLower() == "rewrite")
                {
                    if (xe.Attributes["name"] != null)
                    {
                        if (!string.IsNullOrEmpty(channel))
                        {
                            if (channel.ToLower() == xe.Attributes["channel"].Value.ToLower())
                            {
                                Model.url_rewrite model = new Model.url_rewrite();
                                if (xe.Attributes["name"] != null)
                                    model.name = xe.Attributes["name"].Value;
                                if (xe.Attributes["path"] != null)
                                    model.path = xe.Attributes["path"].Value;
                                if (xe.Attributes["pattern"] != null)
                                    model.pattern = xe.Attributes["pattern"].Value;
                                if (xe.Attributes["page"] != null)
                                    model.page = xe.Attributes["page"].Value;
                                if (xe.Attributes["querystring"] != null)
                                    model.querystring = xe.Attributes["querystring"].Value;
                                if (xe.Attributes["templet"] != null)
                                    model.templet = xe.Attributes["templet"].Value;
                                if (xe.Attributes["channel"] != null)
                                    model.channel = xe.Attributes["channel"].Value;
                                if (xe.Attributes["type"] != null)
                                    model.type = xe.Attributes["type"].Value;
                                if (xe.Attributes["inherit"] != null)
                                    model.inherit = xe.Attributes["inherit"].Value;
                                ls.Add(model);
                            }
                        }
                        else
                        {
                            Model.url_rewrite model = new Model.url_rewrite();
                            if (xe.Attributes["name"] != null)
                                model.name = xe.Attributes["name"].Value;
                            if (xe.Attributes["path"] != null)
                                model.path = xe.Attributes["path"].Value;
                            if (xe.Attributes["pattern"] != null)
                                model.pattern = xe.Attributes["pattern"].Value;
                            if (xe.Attributes["page"] != null)
                                model.page = xe.Attributes["page"].Value;
                            if (xe.Attributes["querystring"] != null)
                                model.querystring = xe.Attributes["querystring"].Value;
                            if (xe.Attributes["templet"] != null)
                                model.templet = xe.Attributes["templet"].Value;
                            if (xe.Attributes["channel"] != null)
                                model.channel = xe.Attributes["channel"].Value;
                            if (xe.Attributes["type"] != null)
                                model.type = xe.Attributes["type"].Value;
                            if (xe.Attributes["inherit"] != null)
                                model.inherit = xe.Attributes["inherit"].Value;
                            ls.Add(model);
                        }

                    }
                }
            }
            return ls;
        }
        #endregion

    }
}
