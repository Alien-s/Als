using Als.Infrastructure.Commands.LambdaCommands;
using Als.Interfaces;
using Als.MDB.Entities;
using Als.Services.Interfaces;
using Als.ViewModels.BaseViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;


namespace Als.ViewModels
{
    class UserManagerWindowVewModel : ViewModel
    {
        #region PROPERTIES

        /// <summary>Property for reference to the Properties of the MainWindowViewModel</summary>
        private readonly MainWindowViewModel _MainWindowViewModel;

        /// <summary>Repository of Users</summary>
        private readonly IRepository<User> _UserRepository;

        /// <summary>Repository of Positions</summary>
        private readonly IRepository<Position> _PositionRepository;

        /// <summary>Repository of Roles</summary>
        private readonly IRepository<Role> _RoleRepository;

        /// <summary>INotifyProperty for Position collection</summary>
        private ObservableCollection<Position> _PositionCollection = new ObservableCollection<Position>();
        public ObservableCollection<Position> PositionCollection { get => _PositionCollection; set => Set(ref _PositionCollection, value); }

        /// <summary>INotifyProperty for Role collection</summary>
        private ObservableCollection<Role> _RoleCollection = new ObservableCollection<Role>();
        public ObservableCollection<Role> RoleCollection { get => _RoleCollection; set => Set(ref _RoleCollection, value); }

        /// <summary>Property UserDialog</summary>
        private IUserDialog _UserDialog;

        /// <summary>INotifyProperty for FILTRATION of the users</summary>
        private ICollectionView _SelectedItems;
        public ICollectionView SelectedItems { get => _SelectedItems; set => Set(ref _SelectedItems, value); }

        /// <summary>Property for text field of the FILTER</summary>
        private string _ItemsFilterText = string.Empty;
        public string ItemsFilterText
        {
            get => _ItemsFilterText;
            set
            {
                Set(ref _ItemsFilterText, value);
                SelectedItems.Filter += Filter;
            }
        }

        /// <summary>INotifyProperty for filling of the TextBoxes with Selected User data</summary>
        private User _SelectedUser = new User();
        public User SelectedUser { get => _SelectedUser; set => Set(ref _SelectedUser, value); }

        /// <summary>INotifyProperty for GropBoxHeader</summary>
        private string _UserMode = Resources.Dictionaries.UserManagerWindowDict.UserModeDefault;
        public string UserMode { get => _UserMode; set => Set(ref _UserMode, value); }

        #endregion PROPERTIES


        #region COMMANDS

        #region LoadUserManagerWindowCommand
        /// <summary>LoadUserManagerWindowCommand</summary>
        private ICommand _LoadUserManagerWindowCommand;
        public ICommand LoadUserManagerWindowCommand => _LoadUserManagerWindowCommand
            ??= new LambdaCommandAsync(OnLoadUserManagerWindowCommandExecuted, CanLoadUserManagerWindowCommandExecute);

        //Method for permisions for the Command LoadUserManagerWindow
        private bool CanLoadUserManagerWindowCommandExecute(object parameter) => true;

        //Method for execution for the Command LoadUserManagerWindow
        private async Task OnLoadUserManagerWindowCommandExecuted(object parameter) 
        {
            //Filling of the PositionCollection via ObservableCollection EXTENSION
            PositionCollection.AddClear(await _PositionRepository.Items.ToArrayAsync());

            //Filling of the RoleCollection via ObservableCollection EXTENSION
            RoleCollection.AddClear(await _RoleRepository.Items.ToArrayAsync());

            //Clear Filter and Selected User
            ItemsFilterText = string.Empty;
            SelectedUser = null;
        }
        #endregion LoadUserManagerWindowCommand


        #region AddNewUserCommand
        /// <summary>Command for adding of new User</summary>
        private ICommand _AddNewUserCommand;
        public ICommand AddNewUserCommand => _AddNewUserCommand
            ??= new LambdaCommand(OnAddNewUserCommandExecuted, CanAddNewUserCommandExecute);

        //Method for permisions for the Command AddNewUser
        private bool CanAddNewUserCommandExecute(object parameter) => true;

