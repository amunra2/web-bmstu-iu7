using AutoMapper;
using ServerING.ModelsBL;
using ServerING.Interfaces;
using ServerING.Models;
using ServerING.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ServerING.DTO;
using ServerING.Exceptions;
using ServerING.Enums;

namespace ServerING.Services {

    public interface IServerService {
        Server AddServer(ServerDtoBase server);
        Server DeleteServer(int id);
        Server PutServer(int id, ServerUpdateDto server);
        Server PatchServer(int id, ServerUpdateDto server);

        Server GetServerByID(int id);
        IEnumerable<ServerBL> GetAllServers(
            ServerFilterDto filter,
            ServerSortState? sortState,
            int? page
        );

        public IEnumerable<Player> GetServerPlayers(int serverId);

        Server GetServerByName(string name);
        Server GetServerByIP(string ip);

        IEnumerable<Server> GetServersByGameName(string gameVersion);
        IEnumerable<Server> GetServersByHostingID(int id);
        IEnumerable<Server> GetServersByPlatformID(int id);
        IEnumerable<Server> GetServersByRating(int rating);

        IEnumerable<ServerBL> FilterServers(IEnumerable<ServerBL> servers, ServerFilterDto filter);
        IEnumerable<ServerBL> SortServersByOption(IEnumerable<ServerBL> servers, ServerSortState sortOrder);
        IEnumerable<ServerBL> PaginationServers(IEnumerable<ServerBL> servers, int page);
        // IndexViewModel ParseServers(IEnumerable<Server> parsedServers, string name, int? platformId, int page, ServerSortState sortOrder);
        DetailViewModel DetailServer(int serverId);
        IEnumerable<int> GetUserFavoriteServersIds(int userId);

        void UpdateServerRating(int serverId, int change);

        bool IsServerExists(Server server);
    }

    public class ServerService : IServerService {

        private readonly IServerRepository serverRepository;
        private readonly IPlatformRepository platformRepository;
        private readonly IUserRepository userRepository;
        private readonly IHostingRepository hostingRepository;
        private readonly IMapper mapper;

        public ServerService(IServerRepository serverRepository, 
                IPlatformRepository platformRepository, 
                IUserRepository userRepository,
                IHostingRepository hostingRepository,
                IMapper mapper) {
            this.serverRepository = serverRepository;
            this.platformRepository = platformRepository;
            this.userRepository = userRepository;
            this.hostingRepository = hostingRepository;
            this.mapper = mapper;
        }


        private bool IsExist(string serverIp) {
            return serverRepository.GetAll()
                .Any(item => item.Ip == serverIp);
        }

        private bool IsExistById(int id) {
            return serverRepository.GetByID(id) != null;
        }

        public Server AddServer(ServerDtoBase server) {
            if (IsExist(server.Ip)) {
                var conflictedId = serverRepository.GetByIP(server.Ip).Id;
                throw new ServerConflictException(conflictedId);
            }

            var transferedServer = new ServerBL {
                Name = server.Name,
                GameName = server.GameName,
                Ip = server.Ip,
                Status = server.Status.Value,
                HostingID = server.HostingID.Value,
                PlatformID = server.PlatformID.Value,
                CountryID = server.CountryID.Value,
                OwnerID = server.OwnerID.Value
            };

            return serverRepository.Add(mapper.Map<Server>(transferedServer));
        }

        public Server PutServer(int id, ServerUpdateDto server) {
            if (IsExist(server.Ip)) {
                var conflictedId = serverRepository.GetByIP(server.Ip).Id;
                throw new ServerConflictException(conflictedId);
            }

            if (!IsExistById(id))
                throw null;

            var transferedServer = new Server {
                Id = id,
                Name = server.Name,
                GameName = server.GameName,
                Status = server.Status.Value,
                Rating = server.Rating.Value, // ! чекнуть когда-нибудь
                Ip = server.Ip,
                HostingID = server.HostingID.Value,
                PlatformID = server.PlatformID.Value,
                CountryID = server.CountryID.Value,
                OwnerID = server.OwnerID.Value
            };

            return serverRepository.Update(transferedServer);
        }

        public Server PatchServer(int id, ServerUpdateDto server) {
            if (IsExist(server.Ip)) {
                var conflictedId = serverRepository.GetByIP(server.Ip).Id;
                throw new ServerConflictException(conflictedId);
            }

            if (!IsExistById(id))
                return null;

            var existedServer = GetServerByID(id);

            var transferedServer = new Server {
                Id = id,
                Name = server.Name != null ? server.Name : existedServer.Name,
                GameName = server.GameName != null ? server.GameName : existedServer.GameName,
                Ip = server.Ip != null ? server.Ip : existedServer.Ip,
                Status = server.Status != null ? server.Status.Value : existedServer.Status,
                Rating = server.Rating != null ? server.Rating.Value : existedServer.Rating,
                HostingID = server.HostingID != null ? server.HostingID.Value : existedServer.HostingID,
                PlatformID = server.PlatformID != null ? server.PlatformID.Value : existedServer.PlatformID,
                CountryID = server.CountryID != null ? server.CountryID.Value : existedServer.CountryID,
                OwnerID = server.OwnerID != null ? server.OwnerID.Value : existedServer.OwnerID,
            };

            return serverRepository.Update(transferedServer);
        }

