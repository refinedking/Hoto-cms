using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Caching;
using Hoto.Common;

namespace Hoto.BLL
{
    public partial class userconfig
    {
        private readonly DAL.userconfig dal = new DAL.userconfig();

        /// <summary>
        ///  读取用户配置文件
        /// </summary>
        public Model.userconfig loadConfig(string configFilePath)
        {
            Model.userconfig model = HotoCacheHelper.Get<Model.userconfig>(HotoKeys.CACHE_USER_CONFIG);
            if (model == null)
            {
                HotoCacheHelper.Insert(HotoKeys.CACHE_USER_CONFIG, dal.loadConfig(configFilePath), configFilePath);
                model = HotoCacheHelper.Get<Model.userconfig>(HotoKeys.CACHE_USER_CONFIG);
            }
            return model;
        }

        /// <summary>
        ///  保存用户配置文件
        /// </summary>
        public Model.userconfig saveConifg(Model.userconfig model, string configFilePath)
        {
            return dal.saveConifg(model, configFilePath);
        }

    }
}
