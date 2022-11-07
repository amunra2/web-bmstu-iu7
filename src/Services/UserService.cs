using ServerING.Interfaces;
using ServerING.Models;
using ServerING.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ServerING.Services {

    public interface IUserService {
        void AddUser(User user);
        User DeleteUser(User user);
        void UpdateUser(User user);

        User GetUserByID(int id);
        IEnumerable<User> GetAllUsers();

        User GetUserByLogin(string login);
        IEnumerable<User> GetUsersByRole(string role);
        
        IEnumerable<Server> GetUserFavoriteServers(User user);

        UsersViewModel ParseUsers(IEnumerable<User> parsedUsers, string login, int page, UserSortState sortOrder);
        User ValidateUser(LoginViewModel model);
    }

    public class UserService : IUserService {

        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }


        private bool IsExist(User user) {
            return userRepository.GetAll()
                .Any(item =>
                    item.Login == user.Login &&
                    item.Role == user.Role
                    );
        }


        private bool IsExistById(int id) {
            return userRepository.GetByID(id) != null;
        }


        public void AddUser(User user) {

            if (IsExist(user))
                throw new Exception("Such user is already exist");

            userRepository.Add(user);
        }

        public IEnumerable<User> GetAllUsers() {
            return userRepository.GetAll();
        }

        public User GetUserByID(int id) {
            return userRepository.GetByID(id);
        }

        public User GetUserByLogin(string login) {
            return userRepository.GetByLogin(login);
        }

        public IEnumerable<User> GetUsersByRole(string role) {
            return userRepository.GetByRole(role);
        }

        public User DeleteUser(User user) {

            if (!IsExistById(user.Id))
                throw new Exception("No such user");

            return userRepository.Delete(user.Id);
        }

        public void UpdateUser(User user) {

            if (!IsExistById(user.Id))
                throw new Exception("No such user");

            userRepository.Update(user);
        }

        public IEnumerable<Server> GetUserFavoriteServers(User user) {
            return userRepository.GetFavoriteServersByUserId(user.Id);
        }

        public UsersViewModel ParseUsers(IEnumerable<User> parsedServers, string login, int page, ServerSortState sortOrder) {
            throw new NotImplementedException();
        }

        private IEnumerable<User> FilterUsersByName(IEnumerable<User> users, string login) {

            var filteredUsers = users;

            if (!String.IsNullOrEmpty(login)) {
                filteredUsers = filteredUsers.Where(p => p.Login.Contains(login));
            }

            return filteredUsers;
        }

        private IEnumerable<User> SortUsersByOption(IEnumerable<User> users, UserSortState sortOrder) {

            IEnumerable<User> filteredUsers;

            if (sortOrder == UserSortState.LoginDesc) {
                filteredUsers = users.OrderByDescending(s => s.Login);
            }
            else if (sortOrder == UserSortState.RoleAsc) {
                filteredUsers = users.OrderBy(s => s.Role);
            }
            else if (sortOrder == UserSortState.RoleDesc) {
                filteredUsers = users.OrderByDescending(s => s.Role);
            }
            else {
                filteredUsers = users.OrderBy(s => s.Login);
            }

            return filteredUsers;
        }

        private IEnumerable<User> PaginationUsers(IEnumerable<User> users, int page, int pageSize) {

            var paginatedServers = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return paginatedServers;
        }

        public UsersViewModel ParseUsers(IEnumerable<User> parsedUsers, string login, int page, UserSortState sortOrder) {

            // Параметры пагинации 
            int pageSize = 3;
            var count = parsedUsers.Count();

            // фильтрация
            parsedUsers = FilterUsersByName(parsedUsers, login);

            // сортировка
            parsedUsers = SortUsersByOption(parsedUsers, sortOrder);

            // пагинация
            parsedUsers = PaginationUsers(parsedUsers, page, pageSize);

            // Вывод - формируем модель представления
            UsersViewModel viewModel = new UsersViewModel {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortUserViewModel = new SortUserViewModel(sortOrder),
                FilterUserViewModel = new FilterUserViewModel(login),
                Users = parsedUsers.ToList()
            };

            return viewModel;
        }

        public User ValidateUser(LoginViewModel model) {

            User user = userRepository.GetByLogin(model.Login);

            if (user != null) {
                if (user.Password == model.Password) {
                    return user;
                }
            }

            return null;
        }
    }
}