        public Server DeleteServer(int id) {
            return serverRepository.Delete(id);
        }

        public IEnumerable<ServerBL> GetAllServers(
            ServerFilterDto filter, 
            ServerSortState? sortState,
            int? page
        ) {
            var servers = mapper.Map<IEnumerable<ServerBL>>(serverRepository.GetAll());

            // Фильтрация
            servers = FilterServers(servers, filter);

            // Сортировка
            if (sortState != null) {
                servers = SortServersByOption(servers, sortState.Value);
            }

            // Пагинация
            if (page != null) {
                servers = PaginationServers(servers, page.Value);
            }
            
            return servers;
        }

        public Server GetServerByIP(string ip) {
            return serverRepository.GetByIP(ip);
        }

        public Server GetServerByName(string name) {
            return serverRepository.GetByName(name);
        }

        public IEnumerable<Server> GetServersByGameName(string gameName) {
            return serverRepository.GetByGameName(gameName);
        }

        public IEnumerable<Server> GetServersByHostingID(int id) {
            return serverRepository.GetByWebHostingID(id);
        }

        public IEnumerable<Server> GetServersByPlatformID(int id) {
            return serverRepository.GetByPlatformID(id);
        }

        public Server GetServerByID(int id) {
            return serverRepository.GetByID(id);
        }

        public IEnumerable<Player> GetServerPlayers(int serverId) {
            if (!IsExistById(serverId))
                throw new ServerNotExistsException("No server with such id");

            return serverRepository.GetPlayersByServerID(serverId);
        }


        public IEnumerable<ServerBL> FilterServers(IEnumerable<ServerBL> servers, ServerFilterDto filter) {
            var filteredServers = servers;

            if (filter.OwnerID != null) {
                filteredServers = filteredServers.Where(s => s.OwnerID == filter.OwnerID);
            }

            if (filter.Status != null) {
                filteredServers = filteredServers.Where(s => s.Status == filter.Status.Value);
            }

            if (!String.IsNullOrEmpty(filter.Name)) {
                filteredServers = filteredServers.Where(s => s.Name.Contains(filter.Name));
            }

            if (!String.IsNullOrEmpty(filter.PlatformName)) {
                var platform = platformRepository.GetByName(filter.PlatformName);

                if (platform != null)
                    filteredServers = filteredServers.Where(p => p.PlatformID == platform.Id);
                else
                    filteredServers = new List<ServerBL>();
            }

            return filteredServers;
        }

        public IEnumerable<ServerBL> SortServersByOption(IEnumerable<ServerBL> servers, ServerSortState sortOrder) {

            IEnumerable<ServerBL> filteredServers;

            if (sortOrder == ServerSortState.NameDesc) {
                filteredServers = servers.OrderByDescending(s => s.Name);
            }
            else if (sortOrder == ServerSortState.IPAsc) {
                filteredServers = servers.OrderBy(s => s.Ip);
            }
            else if (sortOrder == ServerSortState.IPDesc) {
                filteredServers = servers.OrderByDescending(s => s.Ip);
            }
            else if (sortOrder == ServerSortState.GameNameAsc) {
                filteredServers = servers.OrderBy(s => s.GameName);
            }
            else if (sortOrder == ServerSortState.GameNameDesc) {
                filteredServers = servers.OrderByDescending(s => s.GameName);
            }
            else if (sortOrder == ServerSortState.RatingAsc) {
                filteredServers = servers.OrderBy(s => s.Rating);
            }
            else if (sortOrder == ServerSortState.RatingDesc) {
                filteredServers = servers.OrderByDescending(s => s.Rating);
            }
            else if (sortOrder == ServerSortState.PlatformAsc) {
                filteredServers = servers.OrderBy(s => s.Platform.Name);
            }
            else if (sortOrder == ServerSortState.PlatformDesc) {
                filteredServers = servers.OrderByDescending(s => s.Platform.Name);
            }
            else {
                filteredServers = servers.OrderBy(s => s.Name);
            }

            return filteredServers;
        }

        public IEnumerable<ServerBL> PaginationServers(IEnumerable<ServerBL> servers, int page) {
            var pageSize = 10;
            var paginatedServers = servers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return paginatedServers;
        }

