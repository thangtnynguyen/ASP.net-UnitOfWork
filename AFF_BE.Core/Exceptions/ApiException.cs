
using AFF_BE.Core.Constants;

namespace AFF_BE.Core.Exceptions
{
    public class ApiException : Exception
    {
        public int Status { get; }
        public object Data { get; }

        public ApiException(string message, int status = HttpStatusCodeConstant.BadRequest, object data = null) : base(message)
        {
            Status = status;
            Data = data;
        }
    }
}