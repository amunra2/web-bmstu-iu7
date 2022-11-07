using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerING.Models {
    public class Server {

        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required, MaxLength(30)]
        public string Ip { get; set; }

        [Required, MaxLength(30)]
        public string GameVersion { get; set; }

        /*[Required]*/
        public int Rating{ get; set; }

        [ForeignKey("WebHosting")]
        public int HostingID { get; set; }

        [ForeignKey("Platform")]
        public int PlatformID { get; set; }

        [ForeignKey("Country")]
        public int CountryID { get; set; }

        public virtual WebHosting Hosting { get; set; }
        public virtual Platform Platform { get; set; }
        public virtual Country Country { get; set; }
    }


    public enum ServerSortState {
        NameAsc,            // по имени по возрастанию
        NameDesc,           // по имени по убыванию
        IPAsc,              // по IP по возрастанию
        IPDesc,             // по IP по убыванию
        GameVersionAsc,     // по версии игры по возрастанию
        GameVersionDesc,    // по версии игры по убыванию
        RatingAsc,          // по платформе по возрастанию
        RatingDesc,         // по платформе по убыванию
        PlatformAsc,        // по платформе по возрастанию
        PlatformDesc        // по платформе по убыванию
    }
}
