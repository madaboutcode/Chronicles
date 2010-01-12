using System;

namespace Chronicles.Services
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message):base(message) { }

        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}