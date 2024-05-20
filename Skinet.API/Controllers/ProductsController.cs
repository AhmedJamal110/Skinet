using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skinet.API.DTO;
using Skinet.API.Entities;
using Skinet.API.Errors;
using Skinet.API.Helper;
using Skinet.Core.Entities;
using Skinet.Core.Repository;
using Skinet.Core.Specification;

namespace Skinet.API.Controllers
{
	
	public class ProductsController : BaseController
	{
		private readonly IGenericRepository<Product> _productRepo;
		private readonly IGenericRepository<ProductBrand> _brandRepo;
		private readonly IGenericRepository<ProductType> _typeRepo;
		private readonly IMapper _mapper;

		public ProductsController( IGenericRepository<Product> productRepo , 
			IGenericRepository<ProductBrand> brandRepo,
			IGenericRepository<ProductType> typeRepo
			, IMapper mapper)
        {

			_productRepo = productRepo;
		    _brandRepo = brandRepo;
			_typeRepo = typeRepo;
			_mapper = mapper;
		}


		[CachedAttribute(600)]
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ProductToRetrunDto>>> GetAllProducts( [FromQuery]ProductParmSpec parmSpec)
		{

			var Spec = new ProductWithBrandAndTypeSpec(parmSpec);
			var Products = await _productRepo.GetAllWithSpecByAsync(Spec);
			var Mapped = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToRetrunDto>>(Products);

			var SpecForCount = new ProductWithFiltrationForCount(parmSpec);
			var getCountWithFilteration = await _productRepo.GetCountWithSpecByAsync(SpecForCount);

			var standerResponse = new PaginationStanderdResponse<ProductToRetrunDto>(
				parmSpec.PigeSize, parmSpec.PigeIndex, getCountWithFilteration , Mapped);
			return Ok(standerResponse);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductToRetrunDto) , StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ProductToRetrunDto>> GetProductById(int id)
		{
			var Spec = new ProductWithBrandAndTypeSpec(id);
			
			var Product =  await _productRepo.GetByIdWithSpec(Spec);
			if (Product is null)
				return NotFound(new ApiResponse(StatusCodes.Status404NotFound));
			var mapped = _mapper.Map<Product, ProductToRetrunDto>(Product);
			return Ok(mapped);
		}


		[HttpGet("brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
		{

			var productBrand = await _brandRepo.GetAllAsync();
			return Ok(productBrand);

	    }

		[HttpGet("types")]
		public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypes()
		{
			var productTypes = await _typeRepo.GetAllAsync();
			return Ok(productTypes);
		}

	}
}
