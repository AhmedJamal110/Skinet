using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Skinet.API.Errors;
using System.Text.Json;

namespace Skinet.API.Middelware
{
	public class ExceptionMiddelware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddelware> _logger;
		private readonly IHostEnvironment _env;

		public ExceptionMiddelware(RequestDelegate next , ILogger<ExceptionMiddelware> logger , IHostEnvironment env)
        {
			_next = next;
			_logger = logger;
			_env = env;
		}

		public async Task InvokeAsync( HttpContext context)
		{
			
			try
			{
				await _next.Invoke(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = StatusCodes.Status500InternalServerError;

				var reponse = _env.IsDevelopment() ? new ApiExceptionResponse(StatusCodes.Status500InternalServerError, ex.Message, ex.StackTrace.ToString())
					: new ApiExceptionResponse(StatusCodes.Status500InternalServerError);

				var options = new JsonSerializerOptions()
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			
				};

			var responseJson = JsonSerializer.Serialize(reponse, options);

			await context.Response.WriteAsync(responseJson);

		}


	}
	}
}
