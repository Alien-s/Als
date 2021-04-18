using System.ComponentModel.DataAnnotations;


namespace Als.MDB.Entities.BaseEntity
{
    public abstract class NamedEntity : Entity
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
    }
}
