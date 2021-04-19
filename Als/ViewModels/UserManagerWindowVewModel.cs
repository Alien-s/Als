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

        /// <summary>Repository of Users</summary>
        private readonly IRepository<User> _UserRepository;

        /// <summary>INotifyProperty for User collection</summary>
        private ObservableCollection<User> _UserCollection = new ObservableCollection<User>();
        public ObservableCollection<User> UserCollection { get => _UserCollection; set => Set(ref _UserCollection, value); }

        #endregion PROPERTIES


        #region COMMANDS

        #region LoadUserCollectionCommand
        private ICommand _LoadUserCollectionCommand;
        public ICommand LoadUserCollectionCommand => _LoadUserCollectionCommand
            ??= new LambdaCommandAsync(OnLoadUserCollectionCommandExecuted, CanLoadUserCollectionCommandExecute);

        //Method for permisions for the Command LoadUserCollection
        private bool CanLoadUserCollectionCommandExecute(object parameter) => true;

        //Method for execution for the Command LoadUserCollection
        private async Task OnLoadUserCollectionCommandExecuted(object parameter)
        {
            UserCollection.Clear();
            UserCollection.Add(await _UserRepository.Items.ToArrayAsync());
        }
        #endregion LoadUserCollectionCommand

        #endregion COMMANDS


        #region FILTER OF LIST OF THE USERS

        // INotifyPropertyChanged properties for filtration of the users
        private ICollectionView selectedItems;
        public ICollectionView SelectedItems { get => selectedItems; set => Set(ref selectedItems, value); }


        //Property for text field of the filter
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

        public UserManagerWindowVewModel(IRepository<User> UserRepository)
        {
            _UserRepository = UserRepository;

            //Creating of the collection of users for demonstration at DataGrid and filtration
            SelectedItems = CollectionViewSource.GetDefaultView(UserCollection);
        }

        #endregion CONSTRUCTOR
    }
}
