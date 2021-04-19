using Als.Infrastructure.Commands;
using Als.Interfaces;
using Als.MDB.Entities;
using Als.ViewModels.BaseViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Als.ViewModels
{
    class UsersViewViewModel : ViewModel
    {
        /// <summary>INotifyProperty for filling of the DataGrid with Users</summary>
        private ICollection<User> _Users;
        public ICollection<User> Users { get => _Users; set => Set(ref _Users, value); }




        public UsersViewViewModel(ICollection<User> users)
        {
            Users = users;
        }
    }
}
