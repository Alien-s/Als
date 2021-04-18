using System.ComponentModel.DataAnnotations;


namespace Als.MDB.Entities.BaseEntity
{
    public abstract class Person : NamedEntity
    {
        [Required, MaxLength(100)]
        public string Surname { get; set; }

        [MaxLength(100)]
        public string Patronymic { get; set; }
    }
}
