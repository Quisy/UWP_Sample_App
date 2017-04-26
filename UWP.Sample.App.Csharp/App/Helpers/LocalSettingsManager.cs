using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Helpers
{
    public class LocalSettingsManager
    {
        private readonly Windows.Storage.ApplicationDataContainer _localSettings =
            Windows.Storage.ApplicationData.Current.LocalSettings;

        public void SaveSetting(string settingName, object value)
        {
            _localSettings.Values[settingName] = value;
        }

        public object LoadSetting(string settingName)
        {
            try
            {
                return _localSettings.Values[settingName];
            }
            catch (Exception)
            {
                throw new ArgumentException("Setting key not found");
            }
            
        }
    }
}
