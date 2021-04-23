using Als.MDB.Entities;


namespace Als.Services.Interfaces
{
    internal interface IUserDialog
    {
        bool ConfirmInformation(string Information, string Caption);
        bool ConfirmWarning(string Warning, string Caption);
        bool ConfirmError(string Error, string Caption);
    }
}