        //Method for execution for the Command AddNewUser
        private void OnAddNewUserCommandExecuted(object parameter) 
        {
            SelectedUser = new User();
            UserMode = Resources.Dictionaries.UserManagerWindowDict.UserModeAdd;
        }
        #endregion AddNewUserCommand


        #region EditUserCommand
        /// <summary>Command for edit of User</summary>
        private ICommand _EditUserCommand;
        public ICommand EditUserCommand => _EditUserCommand
            ??= new LambdaCommand(OnEditUserCommandExecuted, CanEditUserCommandExecute);

        //Method for permisions for the Command EditUser
        private bool CanEditUserCommandExecute(object parameter) 
        {
            if (SelectedUser == null || SelectedUser.Name == null) return false;
            return true;
        }

        //Method for execution for the Command EditUser
        private void OnEditUserCommandExecuted(object parameter) => UserMode = Resources.Dictionaries.UserManagerWindowDict.UserModeEdit;
        #endregion EditUserCommand


        #region SaveChangingsByUserCommand
        private ICommand _SaveChangingsByUserCommand;
        public ICommand SaveChangingsByUserCommand => _SaveChangingsByUserCommand 
            ??= new LambdaCommand(OnSaveChangingsByUserCommandExecuted, CanSaveChangingsByUserCommandExecute);

        //Method for permisions for the Command SaveChangingsByUser
        private bool CanSaveChangingsByUserCommandExecute(object parameter) => true;

        //Method for execution for the Command SaveChangingsByUser
        private void OnSaveChangingsByUserCommandExecuted(object parameter)
        {
            //TODO: Is neccessary make a Validation
            var new_user = SelectedUser;

            if (SelectedUser.Id == 0)
            {
                _MainWindowViewModel.UserCollection.Add(_UserRepository.Add(new_user));
            }
            else
            {
                _UserRepository.Update(new_user);
            }
   
            SelectedUser = null;
            UserMode = Resources.Dictionaries.UserManagerWindowDict.UserModeDefault;
        }

        #endregion SaveChangingsByUserCommand


        #region RemoveUserCommand
        /// <summary>Command for delete of User</summary>
        private ICommand _RemoveUserCommand;
        public ICommand RemoveUserCommand => _RemoveUserCommand
            ??= new LambdaCommand(OnRemoveUserCommandExecuted, CanRemoveUserCommandExecute);

        //Method for permisions for the Command RemoveUser
        private bool CanRemoveUserCommandExecute(object parameter) 
        {
            if (SelectedUser == null || SelectedUser.Name == null) return false;
            return true;
        }

        //Method for execution for the Command RemoveUser
        private void OnRemoveUserCommandExecuted(object parameter) 
        {
            var user_to_remove = SelectedUser;

            if (!_UserDialog.ConfirmWarning(
                $"{user_to_remove.Name} {user_to_remove.Surname} {Resources.Dictionaries.UserManagerWindowDict.ConfirmWarning}", Resources.Dictionaries.UserManagerWindowDict.WarningCaption)) return;

            _UserRepository.Remove(user_to_remove.Id);
            _MainWindowViewModel.UserCollection.Remove(user_to_remove);

            SelectedUser = null;

        }
        #endregion RemoveUserCommand


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

        /// <summary>This method is filtering of the users depends of the request from text field</summary>
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

        //Constructor
        public UserManagerWindowVewModel(MainWindowViewModel MainViewModel, IRepository<User> UserRepository, IRepository<Position> PositionRepository, IRepository<Role> RoleRepository, IUserDialog UserDialog)
        {
            _MainWindowViewModel = MainViewModel;
            _UserRepository = UserRepository;
            _PositionRepository = PositionRepository;
            _RoleRepository = RoleRepository;
            _UserDialog = UserDialog;

            //Creating of the collection of users for demonstration at DataGrid and filtration
            SelectedItems = CollectionViewSource.GetDefaultView(_MainWindowViewModel.UserCollection);
        }

        #endregion CONSTRUCTOR
    }
}
