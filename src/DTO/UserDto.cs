namespace ServerING.DTO {
    public class UserDtoBase {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Role{ get; set; }
    }

    public class UserDto : UserDtoBase {
        public int Id { get; set; }
    }
}
