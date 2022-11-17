using ServerING.Interfaces;
using ServerING.Models;
using ServerING.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace ServerING.Services {

    public interface IServerService {
        void AddServer(Server server);
        Server DeleteServer(Server server);
        void UpdateServer(Server server);

        Server GetServerByID(int id);
        IEnumerable<Server> GetAllServers();

        Server GetServerByName(string name);
        Server GetServerByIP(string ip);

        IEnumerable<Server> GetServersByGameVersion(string gameVersion);
        IEnumerable<Server> GetServersByHostingID(int id);
        IEnumerable<Server> GetServersByPlatformID(int id);
        IEnumerable<Server> GetServersByRating(int rating);

        IndexViewModel ParseServers(IEnumerable<Server> parsedServers, string name, int? platformId, int page, ServerSortState sortOrder);
        DetailViewModel DetailServer(int serverId);
        IEnumerable<int> GetUserFavoriteServersIds(int userId);

        void AddFavoriteServer(int serverId, int userId);
        void DeleteFavoriteServer(int serverId, int userId);

        bool IsServerExists(Server server);
    }

    public class ServerService : IServerService {

        private readonly IServerRepository serverRepository;
        private readonly IPlatformRepository platformRepository;
        private readonly IUserRepository userRepository;

        public ServerService(IServerRepository serverRepository, IPlatformRepository platformRepository, IUserRepository userRepository) {
            this.serverRepository = serverRepository;
            this.platformRepository = platformRepository;
            this.userRepository = userRepository;
        }


        private bool IsExist(Server server) {
            return serverRepository.GetAll()
                .Any(item =>
                    item.Name == server.Name &&
                    item.GameName == server.GameName &&
                    item.HostingID == server.HostingID &&
                    item.PlatformID == server.PlatformID
                    );
        }


        private bool IsExistById(int id) {
            return serverRepository.GetByID(id) != null;
        }


        public void AddServer(Server server) {
            if (IsExist(server))
                throw new Exception("Such server is already exist");

            serverRepository.Add(server);
        }

        public Server DeleteServer(Server server) {
            if (!IsExistById(server.Id))
                throw new Exception("No such server");

            return serverRepository.Delete(server.Id);
        }

        public IEnumerable<Server> GetAllServers() {
            return serverRepository.GetAll();
        }

        public Server GetServerByIP(string ip) {
            return serverRepository.GetByIP(ip);
        }

        public Server GetServerByName(string name) {
            return serverRepository.GetByName(name);
        }

        public IEnumerable<Server> GetServersByGameVersion(string gameVersion) {
            return serverRepository.GetByGameVersion(gameVersion);
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

        public void UpdateServer(Server server) {
            if (!IsExistById(server.Id))
                throw new Exception("No such server");

            serverRepository.Update(server);
        }


        private IEnumerable<Server> FilterServersByName(IEnumerable<Server> servers, string name, int? platformId) {

            var filteredServers = servers;

            if ((platformId != null) && (platformId > 0)) {
                var joinedServerPlatformAsc = servers.Join(platformRepository.GetAll(),
                                          s => s.PlatformID,
                                          p => p.Id,
                                          (s, p) => new {
                                              Id = s.Id,
                                              ServerName = s.Name,
                                              GameVersion = s.GameName,
                                              Ip = s.Ip,
                                              PlatformId = s.PlatformID,
                                              WebHostingId = s.HostingID,
                                              PlatformName = p.Name,
                                              Rating = s.Rating
                                          });

                filteredServers = joinedServerPlatformAsc
                    .Where(j => j.PlatformId == platformId)
                    .Select(j => new Server {
                        Id = j.Id,
                        Name = j.ServerName,
                        GameName = j.GameVersion,
                        Ip = j.Ip,
                        HostingID = j.WebHostingId,
                        PlatformID = j.PlatformId,
                        Rating = j.Rating
                    });
            }

            if (!String.IsNullOrEmpty(name)) {
                filteredServers = filteredServers.Where(p => p.Name.Contains(name));
            }

            return filteredServers;
        }


        private IEnumerable<Server> SortServersByOption(IEnumerable<Server> servers, ServerSortState sortOrder) {

            IEnumerable<Server> filteredServers;

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
            // Нужна ли вообще эта сортировка?
            else if (sortOrder == ServerSortState.PlatformAsc) { // особая сортировка (сортировка одной таблицы, относительно другой)

                var joinedServerPlatformAsc = servers.Join(platformRepository.GetAll(),
                                          s => s.PlatformID,
                                          p => p.Id,
                                          (s, p) => new {
                                              Id = s.Id,
                                              ServerName = s.Name,
                                              GameVersion = s.GameName,
                                              Ip = s.Ip,
                                              PlatformId = s.PlatformID,
                                              WebHostingId = s.HostingID,
                                              PlatformName = p.Name,
                                              Rating = s.Rating
                                          });

                filteredServers = joinedServerPlatformAsc
                    .OrderBy(j => j.PlatformName)
                    .Select(j => new Server {
                        Id = j.Id,
                        Name = j.ServerName,
                        GameName = j.GameVersion,
                        Ip = j.Ip,
                        HostingID = j.WebHostingId,
                        PlatformID = j.PlatformId,
                        Rating = j.Rating
                    });
            }
            else if (sortOrder == ServerSortState.PlatformDesc) { // особая сортировка (сортировка одной таблицы, относительно другой)
                var joinedServerPlatformDesc = servers.Join(platformRepository.GetAll(),
                                          s => s.PlatformID,
                                          p => p.Id,
                                          (s, p) => new {
                                              Id = s.Id,
                                              ServerName = s.Name,
                                              GameVersion = s.GameName,
                                              Ip = s.Ip,
                                              PlatformId = s.PlatformID,
                                              WebHostingId = s.HostingID,
                                              PlatformName = p.Name,
                                              Rating = s.Rating
                                          });

                filteredServers = joinedServerPlatformDesc
                    .OrderByDescending(j => j.PlatformName)
                    .Select(j => new Server {
                        Id = j.Id,
                        Name = j.ServerName,
                        GameName = j.GameVersion,
                        Ip = j.Ip,
                        HostingID = j.WebHostingId,
                        PlatformID = j.PlatformId,
                        Rating = j.Rating
                    });
            }
            else {
                filteredServers = servers.OrderBy(s => s.Name);
            }

            return filteredServers;
        }


        private IEnumerable<Server> PaginationServers(IEnumerable<Server> servers, int page, int pageSize) {

            var paginatedServers = servers.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return paginatedServers;
        }


        public IndexViewModel ParseServers(IEnumerable<Server> parsedServers, string name, int? platformId, int page, ServerSortState sortOrder) {

            // Параметры пагинации 
            int pageSize = 10;
            var count = parsedServers.Count();

            // фильтрация
            parsedServers = FilterServersByName(parsedServers, name, platformId);

            // сортировка
            parsedServers = SortServersByOption(parsedServers, sortOrder);

            // пагинация
            parsedServers = PaginationServers(parsedServers, page, pageSize);


            // Вывод - формируем модель представления
            IndexViewModel viewModel = new IndexViewModel {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(platformRepository.GetAll().ToList(), platformId, name),
                Servers = parsedServers.ToList(),
                Platforms = platformRepository.GetAll().ToList()
            };

            return viewModel;
        }


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

        private void UpdateServerRating(int serverId, int change) {
            Server server = serverRepository.GetByID(serverId);
            server.Rating += change;

            serverRepository.Update(server);
        }

        public IEnumerable<int> GetUserFavoriteServersIds(int userId) {
            return userRepository.GetFavoriteServersByUserId(userId).Select(s => s.Id);
        }

        public void AddFavoriteServer(int serverId, int userId) {
            UpdateServerRating(serverId, +1);

            userRepository.AddFavoriteServer(serverId, userId);
        }

        public void DeleteFavoriteServer(int serverId, int userId) {
            UpdateServerRating(serverId, -1);

            FavoriteServer favoriteServer = userRepository.GetFavoriteServerByUserAndServerId(userId, serverId);
            userRepository.DeleteFavoriteServer(favoriteServer.Id);
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
