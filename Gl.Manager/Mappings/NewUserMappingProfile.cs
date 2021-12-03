using AutoMapper;
using Gl.Core.Domain;
using Gl.Core.Shared.ModelInput.User;
using Gl.Core.Shared.ModelViews.User;

namespace Gl.Manager.Mappings
{
    public class NewUserMappingProfile : Profile
    {
        public NewUserMappingProfile()
        {
            CreateMap<NewUser, User>().ReverseMap();
        }
    }
}