using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MPExtended.Libraries.Service;
using MPExtended.Libraries.Service.Composition;
using MPExtended.Services.TVAccessService.Interfaces;

namespace MPExtended.Services.TVAccessService
{
    public class ProviderHandler
    {
        public static ITVAccessService GetProvider()
        {
            var loader = new IndexedPluginLoader<int>("Id");
            loader.AddFromTreeMatch(@"PlugIns\MPExtended.PlugIns.TAS.*", @"Plugins\TV");
            loader.AddExport<IPluginData>(new PluginData());
            loader.AddRequiredMetadata("Id");
            loader.AddRequiredMetadata("Name");

            Dictionary<int, Plugin<ITVAccessService>> plugins = loader.GetIndexedPlugins<ITVAccessService>();
            foreach (var plugin in plugins)
            {
                Log.Debug("Checking TAS plugin provider {0}", plugin);
                if (plugin.Key == Configuration.TV.Id)
                {
                    Log.Info("Matched plugin {0} with required ID {1}", plugin.Value.Metadata["Name"], Configuration.TV.Id);
                    return plugin.Value.Value;
                }
            }

            Log.Error("Cannot map {0} to a service", Configuration.TV.Id);
            return null;
        }
    }
}
