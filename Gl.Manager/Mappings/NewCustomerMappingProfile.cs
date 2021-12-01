using System;
using AutoMapper;
using Gl.Core.Domain;
using Gl.Core.Shared.ModelViews.Customer;

namespace Gl.Manager.Mappings
{
    public class NewCustomerMappingProfile : Profile
    {
        public NewCustomerMappingProfile()
        {
            CreateMap<NewCustomer, Customer>()
                .ForMember(d => d.CreateAt, o => o.MapFrom(x => DateTime.UtcNow));
        }
    }
}