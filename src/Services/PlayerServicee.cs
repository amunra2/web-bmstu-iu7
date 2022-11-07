using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ServerING.Services {

    public interface IPlayerService {
        void AddPlayer(Player player);
        Player DeletePlayer(Player player);
        void UpdatePlayer(Player player);

        Player GetPlayerByID(int id);
        IEnumerable<Player> GetAllPlayers();

        Player GetPlayerByNickname(string nickname);
        IEnumerable<Player> GetPlayersByHoursPlayed(int hoursPlayed);
        IEnumerable<Player> GetPlayersByLastPlayed(DateTime lastPlayed);
    }


    public class PlayerService : IPlayerService {

        private readonly IPlayerRepository playerRepository;

        public PlayerService(IPlayerRepository playerRepository) {
            this.playerRepository = playerRepository;
        }


        private bool IsExist(Player player) {
            return playerRepository.GetAll()
                .Any(item =>
                    item.Nickname == player.Nickname &&
                    item.HoursPlayed == player.HoursPlayed &&
                    item.LastPlayed == player.LastPlayed
                    );
        }


        private bool IsExistById(int id) {
            return playerRepository.GetByID(id) != null;
        }

        public void AddPlayer(Player player) {
            if (IsExist(player))
                throw new Exception("Such player is already exist");

            playerRepository.Add(player);
        }

        public Player DeletePlayer(Player player) {
            if (!IsExistById(player.Id))
                throw new Exception("No such player");

            return playerRepository.Delete(player.Id);
        }

        public IEnumerable<Player> GetAllPlayers() {
            return playerRepository.GetAll();
        }

        public Player GetPlayerByID(int id) {
            return playerRepository.GetByID(id);
        }

        public Player GetPlayerByNickname(string nickname) {
            return playerRepository.GetByNickname(nickname);
        }

        public IEnumerable<Player> GetPlayersByHoursPlayed(int hoursPlayed) {
            return playerRepository.GetByHoursPlayed(hoursPlayed);
        }

        public IEnumerable<Player> GetPlayersByLastPlayed(DateTime lastPlayed) {
            return playerRepository.GetByLastPlayed(lastPlayed);
        }

        public void UpdatePlayer(Player player) {
            if (!IsExistById(player.Id))
                throw new Exception("No such player");

            playerRepository.Update(player);
        }
    }
}
