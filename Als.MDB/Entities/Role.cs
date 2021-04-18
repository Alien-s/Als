using Als.MDB.Entities.BaseEntity;
using System.Collections.Generic;

namespace Als.MDB.Entities
{
    public class Role : NamedEntity
    {
        //Navigation Property for generate a Forign Key
        public ICollection<User> Users { get; set; }
    }
}
