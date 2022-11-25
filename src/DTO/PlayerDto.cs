using System;

namespace ServerING.DTO {
    public class PlayerDto {
        public string Nickname { get; set; }

        public int HoursPlayed { get; set; }

        public DateTime LastPlayed { get; set; }
    }

    public class PlayerUpdateDto : PlayerDto {
        public int Id { get; set; }
    }

    public class PlayerSparceDto {
        public string Nickname { get; set; }

        public int? HoursPlayed { get; set; }

        public DateTime? LastPlayed { get; set; }
    }

    public class PlayerUpdateSparceDto : PlayerSparceDto {
        public int Id { get; set; }
    }
}
