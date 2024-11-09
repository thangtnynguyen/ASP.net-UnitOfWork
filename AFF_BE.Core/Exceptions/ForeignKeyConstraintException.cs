using AFF_BE.Core.Constants;

namespace AFF_BE.Core.Exceptions
{
    public class ForeignKeyConstraintException : Exception
    {
        public ForeignKeyConstraintException() { }
        public ForeignKeyConstraintException(string message) : base(message) { }
        public ForeignKeyConstraintException(string message, Exception inner) : base(message, inner) { }
        protected ForeignKeyConstraintException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
