using Microsoft.Extensions.DependencyInjection;


namespace Als.ViewModels
{
    /// <summary> Class for Properties of ViewModels</summary>
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainViewModel => App.Services.GetRequiredService<MainWindowViewModel>();
        public LoginWindowViewModel LoginViewModel => App.Services.GetRequiredService<LoginWindowViewModel>();
        public UserManagerWindowVewModel UserManagerWindowVewModel => App.Services.GetRequiredService<UserManagerWindowVewModel>();
    }
}
