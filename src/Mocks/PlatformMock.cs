using ServerING.Interfaces;
using ServerING.Models;
using System.Collections.Generic;
using System.Linq;


namespace ServerING.Mocks {
    public class PlatformMock : MockData, IPlatformRepository {

        public void Add(Platform model) {
            _platforms.Add(model);
        }

        public Platform Delete(int id) {
            Platform platform = _platforms[id - 1];
            _platforms.Remove(platform);

            return platform;
        }

        public IEnumerable<Platform> GetAll() {
            return _platforms;
        }

        public IEnumerable<Platform> GetByCost(int cost) {
            return _platforms.Where(x => x.Cost == cost);
        }

        public Platform GetByID(int id) {
            return _platforms[id - 1];
        }

        public Platform GetByName(string name) {
            return _platforms.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<Platform> GetByPopularity(ushort popularity) {
            return _platforms.Where(_x => _x.Popularity == popularity);
        }

        public void Update(Platform model) {
            Platform plat = _platforms[model.Id - 1];

            plat.Id = model.Id;
            plat.Name = model.Name;
            plat.Popularity = model.Popularity;
            plat.Cost = model.Cost;

            _platforms[model.Id - 1] = plat;
        }
    }
}
