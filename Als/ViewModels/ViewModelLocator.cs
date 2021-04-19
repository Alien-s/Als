using Microsoft.Extensions.DependencyInjection;


namespace Als.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainViewModel => App.Services.GetRequiredService<MainWindowViewModel>();
        public LoginWindowViewModel LoginViewModel => App.Services.GetRequiredService<LoginWindowViewModel>();
        public UserManagerWindowVewModel UserManagerWindowVewModel => App.Services.GetRequiredService<UserManagerWindowVewModel>();
        //public UsersViewViewModel UsersViewViewModel => App.Services.GetRequiredService<UsersViewViewModel>();
        //public PositionsViewViewModel PositionsViewViewModel => App.Services.GetRequiredService<PositionsViewViewModel>();
    }
}
