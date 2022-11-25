namespace ServerING.DTO {
    public class UserDto {
        public string Login { get; set; }

        public string Password { get; set; }

        public string Role{ get; set; }
    }

    public class UserUpdateDto : UserDto {
        public int Id { get; set; }
    }
}
