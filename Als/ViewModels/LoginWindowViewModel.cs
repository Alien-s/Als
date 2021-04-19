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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Als.ViewModels
{
    class LoginWindowViewModel :ViewModel
    {
        #region PROPERTIES

        /// <summary>Property for reference to the Properties of the MainWindowViewModel</summary>
        private MainWindowViewModel _MainWindowViewModel;

        /// <summary>INotifyProperty Login</summary>
        private string login;
        public string Login { get => login; set => Set(ref login, value); }

        /// <summary>INotifyProperty Password</summary>
        private string password;
        public string Password { get => password; set => Set(ref password, value); }

        /// <summary>INotifyProperty for Checking of the Access</summary>
        private string access;
        public string Access { get => access; set => Set(ref access, value); }

        #endregion PROPERTIES


        #region COMMANDS

        #region LogInCommand
        /// <summary>Log In Command</summary>
        private ICommand _LogInCommand;
        public ICommand LogInCommand => _LogInCommand 
            ??= new LambdaCommand(OnLogInCommandExecuted, CanLogInCommandExecute);

        //Method for permisions for the Command LogInCommand
        private bool CanLogInCommandExecute(object parameter) => true;

        //Method for execution for the Command LogInCommand
        private void OnLogInCommandExecuted(object parameter)
        {
            PasswordBox passwordBox = (PasswordBox)parameter;
            Password = passwordBox.Password;

            User user = _MainWindowViewModel.UserCollection.FirstOrDefault(p => p.Login == Login && p.Password == Password);

            if (user == null || user.Role.Name.ToString().ToLower() == "blocked")
            {
                MessageBox.Show("Sorry, access denied");
                return;
                
                //TODO: MessageBox is Not MVVM - must be changet to IUserDialog
                //TODO: Is neccessary make a Validation
            }

            if (user.Role.Name.ToString().ToLower() != "blocked")
            {
                Access = "Success";
                _MainWindowViewModel.CurrentUser = user;
                Password = null;
            }
        }

        #endregion LogInCommand

        #endregion COMMANDS


        //Constructor
        public LoginWindowViewModel(MainWindowViewModel MainViewModel)
        {
            _MainWindowViewModel = MainViewModel;

        }

    }
}
