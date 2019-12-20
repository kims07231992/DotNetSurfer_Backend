using System;

namespace DotNetSurfer_Backend.Core.Exceptions
{
    public class BaseCustomException : Exception
    {
        protected bool _isMessageTaken;

        public BaseCustomException()
        {
        }

        public BaseCustomException(string message)
            : base(message)
        {
            this._isMessageTaken = true;
        }

        public BaseCustomException(string message, Exception inner)
            : base(message, inner)
        {
            this._isMessageTaken = true;
        }

        protected virtual string CustomMessage { get => "An error occurred during processing."; }

        public override string Message => this._isMessageTaken 
            ? base.Message 
            : this.CustomMessage;
    }
}
