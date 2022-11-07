using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerING.Repository {
    public class ServerPlayerRepository : IServerPlayerRepository {

        ///
        private readonly AppDBContent appDBContent;

        public ServerPlayerRepository(AppDBContent appDBContent) {
            this.appDBContent = appDBContent;
        }
        ///
        
        public void Add(ServerPlayer serverPlayer) {

            try {
                appDBContent.ServerPlayer.Add(serverPlayer);
                appDBContent.SaveChanges();
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("ServerPlayer Add Error");
            }
        }

        public ServerPlayer Delete(int id) {

            try {
                ServerPlayer serverPlayer = appDBContent.ServerPlayer.Find(id);

                if (serverPlayer == null) {
                    return null;
                }
                else {
                    appDBContent.ServerPlayer.Remove(serverPlayer);
                    appDBContent.SaveChanges();

                    return serverPlayer;
                }
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("ServerPlayer Delete Error");
            }
        }

        public void Update(ServerPlayer serverPlayer) {

            try {
                appDBContent.ServerPlayer.Update(serverPlayer);
                appDBContent.SaveChanges();
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("ServerPlayer Update Error");
            }
        }

        public IEnumerable<ServerPlayer> GetAll() {
            return appDBContent.ServerPlayer;
        }

        public ServerPlayer GetByID(int id) {
            return appDBContent.ServerPlayer.Find(id);
        }

        public IEnumerable<ServerPlayer> GetByPlayerID(int id) {
            return appDBContent.ServerPlayer.Where(sp => sp.PlayerID == id);
        }

        public IEnumerable<ServerPlayer> GetByServerID(int id) {
            return appDBContent.ServerPlayer.Where(sp => sp.ServerID == id);
        }
    }
}
