using Als.Infrastructure.Commands.LambdaCommands;
using Als.Interfaces;
using Als.MDB.Entities;
using Als.ViewModels.BaseViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Als.ViewModels
{
    class LoginWindowViewModel :ViewModel
    {

        private MainWindowViewModel mainWindowViewModel;

        public LoginWindowViewModel(MainWindowViewModel mainViewModel)
        {
            mainWindowViewModel = mainViewModel;

        }

    }
}
