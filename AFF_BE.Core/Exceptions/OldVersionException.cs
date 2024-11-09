
namespace AFF_BE.Core.Exceptions
{
    public class OldVersionException : Exception
    {
        public OldVersionException() { }
        public OldVersionException(string message) : base(message) { }
        public OldVersionException(string message, Exception inner) : base(message, inner) { }
        protected OldVersionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
