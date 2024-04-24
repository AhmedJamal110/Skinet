using AutoMapper;
using Skinet.API.DTO;
using Skinet.API.Entities;

namespace Skinet.API.Helper
{
	public class ProductPictureUrlResolver : IValueResolver<Product, ProductToRetrunDto, string>
	{
		private readonly IConfiguration _configuration;

		public ProductPictureUrlResolver(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public string Resolve(Product source, ProductToRetrunDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
				return _configuration["ApiBaseUrl"] + source.PictureUrl;

			return string.Empty;
		}
	}
}
