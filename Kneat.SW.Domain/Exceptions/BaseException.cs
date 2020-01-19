using System;

namespace Kneat.SW.Domain.Exceptions
{
    public abstract class BaseException : Exception
    {
        public BaseException(string message) : base(message) { }
    }
}
