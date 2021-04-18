using Microsoft.Extensions.DependencyInjection;


namespace Als.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainViewModel => App.Services.GetRequiredService<MainWindowViewModel>();
        public LoginWindowViewModel LoginViewModel => App.Services.GetRequiredService<LoginWindowViewModel>();
    }
}
