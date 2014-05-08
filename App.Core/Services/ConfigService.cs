﻿using App.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;

namespace App.Core.Services
{
    public sealed class ConfigName
    {

        private readonly String name;

        public static readonly ConfigName WebsiteUrlName = new ConfigName("WebsiteUrlName");
        public static readonly ConfigName WebsiteTitle = new ConfigName("WebsiteTitle");
        public static readonly ConfigName WebsiteUrl = new ConfigName("WebsiteUrl");

        public static readonly ConfigName EmailHost = new ConfigName("EmailHost");
        public static readonly ConfigName EmailUsername = new ConfigName("EmailUsername");
        public static readonly ConfigName EmailPassword = new ConfigName("EmailPassword");
        public static readonly ConfigName EmailPort = new ConfigName("EmailPort");
        public static readonly ConfigName EmailEnableSSL = new ConfigName("EmailEnableSSL");
        public static readonly ConfigName EmailFrom = new ConfigName("EmailFrom");

        private ConfigName(String name)
        {
            this.name = name;
        }

        public override String ToString()
        {
            return name;
        }

    }

    public interface IConfigService
    {
        string GetValue(ConfigName name);
        IDictionary<string, string> GetValues(ConfigName[] configNames);
    }

    public class ConfigService : IConfigService
    {
        private ShopDBEntities db;

        public ConfigService()
        {
            this.db = new ShopDBEntities();
        }

        string IConfigService.GetValue(ConfigName configName)
        {
            IDictionary<string, string> ConfigList = (IDictionary<string, string>)HttpContext.Current.Cache["ConfigList"];

            if (ConfigList == null)
            {
                ConfigList = db.Configs.ToDictionary(x => x.Key, x => x.Value);
                HttpContext.Current.Cache.Insert("ConfigList", ConfigList, null, DateTime.Now.AddSeconds(3600), System.Web.Caching.Cache.NoSlidingExpiration);
            }

            string name = configName.ToString();
            var config = ConfigList.FirstOrDefault(x => x.Key.Equals(name));
            if (!string.IsNullOrEmpty(config.Key))
            {
                return config.Value;
            }
            return null;
        }

        IDictionary<string, string> IConfigService.GetValues(ConfigName[] configNames)
        {
            var names = configNames.Select(x => x.ToString());
            var configs = this.db.Configs.Where(x => names.Contains(x.Key));

            return configs.ToDictionary(x => x.Key, x => x.Value);
        }


    }
}
