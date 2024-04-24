namespace Skinet.API.Errors
{
	public class ApiResponse
	{

		public int StatusCode { get; set; }
		public string? Message { get; set; }


        public ApiResponse(int statusCode , string? message = null )
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageFromStatusCode(statusCode);
        }


         private string? GetDefaultMessageFromStatusCode( int statusCode)
        {
            return statusCode switch
            {
                400 => "A Bad Request , you have made ",
                401 => "Authorized , you are not ",
                404 => " Resources Found , it was not",
                500 => "Errors are the Path to the dark side. Errors lead to anger, anger leads to hate, hate leads to career change",
                _ => null
            };
		}


         //return statuscode switch
         //   {
         //       400 => "Bad Request",
         //       401 => "You Are Not Auth ",
         //       404 => "Resources Not Found",
         //       500 => "Internal Server Error",
         //       _ => null
         //   };



}
}
