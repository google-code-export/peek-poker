using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using PeekPoker.Interface;

namespace PeekPoker.Plugin
{
    /// <summary>
    ///     Internal plug-in service class that get's all the information regarding the plug-in
    /// </summary>
    public class PluginService
    {
        private List<IPlugin> _pluginDatas = new List<IPlugin>();

        /// <summary>
        ///     Plug-in service constructor
        /// </summary>
        /// <param name="folderPath"> The plug-in folder path </param>
        public PluginService(string folderPath)
        {
            try
            {
                foreach (string plugin in Directory.GetFiles(folderPath))
                {
                    FileInfo file = new FileInfo(plugin);
                    if (file.Extension.Equals(".dll"))
                    {
                        AddPlugin(plugin);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        ///     Plug-in Data
        /// </summary>
        public List<IPlugin> PluginDatas
        {
            get { return _pluginDatas; }
        }

        private void AddPlugin(string pluginPath)
        {
            try
            {
                Assembly pluginAssembly = Assembly.LoadFrom(pluginPath); //Load assembly given its full name and path
                foreach (Type pluginType in pluginAssembly.GetTypes())
                {
                    if (!pluginType.IsPublic) continue; //break the for each loop to next iteration if any
                    if (pluginType.IsAbstract) continue; //break the for each loop to next iteration if any
                    //search for specified interface while ignoring case sensitivity
                    Type typeInterface = pluginType.GetInterface("PeekPoker.Interface.IPlugin", true);

                    if (typeInterface == null) continue; //break if interface is present in the application

                    //New plug-in information setting
                    IPlugin pluginInterfaceInstance =
                        (IPlugin) (Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString())));

                    _pluginDatas.Add(pluginInterfaceInstance);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}