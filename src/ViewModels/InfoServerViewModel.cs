using System.ComponentModel.DataAnnotations;

namespace ServerING.ViewModels {
    public class InfoServerViewModel {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [StringLength(100)]
        [Required(ErrorMessage = "Must be")]
        public string Name { get; set; }

        [Display(Name = "IP")]
        [StringLength(20)]
        [Required(ErrorMessage = "Must be")]
        public string Ip { get; set; }

        [Display(Name = "Game Name")]
        [StringLength(20)]
        [Required(ErrorMessage = "Must be")]
        public string GameName { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Must be")]
        public int CountryId { get; set; }

        [Display(Name = "Platform")]
        [Required(ErrorMessage = "Must be")]
        public int PlatformId { get; set; }

        [Display(Name = "Web Hosting")]
        [Required(ErrorMessage = "Must be")]
        public int WebHostingId { get; set; }
    }
}
