namespace ServerING.ModelsBL
{
    public class UserBL {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role{ get; set; }
    }

    public enum UserSortState {
        LoginAsc,       // по логину по возрастанию
        LoginDesc,      // по логину по убыванию
        RoleAsc,        // по роли по возрастанию
        RoleDesc        // по роли по убыванию
    }
}
