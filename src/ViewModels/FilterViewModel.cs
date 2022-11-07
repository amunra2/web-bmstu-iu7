using Microsoft.AspNetCore.Mvc.Rendering;
using ServerING.Models;
using System.Collections.Generic;

namespace ServerING.ViewModels {
    public class FilterViewModel {

        public FilterViewModel(List<Platform> platforms, int? platform, string name) {

            platforms.Insert(0, new Platform { Id = 0, Name = "All" }); // начальный элемент для выбора всех платформ
            Platforms = new SelectList(platforms, "Id", "Name", platform);
            SelectedPlatform = platform;

            SelectedName = name;
        }

        public SelectList Platforms { get; private set; }       // список платформ
        public int? SelectedPlatform { get; private set; }      // выбранная платформа

        public string SelectedName { get; private set; }        // введенное имя
    }
}