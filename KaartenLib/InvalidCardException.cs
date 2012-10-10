using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaartenLib
{
    class InvalidCardException : Exception
    {
        public InvalidCardException() { }

        public InvalidCardException(string message) : base(message) { }

        public InvalidCardException(string message, Exception inner) : base(message, inner) { }
    }
}
