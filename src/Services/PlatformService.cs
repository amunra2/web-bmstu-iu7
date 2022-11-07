using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ServerING.Services {
    public interface IPlatformService {
        void AddPlatform(Platform platform);
        Platform DeletePlatform(Platform platform);
        void UpdatePlatform(Platform platform);

        Platform GetPlatformByID(int id);
        IEnumerable<Platform> GetAllPlatforms();

        Platform GetPlatformByName(string name);
        IEnumerable<Platform> GetPlatformByPopularity(ushort popularity);
        IEnumerable<Platform> GetPlatformByCost(int cost);
    }

    public class PlatformService : IPlatformService {

        private readonly IPlatformRepository platformRepository;

        public PlatformService(IPlatformRepository platformRepository) {
            this.platformRepository = platformRepository;
        }


        private bool IsExist(Platform platform) {
            return platformRepository.GetAll()
                .Any(item =>
                    item.Name == platform.Name &&
                    item.Popularity == platform.Popularity &&
                    item.Cost == platform.Cost
                    );
        }


        private bool IsExistById(int id) {
            return platformRepository.GetByID(id) != null;
        }


        public void AddPlatform(Platform platform) {
            if (IsExist(platform))
                throw new Exception("Such platform is already exist");

            platformRepository.Add(platform);
        }

        public Platform DeletePlatform(Platform platform) {
            if (!IsExistById(platform.Id))
                throw new Exception("No such platform");

            return platformRepository.Delete(platform.Id);
        }

        public Platform GetPlatformByID(int id) {
            return platformRepository.GetByID(id);
        }

        public IEnumerable<Platform> GetAllPlatforms() {
            return platformRepository.GetAll();
        }

        public void UpdatePlatform(Platform platform) {
            if (!IsExistById(platform.Id))
                throw new Exception("No such platform");

            platformRepository.Update(platform);
        }

        public Platform GetPlatformByName(string name) {
            return platformRepository.GetByName(name);
        }

        public IEnumerable<Platform> GetPlatformByPopularity(ushort popularity) {
            return platformRepository.GetByPopularity(popularity);
        }

        public IEnumerable<Platform> GetPlatformByCost(int cost) {
            return platformRepository.GetByCost(cost);
        }
    }
}
