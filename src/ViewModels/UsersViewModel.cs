using ServerING.Models;
using System.Collections.Generic;

namespace ServerING.ViewModels {
    public class UsersViewModel {

        public List<User> Users { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterUserViewModel FilterUserViewModel { get; set; }
        public SortUserViewModel SortUserViewModel { get; set; }
    }
}