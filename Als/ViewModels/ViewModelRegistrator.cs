using Microsoft.Extensions.DependencyInjection;


namespace Als.ViewModels
{
    /// <summary>Class for registration of ViewModels</summary>
    static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<LoginWindowViewModel>()
            .AddSingleton<UserManagerWindowVewModel>()
            ;
    }
}
