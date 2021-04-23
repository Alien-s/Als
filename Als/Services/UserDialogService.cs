using Als.MDB.Entities;
using Als.Services.Interfaces;
using System;
using System.Windows;

namespace Als.Services
{
    internal class UserDialogService : IUserDialog
    {
        public bool ConfirmError(string Error, string Caption) => MessageBox
            .Show(
            Error, Caption,
            MessageBoxButton.OK,
            MessageBoxImage.Error)
            == MessageBoxResult.Yes;


        public bool ConfirmInformation(string Information, string Caption) => MessageBox
            .Show(
            Information, Caption,
            MessageBoxButton.YesNo,
            MessageBoxImage.Information)
            == MessageBoxResult.Yes;


        public bool ConfirmWarning(string Warning, string Caption) => MessageBox
            .Show(
            Warning, Caption,
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning)
            == MessageBoxResult.Yes;
    }
}
