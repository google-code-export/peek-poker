using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using PeekPoker.Interface;

namespace PeekPoker.Plugin
{
    /// <summary>Load the plugin</summary>
    public static class Plugin
    {
        private static PluginService _plugin;

        /// <summary>Plugin initialisation constructor</summary>
        /// <param name="folderPath">The plugin folder path</param>
        public static void Init(string folderPath)
        {
            //initialise a new plugin service class
            _plugin = new PluginService(folderPath);
        }

        public static PluginService PluginService
        {
            get { return _plugin; }
        }
    }

    /// <summary>Internal plugin service class that get's all the information regarding the plugin</summary>
    public class PluginService
    {
        private readonly List<PluginData> _pluginList = new List<PluginData>();

        /// <summary>Plugin service constructor</summary>
        /// <param name="folderPath">The plugin folder path</param>
        public PluginService(string folderPath)
        {
            try
            {
                foreach (var plugin in Directory.GetFiles(folderPath))
                {
                    var file = new FileInfo(plugin);
                    if (file.Extension.Equals(".dll"))
                    {
                        AddPlugin(plugin);
                    }
                }
            }
            catch(Exception e){throw new Exception(e.Message);}
        }

        private void AddPlugin(string pluginPath)
        {
            try
            {
                var pluginAssembly = Assembly.LoadFrom(pluginPath);//Load assembly given its fullname and path
                foreach (var pluginType in pluginAssembly.GetTypes())
                {
                    if (!pluginType.IsPublic) continue;//break the for each loop to next iteration if any
                    if (pluginType.IsAbstract) continue;//break the for each loop to next iteration if any
                    //search for specified interface while ignoring case sesitivity
                    var typeInterface = pluginType.GetInterface("PeekPoker.Interface.IPlugin", true);

                    if (typeInterface == null) continue;  //break if interface is present in the application

                    //New plugin information setting
                    var pluginInterfaceInstance = (IPlugin)(Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString())));
                    //initials the plugin data
                    var pluginData = new PluginData(pluginInterfaceInstance.ApplicationName, pluginInterfaceInstance.Description,
                                                    pluginInterfaceInstance.Author, pluginInterfaceInstance.Version, pluginInterfaceInstance.Icon,pluginInterfaceInstance);

                    _pluginList.Add(pluginData);
                }
            }
            catch (Exception e) {throw new Exception(e.Message);}
        }

        public IEnumerable<PluginData> PluginList
        {
            get { return _pluginList; }
        }
    }

    /// <summary>Plugin Data information class</summary>
    public class PluginData
    {
        private readonly string _name;
        private readonly string _description;
        private readonly string _author;
        private readonly string _version;
        private readonly Icon _icon;
        private readonly IPlugin _instance;

        public PluginData(string name, string description, string author, string version,Icon icon, IPlugin instance)
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
            get{return _name;}
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
