using ServerING.DTO;
using ServerING.Exceptions;
using ServerING.Interfaces;
using ServerING.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ServerING.Services {
    public interface IPlatformService {
        Platform AddPlatform(PlatformFormDto platform);
        Platform DeletePlatform(int id);
        Platform PatchPlatform(int id, PlatformFormDto platform);
        Platform PutPlatform(int id, PlatformFormDto platform);

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


        private bool IsExist(PlatformFormDto platform) {
            return platformRepository
                .GetAll()
                .Any(item => item.Name == platform.Name);
        }


        private bool IsExistById(int id) {
            return platformRepository.GetByID(id) != null;
        }


        public Platform AddPlatform(PlatformFormDto platform) {
            if (IsExist(platform)) {
                var conflictedId = platformRepository.GetByName(platform.Name).Id;
                throw new PlatformConflictException(conflictedId);
            }

            var transferedPlatform = new Platform {
                Name = platform.Name,
                Cost = platform.Cost.Value,
                Popularity = (ushort) platform.Popularity.Value
            };

            return platformRepository.Add(transferedPlatform);
        }

        public Platform DeletePlatform(int id) {
            return platformRepository.Delete(id);
        }

        public Platform GetPlatformByID(int id) {
            return platformRepository.GetByID(id);
        }

        public IEnumerable<Platform> GetAllPlatforms() {
            return platformRepository.GetAll();
        }

        public Platform PutPlatform(int id, PlatformFormDto platform) {
            if (IsExist(platform)) {
                var conflictedId = platformRepository.GetByName(platform.Name).Id;
                throw new PlatformConflictException(conflictedId);
            }

            if (!IsExistById(id))
                throw null;

            var transferedPlatform = new Platform {
                Id = id,
                Name = platform.Name,
                Cost = platform.Cost != null ? platform.Cost.Value : 0,
                Popularity = platform.Popularity != null ? (ushort) platform.Popularity.Value : (ushort) 0
            };

            return platformRepository.Update(transferedPlatform);
        }

        public Platform PatchPlatform(int id, PlatformFormDto platform) {
            if (IsExist(platform)) {
                var conflictedId = platformRepository.GetByName(platform.Name).Id;
                throw new PlatformConflictException(conflictedId);
            }

            if (!IsExistById(id))
                return null;

            var existedPlatform = GetPlatformByID(id);

            var transferedPlatform = new Platform {
                Id = id,
                Name = platform.Name != null ? platform.Name : existedPlatform.Name,
                Cost = platform.Cost != null ? platform.Cost.Value : existedPlatform.Cost,
                Popularity = platform.Popularity != null ? (ushort) platform.Popularity.Value : existedPlatform.Popularity
            };

            return platformRepository.Update(transferedPlatform);
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
