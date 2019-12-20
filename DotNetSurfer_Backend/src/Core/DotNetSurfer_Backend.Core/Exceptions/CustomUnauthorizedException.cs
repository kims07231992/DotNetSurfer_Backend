using System;

namespace DotNetSurfer_Backend.Core.Exceptions
{
    public class CustomUnauthorizedException : BaseCustomException
    {
        public CustomUnauthorizedException()
        {
        }

        public CustomUnauthorizedException(string message)
            : base(message)
        {
        }

        public CustomUnauthorizedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected override string CustomMessage { get => "Information format is not supported."; }
    }
}
