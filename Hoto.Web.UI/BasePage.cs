using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Configuration;
using Hoto.Common;

namespace Hoto.Web.UI
{
    public partial class BasePage : System.Web.UI.Page
    {
        protected internal Hoto.Model.siteconfig config = new Hoto.BLL.siteconfig().loadConfig(HotoUtils.GetXmlMapPath(HotoKeys.FILE_SITE_XML_CONFING), true);
        protected internal Hoto.Model.userconfig uconfig = new Hoto.BLL.userconfig().loadConfig(HotoUtils.GetXmlMapPath(HotoKeys.FILE_USER_XML_CONFING));
        /// <summary>
        /// 父类的构造函数
        /// </summary>
        public BasePage()
        {
            //是否关闭网站
            if (config.webstatus == 0)
            {
                HttpContext.Current.Response.Redirect(config.webpath + "error.aspx?msg=" + HotoUtils.UrlEncode(config.webclosereason));
                return;
            }
            ShowPage();
        }

        /// <summary>
        /// 页面处理虚方法
        /// </summary>
        protected virtual void ShowPage()
        {
            //虚方法代码
        }

        #region 通用处理方法========================================================
        /// <summary>
        /// 判断用户是否已经登录(解决Session超时问题)
        /// </summary>
        public bool IsUserLogin()
        {
            //如果Session为Null
            if (HttpContext.Current.Session[HotoKeys.SESSION_USER_INFO] != null)
            {
                return true;
            }
            else
            {
                //检查Cookies
                string username = HotoUtils.GetCookie(HotoKeys.COOKIE_USER_NAME_REMEMBER, "DTcms"); //解密用户名
                string password = HotoUtils.GetCookie(HotoKeys.COOKIE_USER_PWD_REMEMBER, "DTcms");
                if (username != "" && password != "")
                {
                    Hoto.BLL.users bll = new Hoto.BLL.users();
                    Hoto.Model.users model = bll.GetModel(username, password, 0);
                    if (model != null)
                    {
                        HttpContext.Current.Session[HotoKeys.SESSION_USER_INFO] = model;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 取得用户信息
        /// </summary>
        public Hoto.Model.users GetUserInfo()
        {
            if (IsUserLogin())
            {
                Hoto.Model.users model = HttpContext.Current.Session[HotoKeys.SESSION_USER_INFO] as Hoto.Model.users;
                if (model != null)
                {
                    //为了能查询到最新的用户信息，必须查询最新的用户资料
                    model = new Hoto.BLL.users().GetModel(model.id);
                    return model;
                }
            }
            return null;
        }
        #endregion

        #region 页面基本处理方法=====================================================
        /// <summary>
        /// 返回URL重写统一链接地址
        /// </summary>
        public string linkurl(string _key)
        {
            return linkurl(_key, "");
        }

        /// <summary>
        /// 返回URL重写统一链接地址
        /// </summary>
        public string linkurl(string _key, params object[] _params)
        {
            Hashtable ht = new Hoto.BLL.url_rewrite().GetList();
            Hoto.Model.url_rewrite model = ht[_key] as Hoto.Model.url_rewrite;
            if (model == null)
            {
                return "";
            }
            try
            {
                string _result = string.Empty;
                string _rewriteurl = string.Format(model.path, _params);
                switch (config.staticstatus)
                {
                    case 1: //URL重写
                        _result = config.webpath + _rewriteurl;
                        break;
                    case 2: //全静态
                        _rewriteurl = _rewriteurl.Substring(0, _rewriteurl.LastIndexOf(".") + 1);
                        _result = config.webpath + HotoKeys.DIRECTORY_REWRITE_HTML + "/" + _rewriteurl + config.staticextension;
                        break;
                    default: //不开启
                        string _originalurl = model.page;
                        if (!string.IsNullOrEmpty(model.querystring))
                        {
                            _originalurl = model.page + "?" + Regex.Replace(_rewriteurl, model.pattern, model.querystring, RegexOptions.None | RegexOptions.IgnoreCase);
                        }
                        _result = config.webpath + _originalurl;
                        break;
                }
                return _result;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 返回分页字符串
        /// </summary>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="totalcount">记录总数</param>
        /// <param name="_key">URL映射Name名称</param>
        /// <param name="_params">传输参数</param>
        protected string get_page_link(int pagesize, int pageindex, int totalcount, string _key, params object[] _params)
        {
            return HotoUtils.OutPageList(pagesize, pageindex, totalcount, linkurl(_key, _params), 8);
        }

        /// <summary>
        /// 返回分页字符串
        /// </summary>
        /// <param name="pagesize">页面大小</param>
        /// <param name="pageindex">当前页</param>
        /// <param name="totalcount">记录总数</param>
        /// <param name="linkurl">链接地址</param>
        protected string get_page_link(int pagesize, int pageindex, int totalcount, string linkurl)
        {
            return HotoUtils.OutPageList(pagesize, pageindex, totalcount, linkurl, 8);
        }
        #endregion

    }
}
