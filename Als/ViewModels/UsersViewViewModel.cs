using Als.Interfaces;
using Als.MDB.Entities;
using Als.ViewModels.BaseViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Als.ViewModels
{
    class UsersViewViewModel : ViewModel
    {
        /// <summary>Property for List of Users </summary>
        private readonly IRepository<User> _UserRepository;

        public UsersViewViewModel(IRepository<User> userRepository)
        {
            _UserRepository = userRepository;
        }
    }
}
