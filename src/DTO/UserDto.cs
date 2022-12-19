namespace ServerING.DTO {
    public class UserBaseDto {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role{ get; set; }
    }

    public class UserDto : UserBaseDto {
        public int Id { get; set; }
    }
}
