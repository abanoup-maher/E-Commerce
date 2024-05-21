using System.Collections;
using System.Collections.Generic;

namespace Rocket.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorResponse() : base(400)
        {
          
        }
       
    }
}
