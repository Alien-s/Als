using Als.MDB.Context;
using Als.MDB.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Als.Data
{
    /// <summary>Class for registration of Database</summary>
    public static class DbRegistrator
    {
        /// <summary>Methode for registration of Database and Repositoies</summary>
        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration Configuration) => services
            .AddDbContext<AlsDB>(opt =>
            {
                var type = Configuration["Type"];
                switch (type)
                {
                    case null: throw new InvalidOperationException("Unknow the DataBase type");

                    default: throw new InvalidOperationException($"Connection type {type} is not supported");

                    case "MSSQL":
                    case "LocalMSSQL":
                        opt.UseSqlServer(Configuration.GetConnectionString(type));
                        //Can use also: UseSQLite, UseInMemoryDatabase ...
                        break;
                }
            })
            .AddRepositoriesInDB()
            ;
    }
}
