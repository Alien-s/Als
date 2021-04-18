using Als.Interfaces;
using System.ComponentModel.DataAnnotations;


namespace Als.MDB.Entities.BaseEntity
{
    public abstract class Entity : IEntity
    {
        [Key, Required]
        public int Id { get; set; }
    }
}
