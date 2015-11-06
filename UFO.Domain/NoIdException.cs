namespace UFO.Domain
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class NoIdException : InvalidOperationException
    {
        public NoIdException()
        {
        }

        public NoIdException(string message)
            : base(message)
        {
        }

        public NoIdException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected NoIdException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
