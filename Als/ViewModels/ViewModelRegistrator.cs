using Microsoft.Extensions.DependencyInjection;


namespace Als.ViewModels
{
    static class ViewModelRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services) => services
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<LoginWindowViewModel>()
            ;
    }
}
