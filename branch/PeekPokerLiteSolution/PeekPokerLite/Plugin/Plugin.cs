using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using PeekPoker.Interface;

namespace PeekPoker.Plugin
{
    /// <summary>
    ///   Load the plug-in
    /// </summary>
    public static class Plugin
    {
        private static PluginService _plugin;

        public static PluginService PluginService
        {
            get { return _plugin; }
        }

        /// <summary>
        ///   Plug-in initialisation constructor
        /// </summary>
        /// <param name="folderPath"> The plug-in folder path </param>
        public static void Init(string folderPath)
        {
            //initialise a new plug-in service class
            _plugin = new PluginService(folderPath);
        }
    }

    /// <summary>
    ///   Internal plug-in service class that get's all the information regarding the plugin
    /// </summary>
    public class PluginService
    {
        private readonly List<PluginData> _pluginList = new List<PluginData>();

        /// <summary>
        ///   Plug-in service constructor
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

        public IEnumerable<PluginData> PluginList
        {
            get { return _pluginList; }
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
                    //initials the plug-in data
                    PluginData pluginData = new PluginData(pluginInterfaceInstance.ApplicationName,
                                                           pluginInterfaceInstance.Description,
                                                           pluginInterfaceInstance.Author,
                                                           pluginInterfaceInstance.Version, pluginInterfaceInstance.Icon,
                                                           pluginInterfaceInstance);

                    _pluginList.Add(pluginData);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

    /// <summary>
    ///   Plug-in Data information class
    /// </summary>
    public class PluginData
    {
        private readonly string _author;
        private readonly string _description;
        private readonly Icon _icon;
        private readonly IPlugin _instance;
        private readonly string _name;
        private readonly string _version;

        public PluginData(string name, string description, string author, string version, Icon icon, IPlugin instance)
        {
            _name = name;
            _description = description;
            _author = author;
            _version = version;
            _icon = icon;
            _instance = instance;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public string Author
        {
            get { return _author; }
        }

        public string Version
        {
            get { return _version; }
        }

        public Icon Icon
        {
            get { return _icon; }
        }

        public IPlugin Instance
        {
            get { return _instance; }
        }
    }
}