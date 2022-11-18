using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class NonFoundObject : Exception
    {
        public NonFoundObject() : base() { }
        public NonFoundObject(string message) : base(message) { }
        public NonFoundObject(string message, Exception inner) : base(message, inner) { }
        protected NonFoundObject (SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() =>
            "The object does not exist!";
    }

    public class  ExistingObject :Exception
    {
        public ExistingObject() : base() { }
        public ExistingObject(string message) : base(message) { }
        public ExistingObject(string message, Exception inner) : base(message, inner) { }
        protected ExistingObject(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() =>
            "The object is already exist!";
    }
}
