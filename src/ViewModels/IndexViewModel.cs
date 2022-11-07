using ServerING.Models;
using System.Collections.Generic;

namespace ServerING.ViewModels {
    public class IndexViewModel {

        public List<Server> Servers { get; set; }
        public List<Platform> Platforms { get; set; }
        public List<int> UserFavoriteServerIds { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}