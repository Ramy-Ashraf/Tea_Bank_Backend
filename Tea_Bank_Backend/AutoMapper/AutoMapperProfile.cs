using AutoMapper;

namespace tea_bank.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // user
            CreateMap<Models.User, DTOs.UserDTO>();
            CreateMap<DTOs.UserDTO, Models.User>();

        }
    }
}
