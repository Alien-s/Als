using Als.MDB.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations;


namespace Als.MDB.Entities
{
    public class User : Person
    {
        [Required, MinLength(6), MaxLength(25)]
        public string Login { get; set; }


        [Required, MinLength(6), MaxLength(25)]
        public string Password { get; set; }


        [Required, MaxLength(100)]
        public string Email { get; set; }


        [Required, MaxLength(25)]
        public string Handy { get; set; }


        [MaxLength(25)]
        public string Phone { get; set; }


        public virtual Position Position { get; set; }


        public virtual Role Role { get; set; }
    }
}
