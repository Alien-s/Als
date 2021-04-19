using Als.Infrastructure.Commands.LambdaCommands;
using Als.Interfaces;
using Als.MDB.Entities;
using Als.ViewModels.BaseViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Als.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        /// <summary>Repository of Users</summary>
        private readonly IRepository<User> _UserRepository;

        /// <summary>INotifyProperty for User collection</summary>
        private ObservableCollection<User> _UserCollection = new ObservableCollection<User>();
        public ObservableCollection<User> UserCollection { get => _UserCollection; set => Set(ref _UserCollection, value); }

        /// <summary>INotifyProperty  </summary>
        private string _CurrentUser;
        public string CurrentUser { get => _CurrentUser; set => Set(ref _CurrentUser, value); }


        #region COMMANDS

        #region LoadUserCollectionCommand
        /// <summary>Command for load all users from Database</summary>
        private ICommand _LoadUserCollectionCommand;
        public ICommand LoadUserCollectionCommand => _LoadUserCollectionCommand
            ??= new LambdaCommandAsync(OnLoadUserCollectionCommandExecuted, CanLoadUserCollectionCommandExecute);

        //Method for permisions for the Command LoadUserCollection
        private bool CanLoadUserCollectionCommandExecute(object parameter) => true;

        //Method for execution for the Command LoadUserCollection
        private async Task OnLoadUserCollectionCommandExecuted(object parameter)
        {
            /// <summary>Filling of the UserCollection via ObservableCollection EXTENSION</summary>
            UserCollection.AddClear(await _UserRepository.Items.ToArrayAsync());
        }
        #endregion LoadUserCollectionCommand

        #endregion COMMANDS



        #region CONSTRUCTOR

        public MainWindowViewModel(IRepository<User> UserRepository)
        {
            _UserRepository = UserRepository;
        }

        #endregion CONSTRUCTOR
    }
}
