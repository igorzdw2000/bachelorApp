using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<InvoiceDetail, InvoiceDetailDto>()
                .ForMember(d=>d.Invoice, o=>o.MapFrom(s=>s.Invoice.InvoiceNumber));
            CreateMap<Invoice, InvoiceToReturnDto>()
                .ForMember(c => c.Customer, o => o.MapFrom(s => s.Customer.Name+" "+s.Customer.Surname))
                .ForMember(p => p.Project, o => o.MapFrom(s => s.Project.Name));
            CreateMap<Address, AddressDto>().ReverseMap();
            //CreateMap<Offer, OfferDto>();
        }
    }
}
