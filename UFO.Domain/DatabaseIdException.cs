namespace UFO.Domain
{
    using System;

    [Serializable]
    public class DatabaseIdException : InvalidOperationException
    {
        public DatabaseIdException()
        {
        }

        public DatabaseIdException(string message)
            : base(message)
        {
        }
    }
}
