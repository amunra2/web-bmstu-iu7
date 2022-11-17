using System.ComponentModel.DataAnnotations;

namespace ServerING.Models 
{
    public class User {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Login { get; set; }

        [Required, MaxLength(255)]
        public string Password { get; set; }

        [Required]
        public string Role{ get; set; }
    }

    public enum UserSortState {
        LoginAsc,       // по логину по возрастанию
        LoginDesc,      // по логину по убыванию
        RoleAsc,        // по роли по возрастанию
        RoleDesc        // по роли по убыванию
    }
}
