using AutoMapper;
using Skinet.API.DTO;
using Skinet.API.Entities;
using Skinet.Core.Entities;
using Skinet.Core.Identity;
using System.Net.Sockets;

namespace Skinet.API.Helper
{
	public class MappingProfile : Profile 
	{

        public MappingProfile()
        {
            CreateMap<Product, ProductToRetrunDto>()
                .ForMember(d => d.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
                .ForMember(d => d.ProductType, O => O.MapFrom(S => S.ProductType.Name))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());


            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

        }
    }
}
