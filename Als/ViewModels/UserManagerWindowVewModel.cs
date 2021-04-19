using Als.Infrastructure.Commands.LambdaCommands;
using Als.Interfaces;
using Als.MDB.Entities;
using Als.ViewModels.BaseViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;


namespace Als.ViewModels
{
    class UserManagerWindowVewModel : ViewModel
    {
        #region PROPERTIES

        /// <summary>Property for reference to the Properties of the MainWindowViewModel</summary>
        private MainWindowViewModel mainWindowViewModel;

        /// <summary>INotifyProperty for FILTRATION of the users</summary>
        private ICollectionView selectedItems;
        public ICollectionView SelectedItems { get => selectedItems; set => Set(ref selectedItems, value); }

        /// <summary>Property for text field of the FILTER</summary>
        private string itemsFilterText = string.Empty;
        public string ItemsFilterText
        {
            get => itemsFilterText;
            set
            {
                Set(ref itemsFilterText, value);
                SelectedItems.Filter += Filter;
            }
        }

        /// <summary>INotifyProperty for filling of the TextBoxes with Selected User data</summary>
        private User selectedUser = new User();
        public User SelectedUser { get => selectedUser; set => Set(ref selectedUser, value); }

        /// <summary>INotifyProperty for GropBoxHeader</summary>
        private string userMode = "User Info";
        public string UserMode { get => userMode; set => Set(ref userMode, value); }

        #endregion PROPERTIES


        #region COMMANDS

        #region LoadUserManagerWindowCommand
        /// <summary>LoadUserManagerWindowCommand</summary>
        private ICommand _LoadUserManagerWindowCommand;
        public ICommand LoadUserManagerWindowCommand => _LoadUserManagerWindowCommand
            ??= new LambdaCommand(OnLoadUserManagerWindowCommandExecuted, CanLoadUserManagerWindowCommandExecute);

        //Method for permisions for the Command LoadUserManagerWindow
        private bool CanLoadUserManagerWindowCommandExecute(object parameter) => true;

        //Method for execution for the Command LoadUserManagerWindow
        private void OnLoadUserManagerWindowCommandExecuted(object parameter) 
        {
            SelectedUser = null;
        }
        #endregion LoadUserManagerWindowCommand


        #region AddNewUserCommand
        /// <summary>Command for adding of the new User</summary>
        private ICommand _AddNewUserCommand;
        public ICommand AddNewUserCommand => _AddNewUserCommand
            ??= new LambdaCommand(OnAddNewUserCommandExecuted, CanAddNewUserCommandExecute);

        //Method for permisions for the Command AddNewUser
        private bool CanAddNewUserCommandExecute(object parameter) => true;

        //Method for execution for the Command AddNewUser
        private void OnAddNewUserCommandExecuted(object parameter) { }
        #endregion AddNewUserCommand


        #region ClearFilterCommand
        /// <summary>Command for clear of the filter for Users</summary>
        private ICommand _ClearFilterCommand;
        public ICommand ClearFilterCommand => _ClearFilterCommand 
            ??= new LambdaCommand(OnClearFilterCommandExecuted, CanClearFilterCommandExecute);

        //Method for permisions for the Command ClearFilter
        private bool CanClearFilterCommandExecute(object parameter) => true;

        //Method for execution for the Command ClearFilter
        private void OnClearFilterCommandExecuted(object parameter)
        {
            ItemsFilterText = string.Empty;
            SelectedUser = null;
        }
        #endregion ClearFilterCommand


        #endregion COMMANDS


        #region FILTER OF LIST OF THE USERS

        //This method is filtering of the users depends of the request from text field
        private bool Filter(object obj)
        {
            User currentUser = obj as User;
            if (!string.IsNullOrEmpty(ItemsFilterText))
            {
                //SelectedUser = new User();
                return currentUser.Id.ToString().Contains(ItemsFilterText) || currentUser.Name.ToLower().Contains(ItemsFilterText.ToLower()) || currentUser.Surname.ToLower().Contains(ItemsFilterText.ToLower());
            }
            else
            {
                return true;
            }
        }

        #endregion  FILTER OF LIST OF THE USERS


        #region CONSTRUCTOR

        public UserManagerWindowVewModel(MainWindowViewModel mainViewModel)
        {
            mainWindowViewModel = mainViewModel;

            //Creating of the collection of users for demonstration at DataGrid and filtration
            SelectedItems = CollectionViewSource.GetDefaultView(mainWindowViewModel.UserCollection);
        }

        #endregion CONSTRUCTOR
    }
}
