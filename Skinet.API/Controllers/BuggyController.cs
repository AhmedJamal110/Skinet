using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skinet.API.Errors;
using Skinet.Infrastructure.Data.Context;

namespace Skinet.API.Controllers
{

	public class BuggyController : BaseController
	{
		private readonly WarehouseDbContext _context;

		public BuggyController(WarehouseDbContext context )
        {
			_context = context;
		}

        [HttpGet("notfound")]
		public ActionResult GetNotFound()
		{
			var thing = _context.Products.Find(100);
			if(thing is null)
				return NotFound(new ApiResponse(StatusCodes.Status404NotFound));
			return Ok(thing);
		}

		[HttpGet("serverError")]

		public ActionResult GetServerError()
		{
			var thing = _context.Products.Find(100);
			var thingToReturn = thing.ToString();
			
			return Ok(thingToReturn);

		}

		[HttpGet("badRequest")]
		public ActionResult GetBAdRequest()
		{
			return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest));
		}


		[HttpGet("badRequest/{id}")]	
		public ActionResult GetBadRequest(int id)
		{
			return Ok();
		}
	}


}
