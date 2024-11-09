

namespace AFF_BE.Core.Models.Common
{
    public class ApiResult<T>
    {
        public bool Status { get; set; }

        public string? Message { get; set; }

        public T? Data { get; set; }
    }
}
