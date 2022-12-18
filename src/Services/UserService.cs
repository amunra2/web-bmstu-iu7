using ServerING.Interfaces;
using ServerING.Models;
using ServerING.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

using ServerING.Exceptions;
using ServerING.DTO;
using ServerING.Enums;
using ServerING.ModelsBL;


namespace ServerING.Services {
    public interface IUserService {
        User AddUser(UserDtoBase user);
        User UpdateUser(UserDto userDto);
        User PatchUpdateUser(UserDto userDto);
        User DeleteUser(int id);

        User GetUserByID(int id);
        IEnumerable<User> GetAllUsers();

        User GetUserByLogin(string login);
        IEnumerable<User> GetUsersByRole(string role);
        
        IEnumerable<ServerBL> GetUserFavoriteServers(
            int userId,
            ServerFilterDto filter, 
            ServerSortState? sortState,
            int? page
        );
        FavoriteServer AddFavoriteServer(int userId, int serverId);
        FavoriteServer DeleteFavoriteServer(int userId, int serverId);


        UsersViewModel ParseUsers(IEnumerable<User> parsedUsers, string login, int page, UserSortState sortOrder);
        User ValidateUser(LoginViewModel model);
    }

    public class UserService : IUserService {

        private readonly IUserRepository userRepository;
        private readonly IServerService serverService;
        private readonly IMapper mapper;

        public UserService(
            IUserRepository userRepository,
            IServerService serverService,
            IMapper mapper
        ) {
            this.userRepository = userRepository;
            this.serverService = serverService;
            this.mapper = mapper;
        }


        private bool IsExist(User user) {
            return userRepository.GetAll()
                .Any(item => item.Login == user.Login && item.Id != user.Id);
        }

        private bool IsFavoriteExist(int userId, int serverId) {
            return userRepository.GetFavoriteServerByUserAndServerId(userId, serverId) != null;
        }


        private bool IsExistById(int id) {
            return userRepository.GetByID(id) != null;
        }


        public User AddUser(UserDtoBase userDto) {
            var user = new User()
            {
                Login = userDto.Login,
                Password = userDto.Password,
                Role = userDto.Role
            };

            if (IsExist(user))
                throw new UserAlreadyExistsException("User already exists");

            return userRepository.Add(user);
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

        public User UpdateUser(UserDto userDto) {
            var user = new User()
            {
                Id = userDto.Id,
                Login = userDto.Login,
                Password = userDto.Password,
                Role = userDto.Role
            };

            if (!IsExistById(user.Id))
                throw new UserNotExistsException("No user with such id");

            if (IsExist(user))
                throw new UserAlreadyExistsException("User already exists");

            return userRepository.Update(user);
        }

        public User PatchUpdateUser(UserDto userDto) {
            if (!IsExistById(userDto.Id))
                throw new UserNotExistsException("No user with such id");

            var dbUser = GetUserByID(userDto.Id);

            var user = new User()
            {
                Id = userDto.Id,
                Login = userDto.Login ?? dbUser.Login,
                Password = userDto.Password ?? dbUser.Password,
                Role = userDto.Role ?? dbUser.Role
            };

            if (IsExist(user))
                throw new UserAlreadyExistsException("User already exists");

            return userRepository.Update(user);
        }

        public User DeleteUser(int id) {
            return userRepository.Delete(id);
        }

        public IEnumerable<ServerBL> GetUserFavoriteServers(
            int userId,
            ServerFilterDto filter, 
            ServerSortState? sortState,
            int? page
        ) {
            if (!IsExistById(userId))
                throw new UserNotExistsException("No user with such id");

            var servers = mapper.Map<IEnumerable<ServerBL>>(userRepository.GetFavoriteServersByUserId(userId));

            // Фильтрация
            servers = serverService.FilterServers(servers, filter);

            // Сортировка
            if (sortState != null) {
                servers = serverService.SortServersByOption(servers, sortState.Value);
            }

            // Пагинация
            if (page != null) {
                servers = serverService.PaginationServers(servers, page.Value);
            }
            

            return servers;
        }

        public FavoriteServer AddFavoriteServer(int userId, int serverId) {
            if (IsFavoriteExist(userId, serverId))
                throw new UserFavoriteAlreadyExistsException("Already in favorites");

            FavoriteServer favoriteServer = new FavoriteServer {
                UserID = userId,
                ServerID = serverId
            };

            serverService.UpdateServerRating(serverId, +1);

            userRepository.AddFavoriteServer(favoriteServer);

            return favoriteServer;
        }

        public FavoriteServer DeleteFavoriteServer(int userId, int serverId) {
            serverService.UpdateServerRating(serverId, -1);

            FavoriteServer favoriteServer = userRepository.GetFavoriteServerByUserAndServerId(userId, serverId);

            if (favoriteServer != null)
                userRepository.DeleteFavoriteServer(favoriteServer.Id);

            return favoriteServer;
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
