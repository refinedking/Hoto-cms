using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Configuration;
using System.Xml;
using Hoto.Common;

namespace Hoto.Web.UI
{
    /// <summary>
    /// DTcms的HttpModule类
    /// </summary>
    public class HttpModule : System.Web.IHttpModule
    {
        /// <summary>
        /// 实现接口的Init方法
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(ReUrl_BeginRequest);
        }

        /// <summary>
        /// 实现接口的Dispose方法
        /// </summary>
        public void Dispose()
        { }

        #region 页面请求事件处理
        /// <summary>
        /// 页面请求事件处理
        /// </summary>
        /// <param name="sender">事件的源</param>
        /// <param name="e">包含事件数据的 EventArgs</param>
        private void ReUrl_BeginRequest(object sender, EventArgs e)
        {
            HttpContext context = ((HttpApplication)sender).Context;
            string requestPath = context.Request.Path; //获得当前页面，包含目录
            string requestPage = requestPath.Substring(requestPath.LastIndexOf("/")); //获得当前页面，不包含目录
            Hoto.Model.siteconfig siteConfig = new Hoto.BLL.siteconfig().loadConfig(HotoUtils.GetXmlMapPath(HotoKeys.FILE_SITE_XML_CONFING)); //获得站点配置信息

            bool isRewritePath = IsUrlRewrite(siteConfig.webpath, requestPath); //排除不需要URL重写的目录
            switch (siteConfig.staticstatus)
            {
                case 0: //关闭重写
                    if (isRewritePath && IsAspxFile(requestPath))
                    {
                        context.RewritePath(siteConfig.webpath + HotoKeys.DIRECTORY_REWRITE_ASPX + "/" + requestPage);
                    }
                    break;
                case 1: //伪URL重写
                    if (isRewritePath)
                    {
                        RewriteUrl(context, siteConfig.webpath, requestPath, requestPage);
                    }
                    break;
                case 2: //全静态
                    if (requestPath.ToLower().Equals("/index.aspx"))
                    {
                        context.RewritePath(siteConfig.webpath + HotoKeys.DIRECTORY_REWRITE_HTML + "/index." + siteConfig.staticextension);
                    }
                    break;
            }

        }
        #endregion

        #region 重写URL
        /// <summary>
        /// 重写URL
        /// </summary>
        /// <param name="context">HttpContext对象</param>
        /// <param name="dirPath">站点目录</param>
        /// <param name="requestPath">获取的URL地址</param>
        private void RewriteUrl(HttpContext context, string dirPath, string requestPath, string requestPage)
        {
            foreach (Hoto.Model.url_rewrite url in SiteUrls.GetSiteUrls().Urls)
            {
                if ((Regex.IsMatch(requestPath, "^" + dirPath + url.pattern + "$", RegexOptions.None | RegexOptions.IgnoreCase)) && url.type != "no")
                {
                    string newUrl = Regex.Replace(requestPath, dirPath + url.pattern, url.querystring, RegexOptions.None | RegexOptions.IgnoreCase);
                    context.RewritePath(dirPath + HotoKeys.DIRECTORY_REWRITE_ASPX + "/" + url.page, string.Empty, newUrl);
                    return;
                }
            }
            if (IsAspxFile(requestPath))
            {
                context.RewritePath(dirPath + HotoKeys.DIRECTORY_REWRITE_ASPX + "/" + requestPage);
            }
        }
        #endregion

        #region 是否需要重写URL
        /// <summary>
        /// 遍历站点目录，排除不需要重写的URL地址
        /// </summary>
        /// <param name="dirPath">站点安装目录，以“/”结尾</param>
        /// <param name="requestPath">获取的URL地址</param>
        /// <returns>布尔值</returns>
        private bool IsUrlRewrite(string dirPath, string requestPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(HotoUtils.GetMapPath(dirPath));
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                if (requestPath.ToLower().StartsWith(dirPath + dir.Name.ToLower() + "/"))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 是否为ASPX文件
        /// <summary>
        /// 是否为ASPX文件
        /// </summary>
        /// <param name="requestPath">获取的URL地址</param>
        /// <returns>布尔值</returns>
        private bool IsAspxFile(string requestPath)
        {
            if (requestPath.LastIndexOf(".") >= 0)
            {
                if (requestPath.Substring(requestPath.LastIndexOf(".")) == ".aspx")
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

    }
    
    #region 站点伪Url信息类==========================================================
    /// <summary>
    /// 站点伪Url信息类
    /// </summary>
    public class SiteUrls
    {
        #region 内部属性和方法
        private static object lockHelper = new object();
        private static volatile SiteUrls instance = null;

        string SiteUrlsFile = HotoUtils.GetXmlMapPath(HotoKeys.FILE_SITE_XML_CONFING);
        private ArrayList _Urls;
        public ArrayList Urls
        {
            get { return _Urls; }
            set { _Urls = value; }
        }

        private NameValueCollection _Paths;
        public NameValueCollection Paths
        {
            get { return _Paths; }
            set { _Paths = value; }
        }

        private SiteUrls()
        {
            Urls = new ArrayList();
            Paths = new NameValueCollection();

            Hoto.BLL.url_rewrite bll = new Hoto.BLL.url_rewrite();
            List<Hoto.Model.url_rewrite> ls = bll.GetList("");
            foreach (Hoto.Model.url_rewrite model in ls)
            {
                Paths.Add(model.name, model.path);
                model.page = model.page.Replace("^", "&");
                model.querystring = model.querystring.Replace("^", "&");
                Urls.Add(model);
            }
        }
        #endregion

        public static SiteUrls GetSiteUrls()
        {
            SiteUrls _cache = HotoCacheHelper.Get<SiteUrls>(HotoKeys.CACHE_SITE_HTTP_MODULE);
            lock (lockHelper)
            {
                if (_cache == null)
                {
                    HotoCacheHelper.Insert(HotoKeys.CACHE_SITE_HTTP_MODULE, new SiteUrls(), HotoUtils.GetXmlMapPath(HotoKeys.FILE_URL_XML_CONFING));
                    instance = HotoCacheHelper.Get<SiteUrls>(HotoKeys.CACHE_SITE_HTTP_MODULE);
                }
            }
            return instance;

        }

    }
    #endregion

}