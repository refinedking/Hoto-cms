using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Caching;
using Hoto.Common;

namespace Hoto.BLL
{
    public partial class siteconfig
    {
        private readonly DAL.siteconfig dal = new DAL.siteconfig();

        /// <summary>
        ///  读取配置文件
        /// </summary>
        public Model.siteconfig loadConfig(string configFilePath)
        {
            Model.siteconfig model = HotoCacheHelper.Get<Model.siteconfig>(HotoKeys.CACHE_SITE_CONFIG);
            if (model == null)
            {
                HotoCacheHelper.Insert(HotoKeys.CACHE_SITE_CONFIG, dal.loadConfig(configFilePath), configFilePath);
                model = HotoCacheHelper.Get<Model.siteconfig>(HotoKeys.CACHE_SITE_CONFIG);
            }
            return model;
        }
        /// <summary>
        /// 读取客户端站点配置信息
        /// </summary>
        public Model.siteconfig loadConfig(string configFilePath, bool isClient)
        {
            Model.siteconfig model = HotoCacheHelper.Get<Model.siteconfig>(HotoKeys.CACHE_SITE_CONFIG_CLIENT);
            if (model == null)
            {
                model = dal.loadConfig(configFilePath);
                model.templateskin = model.webpath + "templates/" + model.templateskin;
                HotoCacheHelper.Insert(HotoKeys.CACHE_SITE_CONFIG_CLIENT, model, configFilePath);
            }
            return model;
        }

        /// <summary>
        ///  保存配置文件
        /// </summary>
        public Model.siteconfig saveConifg(Model.siteconfig model, string configFilePath)
        {
            return dal.saveConifg(model, configFilePath);
        }

    }
}
