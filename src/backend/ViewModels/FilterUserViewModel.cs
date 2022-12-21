using Microsoft.AspNetCore.Mvc.Rendering;
using ServerING.Models;
using System.Collections.Generic;

namespace ServerING.ViewModels {
    public class FilterUserViewModel {

        public FilterUserViewModel(string login) {
            SelectedLogin = login;
        }

        public string SelectedLogin { get; private set; }        // введенный логин
    }
}