using System;
using System.Xml;
using System.Text;
using System.Web;
using Hoto.Common;

namespace Hoto.API.OAuth
{
    public class oauth_helper
    {
        /// <summary>
        /// 获取OAuth配置信息
        /// </summary>
        /// <param name="oauth_name"></param>
        public static oauth_config get_config(string oauth_name)
        {
            //读取接口配置信息
            Model.app_oauth model = new BLL.app_oauth().GetModel(oauth_name);
            if (model != null)
            {
                //读取站点配置信息
                Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(HotoKeys.FILE_SITE_XML_CONFING);
                //赋值
                oauth_config config = new oauth_config();
                config.oauth_name = model.api_path.Trim();
                config.oauth_app_id = model.app_id.Trim();
                config.oauth_app_key = model.app_key.Trim();
                config.return_uri = HotoUtils.DelLastChar(siteConfig.weburl, "/") + siteConfig.webpath + "api/oauth/" + model.api_path + "/return_url.aspx";
                return config;
            }
            return null;
        }
    }
}
