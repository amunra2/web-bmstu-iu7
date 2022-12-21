using ServerING.Enums;

namespace ServerING.ViewModels {
    public class SortUserViewModel {
        public UserSortState LoginSort { get; private set; }       // значение для сортировки по логину
        public UserSortState RoleSort { get; private set; }        // значение для сортировки по роли
        public UserSortState CurrentSort { get; private set; }   // текущее значение сортировки

        public SortUserViewModel(UserSortState sortOrder) {
            LoginSort = sortOrder == UserSortState.LoginAsc ? UserSortState.LoginDesc : UserSortState.LoginAsc;
            RoleSort = sortOrder == UserSortState.RoleAsc ? UserSortState.RoleDesc : UserSortState.RoleAsc;
            CurrentSort = sortOrder;
        }
    }
}