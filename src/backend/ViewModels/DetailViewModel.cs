using ServerING.Models;
using System.Collections.Generic;

namespace ServerING.ViewModels {
    public class DetailViewModel {

        public Server Server { get; set; }
        public WebHosting WebHosting { get; set; }
        public Country Country { get; set; }
        public IEnumerable<Player> Players { get; set; }
    }
}
