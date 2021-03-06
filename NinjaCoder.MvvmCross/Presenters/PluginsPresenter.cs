﻿// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Presenters
{
    using System.Collections.Generic;

    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.Translators;
    using NinjaCoder.MvvmCross.Views.Interfaces;

    /// <summary>
    ///  Defines the PluginsPresenter type.
    /// </summary>
    public class PluginsPresenter
    {
        /// <summary>
        /// The view.
        /// </summary>
        private readonly IPluginsView view;

        /// <summary>
        /// The Settings Service.
        /// </summary>
        private readonly ISettingsService settingsService;


        /// <summary>
        /// The translator.
        /// </summary>
        private readonly ITranslator<string, Plugins> translator;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsPresenter" /> class.
        /// </summary>
        public PluginsPresenter(IPluginsView view)
            : this(new SettingsService(), 
                   new PluginsTranslator())
        {
            this.view = view;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsPresenter" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="translator">The translator.</param>
        public PluginsPresenter(
            ISettingsService settingsService,
            ITranslator<string, Plugins> translator)
        {
            this.settingsService = settingsService;
            this.translator = translator;
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <param name="viewModelNames">The view model names.</param>
        public void Load(List<string> viewModelNames)
        {
            Plugins plugins = this.translator.Translate(this.settingsService.CorePluginsPath);

            if (plugins != null)
            {
                foreach (Plugin plugin in plugins.Items)
                {
                    this.view.AddPlugin(plugin);
                }

                foreach (string viewModelName in viewModelNames)
                {
                    this.view.AddViewModel(viewModelName);
                }
            }
        }
    }
}
