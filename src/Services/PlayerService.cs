using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using ServerING.Exceptions;
using ServerING.DTO;


namespace ServerING.Services {

    public interface IPlayerService {
        Player AddPlayer(PlayerDto playerDto);
        Player UpdatePlayer(PlayerUpdateDto playerDto);
        Player PatchUpdatePlayer(PlayerUpdateSparceDto PlayerSparceDto);
        Player DeletePlayer(int id);

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

        public Player AddPlayer(PlayerDto playerDto) {
            var player = new Player()
            {
                Nickname = playerDto.Nickname,
                HoursPlayed = playerDto.HoursPlayed,
                LastPlayed = playerDto.LastPlayed
            };

            if (IsExist(player))
                throw new PlayerAlreadyExistsException("Player already exists");

            return playerRepository.Add(player);
        }

        public Player DeletePlayer(int id) {
            return playerRepository.Delete(id);
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

        public Player UpdatePlayer(PlayerUpdateDto playerDto) {
            var player = new Player()
            {
                Id = playerDto.Id,
                Nickname = playerDto.Nickname,
                LastPlayed = playerDto.LastPlayed,
                HoursPlayed = playerDto.HoursPlayed
            };

            if (!IsExistById(player.Id))
                throw new PlayerNotExistsException("No player with such id");

            if (IsExist(player))
                throw new PlayerAlreadyExistsException("Player already exists");

            return playerRepository.Update(player);
        }

        public Player PatchUpdatePlayer(PlayerUpdateSparceDto playerSparceDto) {
            if (!IsExistById(playerSparceDto.Id))
                throw new PlayerNotExistsException("No player with such id");

            var dbPlayer = GetPlayerByID(playerSparceDto.Id);

            var player = new Player()
            {
                Id = playerSparceDto.Id,
                Nickname = playerSparceDto.Nickname ?? dbPlayer.Nickname,
                HoursPlayed = playerSparceDto.HoursPlayed?? dbPlayer.HoursPlayed,
                LastPlayed = playerSparceDto.LastPlayed ?? dbPlayer.LastPlayed
            };

            if (IsExist(player))
                throw new PlayerAlreadyExistsException("Player already exists");

            return playerRepository.Update(player);
        }
    }
}
