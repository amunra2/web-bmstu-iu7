using ServerING.Enums;

namespace ServerING.DTO {
    public class ServerFilterDto {
        public string Name {get; set;}
        public string PlatformName {get; set;}
        public ServerStatus? Status {get; set;}
        public int? OwnerID { get; set; }
    }
}