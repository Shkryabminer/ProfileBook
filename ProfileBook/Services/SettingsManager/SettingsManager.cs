using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ProfileBook.Services
{
    public class SettingsManager : ISettingsManager
    {

        private ISettings SetPlugin { get; set; }
        public SettingsManager(ISettings plugin)
        {
            SetPlugin = plugin;
        }

        #region --ISettings implementation--
        public int AutorizatedUserId
        {
            get => SetPlugin.GetValueOrDefault(nameof(AutorizatedUserId), -1);
            set => SetPlugin.AddOrUpdateValue(nameof(AutorizatedUserId), value);
        }
        public int SelectedSortMethode
        {
            get => SetPlugin.GetValueOrDefault(nameof(SelectedSortMethode), 0);
            set => SetPlugin.AddOrUpdateValue(nameof(SelectedSortMethode), value);
        }

        #endregion



    }
}
