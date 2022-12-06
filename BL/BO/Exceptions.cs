﻿using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    public class NonFoundObjectBo : Exception
    {
        public NonFoundObjectBo() : base() { }
        public NonFoundObjectBo(string message) : base(message) { }
        public NonFoundObjectBo(string message, Exception inner) : base(message, inner) { }
        protected NonFoundObjectBo(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() =>
            $@"NonFoundObjectBo: The BO object does not exist!
NonFoundObjectDo: The DO object does not exist! 
            ";
    }

    public class ExistingObjectBo : Exception
    {
        public ExistingObjectBo() : base() { }
        public ExistingObjectBo(string message) : base(message) { }
        public ExistingObjectBo(string message, Exception inner) : base(message, inner) { }
        protected ExistingObjectBo(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() =>
            $@"ExistingObjectBo: The BO object is already exist!
ExistingObjectDo: The DO object is already exist! 
            ";
    }

    public class NotValid : Exception
    {
        public NotValid() : base() { }
        public NotValid(string message) : base(message) { }
        public NotValid(string message, Exception inner) : base(message, inner) { }
        protected NotValid(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() =>
            "NotValid: Not valid input!";
    }

    public class NotInStock : Exception
    {
        public NotInStock() : base() { }
        public NotInStock(string message) : base(message) { }
        public NotInStock(string message, Exception inner) : base(message, inner) { }
        protected NotInStock(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() =>
            "NotInStock: Not in stock!";
    }

    public class AlreadyUpdated : Exception
    {
        public AlreadyUpdated() : base() { }
        public AlreadyUpdated(string message) : base(message) { }
        public AlreadyUpdated(string message, Exception inner) : base(message, inner) { }
        protected AlreadyUpdated(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() =>
            "AlreadyUpdated: The date has already updated!";
    }

    public class InExistingOrder : Exception
    {
        public InExistingOrder() : base() { }
        public InExistingOrder(string message) : base(message) { }
        public InExistingOrder(string message, Exception inner) : base(message, inner) { }
        protected InExistingOrder(SerializationInfo info, StreamingContext context) : base(info, context) { }
        override public string ToString() =>
            "InExistingOrder: The product exists in another order";
    }
}