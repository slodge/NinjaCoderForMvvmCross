﻿// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SettingsService type.
// </summary>
// ------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Services
{
    using System;

    using Microsoft.Win32;

    using NinjaCoder.MvvmCross.Services.Interfaces;

    /// <summary>
    ///  Defines the SettingsService type.
    /// </summary>
    public class SettingsService : ISettingsService
    {
        /// <summary>
        /// Gets or sets a value indicating whether [log to file].
        /// </summary>
        public bool LogToFile
        {
            get { return this.GetRegistryValue("LogToFile", "N") == "N"; }
            set { this.SetRegistryValue("LogFile", value ? "Y" : "N"); }
        }
        
        /// <summary>
        /// Gets or sets the log file path.
        /// </summary>
        public string LogFilePath 
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\ninja-coder-for-mvvmcross.log"; } 
        }

        /// <summary>
        /// Gets the converters templates path.
        /// </summary>
        public string ConvertersTemplatesPath
        {
            get { return  this.GetItemTemplatesPath() + @"\Converters"; }
        }

        /// <summary>
        /// Gets the core plugins path.
        /// </summary>
        public string CorePluginsPath 
        { 
            get { return this.GetRegistryValue("Core Plugins Path", string.Empty); }
        }

        /// <summary>
        /// Gets the code snippets path.
        /// </summary>
        public string CodeSnippetsPath
        {
            get { return this.GetRegistryValue("Code Snippets Path", string.Empty); }
        }

        /// <summary>
        /// Gets the registry key.
        /// </summary>
        /// <returns></returns>
        internal RegistryKey GetRegistryKey(bool writeable)
        {
            RegistryKey softwareKey = Registry.CurrentUser.OpenSubKey("Software");

            if (softwareKey != null)
            {
                RegistryKey scorchioKey = softwareKey.OpenSubKey("Scorchio Limited");

                if (scorchioKey != null)
                {
                    return scorchioKey.OpenSubKey("Ninja Coder for MvvmCross", writeable);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the registry value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The vaue.</returns>
        internal string GetRegistryValue(
            string name, 
            string defaultValue)
        {
            RegistryKey registryKey = this.GetRegistryKey(false);

            if (registryKey != null)
            {
                return (string)registryKey.GetValue(name);

            }

            return defaultValue;
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        internal void SetRegistryValue(
            string name,
            string value)
        {
             RegistryKey registryKey = this.GetRegistryKey(true);

             if (registryKey != null)
             {
                 registryKey.SetValue(name, value);
             }
        }

        /// <summary>
        /// Gets the item templates path.
        /// </summary>
        /// <returns>The Item templates path.</returns>
        internal string GetItemTemplatesPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Microsoft Visual Studio 11.0\Common7\IDE\ItemTemplates\CSharp\MvvmCross";
        }
    }
}
