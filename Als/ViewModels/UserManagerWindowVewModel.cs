using Als.Infrastructure.Commands;
using Als.Interfaces;
using Als.MDB.Entities;
using Als.ViewModels.BaseViewModel;
using System.Windows.Input;


namespace Als.ViewModels
{
    class UserManagerWindowVewModel : ViewModel
    {
        #region PROPERTIES

        /// <summary>Property for List of Users </summary>
        private readonly IRepository<User> _UserRepository;


        /// <summary>INotifyProperty for changing of the Views at the Window </summary>
        private ViewModel _CurrentViewModel;
        public ViewModel CurrentViewModel { get => _CurrentViewModel; set => Set(ref _CurrentViewModel, value); }

        #endregion PROPERTIES


        #region COMMANDS

        #region ShowUsersViewCommand
        public ICommand ShowUsersViewCommand { get; }

        //Method for permisions for the Command ShowUsersView
        private bool CanShowUsersViewCommandExecute(object parameter) => true;

        //Method for execution for the Command ShowUsersView
        private void OnShowUsersViewCommandExecuted(object parameter) 
        {
            CurrentViewModel = new UsersViewViewModel(_UserRepository);
        }
        #endregion ShowUsersViewCommand


        #region ShowPositionsViewCommand
        public ICommand ShowPositionsViewCommand { get; }

        //Method for permisions for the Command ShowPositionsView
        private bool CanShowPositionsViewCommandExecute(object parameter) => true;

        //Method for execution for the Command ShowPositionsView
        private void OnShowPositionsViewCommandExecuted(object parameter) { }
        #endregion ShowPositionsViewCommand

        #endregion COMMANDS


        public UserManagerWindowVewModel()
        {
            #region COMMANDS

            //LambdaCommand for handling of the Command ShowUsersView
            ShowUsersViewCommand = new LambdaCommand(OnShowUsersViewCommandExecuted, CanShowUsersViewCommandExecute);

            //LambdaCommand for handling of the Command ShowPositionsView
            ShowPositionsViewCommand = new LambdaCommand(OnShowPositionsViewCommandExecuted, CanShowPositionsViewCommandExecute);
            
            #endregion COMMANDS
        }
    }
}
