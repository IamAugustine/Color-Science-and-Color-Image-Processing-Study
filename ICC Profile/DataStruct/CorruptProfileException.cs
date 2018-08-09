using System;
using System.Runtime.Serialization;

namespace ICC_Profile.DataStruct
{
    [Serializable]
    internal class CorruptProfileException : Exception
    {
        public CorruptProfileException()
        {
        }

        public CorruptProfileException(string message) : base(message)
        {
        }

        public CorruptProfileException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CorruptProfileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}