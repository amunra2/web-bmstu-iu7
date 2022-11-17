using ServerING.Models;

namespace ServerING.ViewModels {
    public class SortViewModel {
        public ServerSortState NameSort { get; private set; }       // значение для сортировки по имени
        public ServerSortState IPSort { get; private set; }         // значение для сортировки по IP
        public ServerSortState GameNameSort { get; private set; }   // значение для сортировки по названию игры
        public ServerSortState RatingSort { get; private set; }     // значение для сортировки по рейтингу
        public ServerSortState PlatformSort { get; private set; }   // значение для сортировки по платформе
        public ServerSortState CurrentSort { get; private set; }    // текущее значение сортировки

        public SortViewModel(ServerSortState sortOrder) {
            NameSort = sortOrder == ServerSortState.NameAsc ? ServerSortState.NameDesc : ServerSortState.NameAsc;
            IPSort = sortOrder == ServerSortState.IPAsc ? ServerSortState.IPDesc : ServerSortState.IPAsc;
            GameNameSort = sortOrder == ServerSortState.GameNameAsc ? ServerSortState.GameNameDesc : ServerSortState.GameNameAsc;
            PlatformSort = sortOrder == ServerSortState.PlatformAsc ? ServerSortState.PlatformDesc : ServerSortState.PlatformAsc;
            RatingSort = sortOrder == ServerSortState.RatingAsc ? ServerSortState.RatingDesc : ServerSortState.RatingAsc;
            CurrentSort = sortOrder;
        }
    }
}