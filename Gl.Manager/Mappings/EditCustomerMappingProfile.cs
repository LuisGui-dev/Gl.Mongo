using AutoMapper;
using Gl.Core.Domain;
using Gl.Core.Shared.ModelInput.Customer;
using Gl.Core.Shared.ModelViews.Customer;

namespace Gl.Manager.Mappings
{
    public class EditCustomerMappingProfile : Profile
    {
        public EditCustomerMappingProfile()
        {
            CreateMap<EditCustomer, Customer>();
            // .ForMember(d => d.LastUpdate, o => o.MapFrom(x => DateTime.UtcNow));
        }
    }
}