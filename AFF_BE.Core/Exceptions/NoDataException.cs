using AFF_BE.Core.Constants;

namespace AFF_BE.Core.Exceptions
{
    public class NoDataException : Exception
    {
        public NoDataException() { }
        public NoDataException(string message) : base(message) { }
        public NoDataException(string message, Exception inner) : base(message, inner) { }
        protected NoDataException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
