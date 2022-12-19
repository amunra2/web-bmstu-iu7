﻿using ServerING.Interfaces;
using ServerING.Models;
using ServerING.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

using ServerING.Exceptions;
using ServerING.DTO;
using ServerING.Enums;
using ServerING.ModelsBL;
using AutoMapper;

namespace ServerING.Services {
    public interface IUserService {
        UserBL AddUser(UserBL user);
        UserBL UpdateUser(int id, UserBL user);
        UserBL DeleteUser(int id);

        UserBL GetUserByID(int id);
        IEnumerable<UserBL> GetAllUsers();

        UserBL GetUserByLogin(string login);
        IEnumerable<UserBL> GetUsersByRole(string role);
        
        IEnumerable<Server> GetUserFavoriteServers(int userId);
        FavoriteServer AddFavoriteServer(int userId, int serverId);
        FavoriteServer DeleteFavoriteServer(int userId, int serverId);


        UsersViewModel ParseUsers(IEnumerable<User> parsedUsers, string login, int page, UserSortState sortOrder);
        User ValidateUser(LoginViewModel model);
    }

    public class UserService : IUserService {

        private readonly IUserRepository userRepository;
        private readonly IServerService serverService;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IServerService serverService, IMapper mapper) {
            this.userRepository = userRepository;
            this.serverService = serverService;
            this.mapper = mapper;
        }


        private bool IsExist(UserBL user) {
            return userRepository.GetAll()
                .Where(item => item.Id != user.Id)
                .Any(item => item.Login == user.Login);
        }


        private bool IsFavoriteExist(int userId, int serverId) {
            return userRepository.GetFavoriteServerByUserAndServerId(userId, serverId) != null;
        }


        private bool IsExistById(int id) {
            return userRepository.GetByID(id) != null;
        }


        public UserBL AddUser(UserBL user) {
            if (IsExist(user))
                throw new UserAlreadyExistsException("User already exists");

            var transferedUser = mapper.Map<User>(user);
            return mapper.Map<UserBL>(userRepository.Add(transferedUser));
        }

        public IEnumerable<UserBL> GetAllUsers() {
            return mapper.Map<IEnumerable<UserBL>>(userRepository.GetAll());
        }

        public UserBL GetUserByID(int id) {
            return mapper.Map<UserBL>(userRepository.GetByID(id));
        }

        public UserBL GetUserByLogin(string login) {
            return mapper.Map<UserBL>(userRepository.GetByLogin(login));
        }

        public IEnumerable<UserBL> GetUsersByRole(string role) {
            return mapper.Map<IEnumerable<UserBL>>(userRepository.GetByRole(role));
        }

        public UserBL UpdateUser(int id, UserBL user) {
            if (!IsExistById(id))
                return null;

            if (IsExist(user))
                throw new UserAlreadyExistsException("User already exists");

            var transferedUser = mapper.Map<User>(user);
            return mapper.Map<UserBL>(userRepository.Update(transferedUser));
        }

        public UserBL DeleteUser(int id) {
            return mapper.Map<UserBL>(userRepository.Delete(id));
        }

        public IEnumerable<Server> GetUserFavoriteServers(int userId) {
            if (!IsExistById(userId))
                throw new UserNotExistsException("No user with such id");

            return userRepository.GetFavoriteServersByUserId(userId);
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
                //SortUserViewModel = new SortUserViewModel(sortOrder),
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
