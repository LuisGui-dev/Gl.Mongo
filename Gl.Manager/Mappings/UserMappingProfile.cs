using AutoMapper;
using Gl.Core.Domain;
using Gl.Core.Shared.ModelViews.User;

namespace Gl.Manager.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserView>().ReverseMap();
            CreateMap<User, UserLogin>().ReverseMap();
        }
    }
}