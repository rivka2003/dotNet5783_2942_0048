using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    public class NonFoundObjectDo : Exception
    {
        public NonFoundObjectDo() : base() { }
        public NonFoundObjectDo(string message) : base(message) { }
        public NonFoundObjectDo(string message, Exception inner) : base(message, inner) { }
        protected NonFoundObjectDo (SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() =>
            $"{Message} does not exist!";
    }

    [Serializable]
    public class  ExistingObjectDo :Exception
    {
        public ExistingObjectDo() : base() { }
        public ExistingObjectDo(string message) : base(message) { }
        public ExistingObjectDo(string message, Exception inner) : base(message, inner) { }
        protected ExistingObjectDo(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() =>
            $"The {Message} is already exist!";
    }

    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }
}