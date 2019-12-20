using System;

namespace DotNetSurfer_Backend.Core.Exceptions
{
    public class CustomArgumentException : BaseCustomException
    {
        public CustomArgumentException()
        {
        }

        public CustomArgumentException(string message)
            : base(message)
        {
        }

        public CustomArgumentException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected override string CustomMessage { get => "Information format is not supported"; }

        public override string Message => this._isMessageTaken
            ? $"{this.CustomMessage}: {base.Message}"
            : $"{this.CustomMessage}.";
    }
}
