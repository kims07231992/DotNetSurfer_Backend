using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetSurfer_Backend.Core.Exceptions
{
    public class CustomNotFoundException : BaseCustomException
    {
        public CustomNotFoundException()
        {
        }

        public CustomNotFoundException(string message)
            : base(message)
        {
        }

        public CustomNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected override string CustomMessage { get => "Item not found"; }

        public override string Message => this._isMessageTaken
            ? $"{this.CustomMessage}: {base.Message}"
            : $"{this.CustomMessage}.";
    }
}
