using System;
using System.Collections.Generic;

namespace ServerING.DTO {
    public class ServerFormDto {
        public string Name {get; set;}

        public string Ip {get; set;}

        public string GameName {get; set;}

        public int HostingID { get; set; }

        public int PlatformID { get; set; }

        public int CountryID { get; set; }
    }
}
