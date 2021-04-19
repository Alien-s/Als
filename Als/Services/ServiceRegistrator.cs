﻿using Als.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace Als.Services
{
    static class ServicesRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IUserDialog, UserDialogService>()
            ;
    }
}
