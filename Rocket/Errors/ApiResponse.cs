namespace Rocket.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Msg { get; set; }
        public ApiResponse(int statuscode, string msg = null)
        {
            StatusCode = statuscode;
            Msg = msg ?? GetDefaultMessageForStatusCode(statuscode);
        }
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "You Are Not Authorized",
                404 => "No Response Found",
                500 => "Server Error",
                _ => null
            };
        }
    }
}
