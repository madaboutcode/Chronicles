using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronicles.Framework
{
    public interface IAppConfigProvider
    {
        string GetConfig(string key);
    }
}
