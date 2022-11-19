using Microsoft.EntityFrameworkCore;
using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerING.Repository {
    public class ServerRepository : IServerRepository {

        ///
        private readonly AppDBContent appDBContent;

        public ServerRepository(AppDBContent appDBContent) {
            this.appDBContent = appDBContent;
        }
        ///

        public void Add(Server server) {

            try {
                appDBContent.Server.Add(server);
                appDBContent.SaveChanges();
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("Server Add Error");
            }
        }

        public Server Delete(int id) {

            try {
                Server server = appDBContent.Server.Find(id);

                if (server == null) {
                    return null;
                }
                else {
                    appDBContent.Server.Remove(server);
                    appDBContent.SaveChanges();

                    return server;
                }
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("Server Delete Error");
            }
        }

        public void Update(Server server) {

            try {
                var curServer = appDBContent.Server.FirstOrDefault(x => x.Id == server.Id);
                /*appDBContent.Server.Update(curServer);*/
                appDBContent.Entry(curServer).CurrentValues.SetValues(server);
                appDBContent.SaveChanges();
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("Server Update Error");
            }
        }

        public IEnumerable<Server> GetAll() {
            return appDBContent
                .Server
                .Include(s => s.Country)
                .Include(s => s.Platform)
                .Include(s => s.Hosting)
                .ToList();
        }

        public Server GetByID(int id) {
            return appDBContent.Server.Find(id);
        }

        public IEnumerable<Server> GetByGameVersion(string gameVersion) {
            return appDBContent.Server.Where(s => s.GameName == gameVersion).ToList();
        }

        public Server GetByIP(string ip) {
            return appDBContent.Server.FirstOrDefault(s => s.Ip == ip);
        }

        public Server GetByName(string name) {
            return appDBContent.Server.FirstOrDefault(s => s.Name == name);
        }

        public IEnumerable<Server> GetByPlatformID(int id) {
            return appDBContent.Server.Where(s => s.PlatformID == id).ToList();
        }

        public IEnumerable<Server> GetByWebHostingID(int id) {
            return appDBContent.Server.Where(s => s.HostingID == id).ToList();
        }

        public IEnumerable<Player> GetPlayersByServerID(int id) {
            Server server = appDBContent.Server.FirstOrDefault(s => s.Id == id);

            if (server != null) {
                var playersOnServerIds = appDBContent.ServerPlayer.Where(x => x.ServerID == id).Select(x => x.PlayerID).ToList();

                IEnumerable<Player> players = appDBContent.Player.Where(x => playersOnServerIds.Contains(x.Id)).ToList();

                return players;
            }

            return null;
        }

        public WebHosting GetWebHostingByServerId(int id) {

            if (id > 0) {
                Server server = appDBContent.Server.FirstOrDefault(s => s.Id == id);

                if (server != null) {
                    return appDBContent.WebHosting.FirstOrDefault(w => w.Id == server.HostingID);
                }
            }

            return null;
        }

        public IEnumerable<Server> GetByRating(int rating) {
            return appDBContent.Server.Where(x => x.Rating == rating).ToList();
        }

        public IEnumerable<FavoriteServer> GetByUserID(int id) {
            return appDBContent.FavoriteServer.Where(fs => fs.UserID == id).ToList();
        }

        public Country GetCountryByServerId(int id) {
            if (id > 0) {
                Server server = appDBContent.Server.FirstOrDefault(s => s.Id == id);

                if (server != null) {
                    return appDBContent.Country.FirstOrDefault(c => c.Id == server.CountryID);
                }
            }

            return null;
        }
    }
}
