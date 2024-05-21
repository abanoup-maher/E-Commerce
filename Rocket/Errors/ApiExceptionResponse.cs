namespace Rocket.Errors
{
    public class ApiExceptionResponse:ApiResponse
    {
        public string Details { get; set; }
        public ApiExceptionResponse(int statuscode , string msg=null , string details=null):base(statuscode , msg)
        {
            Details = details;
        }
       
    }
}
