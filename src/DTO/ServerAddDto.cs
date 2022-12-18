using ServerING.Enums;

namespace ServerING.DTO {
    public class ServerAddDto {
        public string Name {get; set;}

        public string Ip {get; set;}

        public string GameName {get; set;}

        public ServerStatus? Status {get; set;}

        public int? HostingID { get; set; }

        public int? PlatformID { get; set; }

        public int? CountryID { get; set; }

        public int? OwnerID { get; set; }
    }
}
