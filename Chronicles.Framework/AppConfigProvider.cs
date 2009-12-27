using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Chronicles.Framework
{
    public class AppConfigProvider:IAppConfigProvider
    {
        #region IAppConfigProvider Members

        public string GetConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        #endregion
    }
}
