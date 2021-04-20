using Als.Interfaces;
using Als.MDB.Entities;
using Microsoft.Extensions.DependencyInjection;


namespace Als.MDB.Repositories
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) => services
            .AddTransient<IRepository<User>, UsersRepository>()
            .AddTransient<IRepository<Position>, DbRepository<Position>>()
            .AddTransient<IRepository<Role>, DbRepository<Role>>()
            ;
    }
}
