using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Helpers;

namespace App.ViewModels
{
    public class SettingsPageViewModel
    {
        private readonly LocalSettingsManager _localSettingsManager;

        private bool _automaticallySaving;

        public bool AutomaticallySaving
        {
            get { return _automaticallySaving; }
            set
            {
                _automaticallySaving = value;
                SaveAutomaticallySavingSetting();
            }
        }

        public SettingsPageViewModel()
        {
            _localSettingsManager = new LocalSettingsManager();

            LoadAutomaticallySavingSetting();
        }

        private void SaveAutomaticallySavingSetting()
        {
            _localSettingsManager.SaveSetting("SaveUserDataAutomatically", AutomaticallySaving);
        }

        private void LoadAutomaticallySavingSetting()
        {
            try
            {
                AutomaticallySaving = (bool)_localSettingsManager.LoadSetting("SaveUserDataAutomatically");
            }
            catch (Exception)
            {
                AutomaticallySaving = false;
            }
        }
    }
}
