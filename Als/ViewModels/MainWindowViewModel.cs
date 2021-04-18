using Als.Interfaces;
using Als.MDB.Entities;
using Als.ViewModels.BaseViewModel;
using System.Linq;

namespace Als.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<User> _UserRepository;

        public MainWindowViewModel(IRepository<User> userRepository)
        {
            _UserRepository = userRepository;

            //var us = userRepository.Items.ToArray();
        }
    }
}
