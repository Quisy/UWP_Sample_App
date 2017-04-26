using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App.Helpers;
using App.Models;
using App.ViewModels.Base;
using Newtonsoft.Json;

namespace App.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly LocalStorageManager _localStorageManager;
        private readonly LocalSettingsManager _localSettingsManager;

        private string _firstName;
        private string _lastName;
        private bool _isSaving;
        private bool _canSave = true;
        private bool _saveAutomatically;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged(nameof(FirstName));
                RaisePropertyChanged(nameof(UserFullName));

                if (_saveAutomatically && _canSave)
                    SaveUserCommand.Execute(null);
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged(nameof(LastName));
                RaisePropertyChanged(nameof(UserFullName));

                if (_saveAutomatically && _canSave)
                    SaveUserCommand.Execute(null);
            }
        }

        public string UserFullName => $"{FirstName} {LastName}";

        

        public bool IsSaving
        {
            get { return _isSaving; }
            set
            {
                _isSaving = value;
                RaisePropertyChanged(nameof(IsSaving));
            }
        }

        public MainPageViewModel()
        {
            _localStorageManager = new LocalStorageManager();
            _localSettingsManager = new LocalSettingsManager();

            FirstName = "User FirstName";
            LastName = "User LastName";

            LoadAutomaticallySavingSetting();
        }

        public ICommand SaveUserCommand => new CommandHandler(async () => await SaveUserData());
        public ICommand LoadUserCommand => new CommandHandler(async () => await LoadUserData());



        private async Task SaveUserData()
        {
            if (!_canSave)
                return;

            IsSaving = true;

            var user = new User
            {
                FirstName = this.FirstName,
                LastName = this.LastName
            };

            // Delay to show working thread on UI
            await Task.Delay(TimeSpan.FromSeconds(2));

            string data = JsonConvert.SerializeObject(user);

            await _localStorageManager.WriteData(data, "UserData");

            IsSaving = false;
        }

        private async Task LoadUserData()
        {
            _canSave = false;

            User user = JsonConvert.DeserializeObject<User>(await _localStorageManager.ReadData("UserData"));

            this.FirstName = user.FirstName;
            this.LastName = user.LastName;

            _canSave = true;
        }

        private void LoadAutomaticallySavingSetting()
        {
            try
            {
                _saveAutomatically = (bool)_localSettingsManager.LoadSetting("SaveUserDataAutomatically");
            }
            catch (Exception)
            {
                _saveAutomatically = false;
            }
        }

    }
}
