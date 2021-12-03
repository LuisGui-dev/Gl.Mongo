using AutoMapper;
using Gl.Core.Domain;
using Gl.Core.Shared.ModelInput.Customer;
using Gl.Core.Shared.ModelViews.Customer;

namespace Gl.Manager.Mappings
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerView>().ReverseMap();
            CreateMap<Customer, EditCustomer>().ReverseMap();
        }
    }
}