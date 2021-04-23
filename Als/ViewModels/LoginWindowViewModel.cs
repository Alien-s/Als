using Als.Infrastructure.Commands.LambdaCommands;
using Als.MDB.Entities;
using Als.Services.Interfaces;
using Als.ViewModels.BaseViewModel;
using System.Linq;
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

        ///// <summary>Property UserDialog</summary>
        //private IUserDialog _UserDialog;

        #endregion PROPERTIES


        #region COMMANDS

        #region LoadLoginWindowCommand
        /// <summary>LoadLoginWindow Command</summary>
        private ICommand _LoadLoginWindowCommand;
        public ICommand LoadLoginWindowCommand => _LoadLoginWindowCommand
            ??= new LambdaCommand(OnLoadLoginWindowCommandExecuted, CanLoadLoginWindowCommandExecute);

        //Method for permisions for the Command LoadLoginWindow
        private bool CanLoadLoginWindowCommandExecute(object parameter) => true;

        //Method for execution for the Command LoadLoginWindow
        private void OnLoadLoginWindowCommandExecuted(object parameter) 
        {
            _MainWindowViewModel.CurrentUser = null;
            _MainWindowViewModel.CheckCurrentUserRole = false;
        }
        #endregion LoadLoginWindowCommand


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
            //TODO: This is antipattern!! Must be decision without translation of the PasswodBox to ViewModel
            PasswordBox passwordBox = (PasswordBox)parameter;
            Password = passwordBox.Password;

            User user = _MainWindowViewModel.UserCollection.FirstOrDefault(p => p.Login == Login && p.Password == Password);

            if (user == null || user.Role.Name.ToString().ToLower() == "blocked")
            {
                //TODO: _UserDialog.ConfirmError(Resources.Dictionaries.LoginWindowDict.ConfirmError.ToString(), Resources.Dictionaries.LoginWindowDict.CaptionError.ToString());

                MessageBox.Show("Sorry, access denied", "Access Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
                
                //TODO: Is neccessary make a Validation
            }

            if (user.Role.Name.ToString().ToLower() != "blocked")
            {
                if (user.Role.Name.ToString().ToLower() == "administrator") _MainWindowViewModel.CheckCurrentUserRole = true;
                Access = "Success";
                _MainWindowViewModel.CurrentUser = user;

                Password = null;
                Login = null;
                Access = null;
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
