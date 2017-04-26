using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Models;
using App.ViewModels.Base;

namespace App.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged(nameof(UserFullName));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged(nameof(UserFullName));
            }
        }

        public string UserFullName => $"{FirstName} {LastName}";

        public MainPageViewModel()
        {

            FirstName = "User FirstName";
            LastName = "User LastName";
        }
    }
}
