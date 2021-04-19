using Als.Interfaces;
using Als.MDB.Entities;
using Als.ViewModels.BaseViewModel;
using System.Linq;

namespace Als.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        /// <summary>INotifyProperty  </summary>
        private string _CurrentUser;
        public string CurrentUser { get => _CurrentUser; set => Set(ref _CurrentUser, value); }


        #region CONSTRUCTOR
        public MainWindowViewModel()
        {

        }
        #endregion CONSTRUCTOR
    }
}
