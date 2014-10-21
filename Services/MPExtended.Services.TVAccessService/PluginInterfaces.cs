using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPExtended.Services.TVAccessService
{
    public interface IPluginData
    {
        Dictionary<string, string> GetConfiguration(string pluginname);
    }
}
