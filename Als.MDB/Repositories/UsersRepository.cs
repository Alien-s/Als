using Als.MDB.Context;
using Als.MDB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Als.MDB.Repositories
{
    class UsersRepository : DbRepository<User>
    {
        //Overrided methode for receiving of the Users because we need to receive from DB the data about Positions and Roles also
        public override IQueryable<User> Items => base.Items
            .Include(item => item.Position)
            .Include(item => item.Role);


        //Constructor
        public UsersRepository(AlsDB alsDB) : base(alsDB) { }
    }
}
