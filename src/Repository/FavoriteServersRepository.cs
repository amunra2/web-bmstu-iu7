using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerING.Repository {
    public class FavoriteServerRepository : IFavoriteServerRepository {

        ///
        private readonly AppDBContent appDBContent;

        public FavoriteServerRepository(AppDBContent appDBContent) {
            this.appDBContent = appDBContent;
        }
        ///

        public void Add(FavoriteServer favoriteServer) {

            try {
                appDBContent.FavoriteServer.Add(favoriteServer);
                appDBContent.SaveChanges();
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("FavoriteServer Add Error");
            }
        }

        public FavoriteServer Delete(int id) {

            try {
                FavoriteServer favoriteServer = appDBContent.FavoriteServer.Find(id);

                if (favoriteServer == null) {
                    return null;
                }
                else {
                    appDBContent.FavoriteServer.Remove(favoriteServer);
                    appDBContent.SaveChanges();

                    return favoriteServer;
                }
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("FavoriteServer Delete Error");
            }
        }

        public void Update(FavoriteServer favoriteServer) {

            try {
                appDBContent.FavoriteServer.Update(favoriteServer);
                appDBContent.SaveChanges();
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("FavoriteServer Update Error");
            }
        }

        public IEnumerable<FavoriteServer> GetAll() {
            return appDBContent.FavoriteServer;
        }

        public FavoriteServer GetByID(int id) {
            return appDBContent.FavoriteServer.Find(id);
        }

        public IEnumerable<FavoriteServer> GetByUserID(int id) {
            return appDBContent.FavoriteServer.Where(fs => fs.UserID == id);
        }

        public IEnumerable<FavoriteServer> GetByServerID(int id) {
            return appDBContent.FavoriteServer.Where(fs => fs.ServerID == id);
        }
    }
}
