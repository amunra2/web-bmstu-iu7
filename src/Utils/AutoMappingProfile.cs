using AutoMapper;
using ServerING.DTO;
using ServerING.Models;
using ServerING.ModelsBL;

namespace ServerING.Utils {
    public class AutoMappingProfile : Profile {
        public AutoMappingProfile() {			
            // DB <-> BL
            CreateMap<Country, CountryBL>().ReverseMap();
            CreateMap<FavoriteServer, FavoriteServerBL>().ReverseMap();
            CreateMap<Platform, PlatformBL>().ReverseMap();
            CreateMap<Player, PlayerBL>().ReverseMap();
            CreateMap<Server, ServerBL>().ReverseMap();
            CreateMap<ServerPlayer, ServerPlayerBL>().ReverseMap();
            CreateMap<User, UserBL>().ReverseMap();
            CreateMap<WebHosting, WebHostingBL>().ReverseMap();

            // DTO <-> BL
            CreateMap<ServerDtoBase, ServerBL>().ReverseMap();
            CreateMap<ServerDto, ServerBL>().ReverseMap();
            CreateMap<PlayerSparceDto, PlayerBL>().ReverseMap();
        }
    }
}
