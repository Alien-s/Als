using Als.Infrastructure.Commands.LambdaCommands;
using Als.Interfaces;
using Als.MDB.Entities;
using Als.ViewModels.BaseViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;


namespace Als.ViewModels
{
    class UserManagerWindowVewModel : ViewModel
    {
        #region PROPERTIES

        /// <summary>Property for List of Users </summary>
        private readonly IRepository<User> _Users;

        /// <summary>Property for List of Positions </summary>
        private readonly IRepository<Position> _Positions;

        /// <summary>INotifyProperty for changing of the Views at the Window </summary>
        private ViewModel _CurrentViewModel;
        public ViewModel CurrentViewModel { get => _CurrentViewModel; set => Set(ref _CurrentViewModel, value); }

        /// <summary>INotifyProperty for filling of the DataGrid with Users</summary>
        private ICollection<User> allUsers;
        public ICollection<User> AllUsers { get => allUsers; set => Set(ref allUsers, value); }

        #endregion PROPERTIES


        #region COMMANDS

        #region ShowUsersViewCommand
        private ICommand _ShowUsersViewCommand;
        public ICommand ShowUsersViewCommand => _ShowUsersViewCommand 
            ??= new LambdaCommand(OnShowUsersViewCommandExecuted, CanShowUsersViewCommandExecute);

        //Method for permisions for the Command ShowUsersView
        private bool CanShowUsersViewCommandExecute(object parameter) => true;

        //Method for execution for the Command ShowUsersView
        private void OnShowUsersViewCommandExecuted(object parameter) 
        {
            CurrentViewModel = new UsersViewViewModel(AllUsers);
        }
        #endregion ShowUsersViewCommand


        #region ShowPositionsViewCommand
        private ICommand _ShowPositionsViewCommand;
        public ICommand ShowPositionsViewCommand => _ShowPositionsViewCommand 
            ??= new LambdaCommand(OnShowPositionsViewCommandExecuted, CanShowPositionsViewCommandExecute);

        //Method for permisions for the Command ShowPositionsView
        private bool CanShowPositionsViewCommandExecute(object parameter) => true;

        //Method for execution for the Command ShowPositionsView
        private void OnShowPositionsViewCommandExecuted(object parameter) 
        {
            CurrentViewModel = new PositionsViewViewModel();
        }
        #endregion ShowPositionsViewCommand

        #endregion COMMANDS



        #region CONSTRUCTOR

        public UserManagerWindowVewModel(IRepository<User> usersRepository, IRepository<Position> potitionsRepository)
        {
            _Users = usersRepository;
            _Positions = potitionsRepository;
            //AllUsers = usersRepository.Items.ToList();


        }

        #endregion CONSTRUCTOR
    }
}
