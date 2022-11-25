
namespace ServerING.DTO {
    public class CountryDto {

        public string Name { get; set; }

        public int LevelOfInterest { get; set; }

        public int OverallPlayers { get; set; }
    }

    public class CountryUpdateDto : CountryDto {
        public int Id { get; set; }
    }

    public class CountrySparceDto {
        public string Name { get; set; }

        public int? LevelOfInterest { get; set; }

        public int? OverallPlayers { get; set; }
    }

    public class CountryUpdateSparceDto : CountrySparceDto {
        public int Id { get; set; }
    }
}
