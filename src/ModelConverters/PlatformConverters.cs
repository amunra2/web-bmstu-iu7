using ServerING.DTO;
using ServerING.ModelsBL;
using ServerING.Services;

namespace ServerING.ModelConverters {
    public class PlatformConverters {

        private readonly IPlatformService platformService;

        public PlatformConverters(IPlatformService platformService) {
            this.platformService = platformService;
        }

        public PlatformBL convertPatch(int id, PlatformFormDto platform) {
            var existedPlatform = platformService.GetPlatformByID(id);

            return new PlatformBL {
                Id = id,
                Name = platform.Name != null ? platform.Name : existedPlatform.Name,
                Cost = platform.Cost != null ? platform.Cost.Value : existedPlatform.Cost,
                Popularity = platform.Popularity != null ? (ushort) platform.Popularity.Value : existedPlatform.Popularity
            };
        }

        public PlatformBL convertPut(int id, PlatformFormDto platform) {
            return new PlatformBL {
                Id = id,
                Name = platform.Name,
                Cost = platform.Cost != null ? platform.Cost.Value : 0,
                Popularity = platform.Popularity != null ? (ushort) platform.Popularity.Value : (ushort) 0
            };
        }

        public PlatformBL convertPost(PlatformFormDto platform) {
            return new PlatformBL {
                Name = platform.Name,
                Cost = platform.Cost.Value,
                Popularity = (ushort) platform.Popularity.Value
            };
        }
    }
}