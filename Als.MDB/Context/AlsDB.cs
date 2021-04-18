using Als.MDB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Als.MDB.Context
{
    public class AlsDB : DbContext
    {
        //Structure of DataBase
        public DbSet<User> Users { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Role> Roles { get; set; }


        //Contructor
        public AlsDB(DbContextOptions<AlsDB> options) : base(options) { }
    }
}
