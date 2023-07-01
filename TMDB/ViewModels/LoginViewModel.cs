using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDB.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        [RelayCommand]
        public void Login()
        {
            var t = this.username;
        }
    }
}
