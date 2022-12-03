using System.Runtime.Serialization;

namespace Dal
{
    [Serializable]
    internal class NonFoundObjectDo : Exception
    {
        public NonFoundObjectDo()
        {
        }

        public NonFoundObjectDo(string? message) : base(message)
        {
        }

        public NonFoundObjectDo(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NonFoundObjectDo(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}