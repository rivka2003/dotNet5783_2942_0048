using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    internal class ExistingObjectDo : Exception
    {
        public ExistingObjectDo()
        {
        }

        public ExistingObjectDo(string? message) : base(message)
        {
        }

        public ExistingObjectDo(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ExistingObjectDo(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}