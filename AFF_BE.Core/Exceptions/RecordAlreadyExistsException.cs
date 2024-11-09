using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AFF_BE.Core.Constants;

namespace AFF_BE.Core.Exceptions
{
    public class RecordAlreadyExistsException : Exception
    {
        public RecordAlreadyExistsException() : base("Record already exists in the database."){ }
        public RecordAlreadyExistsException(string message) : base(message) { }
        public RecordAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
        protected RecordAlreadyExistsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