        // public IndexViewModel ParseServers(IEnumerable<Server> parsedServers, string name, int? platformId, int page, ServerSortState sortOrder) {

        //     // Параметры пагинации 
        //     int pageSize = 10;
        //     var count = parsedServers.Count();

        //     // фильтрация
        //     parsedServers = FilterServersByName(parsedServers, name, platformId);

        //     // сортировка
        //     parsedServers = SortServersByOption(parsedServers, sortOrder);

        //     // пагинация
        //     parsedServers = PaginationServers(parsedServers, page, pageSize);


        //     // Вывод - формируем модель представления
        //     IndexViewModel viewModel = new IndexViewModel {
        //         PageViewModel = new PageViewModel(count, page, pageSize),
        //         SortViewModel = new SortViewModel(sortOrder),
        //         FilterViewModel = new FilterViewModel(platformRepository.GetAll().ToList(), platformId, name),
        //         Servers = parsedServers.ToList(),
        //         Platforms = platformRepository.GetAll().ToList()
        //     };

        //     return viewModel;
        // }

        public DetailViewModel DetailServer(int serverId) {

            if (serverId > 0) {

                Server server = GetServerByID(serverId);

                if (server != null) {

                    IEnumerable<Player> players = serverRepository.GetPlayersByServerID(serverId);
                    WebHosting webHosting = serverRepository.GetWebHostingByServerId(serverId);
                    Country country = serverRepository.GetCountryByServerId(serverId);

                    DetailViewModel viewModel = new DetailViewModel {
                        Server = server,
                        WebHosting = webHosting,
                        Players = players,
                        Country = country
                    };

                    return viewModel;
                }
            }

            return null;
        }

        public IEnumerable<Server> GetServersByRating(int rating) {
            return serverRepository.GetByRating(rating);
        }

        public void UpdateServerRating(int serverId, int change) {
            Server server = serverRepository.GetByID(serverId);
            server.Rating += change;

            serverRepository.Update(server);
        }

        public IEnumerable<int> GetUserFavoriteServersIds(int userId) {
            return userRepository.GetFavoriteServersByUserId(userId).Select(s => s.Id);
        }


        public bool IsServerExists(Server server) {
            
            Server serverCheckName = serverRepository.GetByName(server.Name);

            if (serverCheckName != null && server.Id != serverCheckName.Id) {
                return true;
            }

            Server serverCheckIP = serverRepository.GetByIP(server.Ip);

            if (serverCheckIP != null && server.Id != serverCheckIP.Id) {
                return true;
            }

            return false;
        }
    }
}





// // Нужна ли вообще эта сортировка?
//             else if (sortOrder == ServerSortState.PlatformAsc) { // особая сортировка (сортировка одной таблицы, относительно другой)

//                 var joinedServerPlatformAsc = servers.Join(platformRepository.GetAll(),
//                                           s => s.PlatformID,
//                                           p => p.Id,
//                                           (s, p) => new {
//                                               Id = s.Id,
//                                               ServerName = s.Name,
//                                               GameVersion = s.GameName,
//                                               Ip = s.Ip,
//                                               PlatformId = s.PlatformID,
//                                               WebHostingId = s.HostingID,
//                                               PlatformName = p.Name,
//                                               Rating = s.Rating
//                                           });

//                 filteredServers = joinedServerPlatformAsc
//                     .OrderBy(j => j.PlatformName)
//                     .Select(j => new Server {
//                         Id = j.Id,
//                         Name = j.ServerName,
//                         GameName = j.GameVersion,
//                         Ip = j.Ip,
//                         HostingID = j.WebHostingId,
//                         PlatformID = j.PlatformId,
//                         Rating = j.Rating
//                     });
//             }
//             else if (sortOrder == ServerSortState.PlatformDesc) { // особая сортировка (сортировка одной таблицы, относительно другой)
//                 var joinedServerPlatformDesc = servers.Join(platformRepository.GetAll(),
//                                           s => s.PlatformID,
//                                           p => p.Id,
//                                           (s, p) => new {
//                                               Id = s.Id,
//                                               ServerName = s.Name,
//                                               GameVersion = s.GameName,
//                                               Ip = s.Ip,
//                                               PlatformId = s.PlatformID,
//                                               WebHostingId = s.HostingID,
//                                               PlatformName = p.Name,
//                                               Rating = s.Rating
//                                           });

//                 filteredServers = joinedServerPlatformDesc
//                     .OrderByDescending(j => j.PlatformName)
//                     .Select(j => new Server {
//                         Id = j.Id,
//                         Name = j.ServerName,
//                         GameName = j.GameVersion,
//                         Ip = j.Ip,
//                         HostingID = j.WebHostingId,
//                         PlatformID = j.PlatformId,
//                         Rating = j.Rating
//                     });
//             }