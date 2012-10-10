using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaartenLib
{
    class NoCardsLeftException : Exception
    {
        public NoCardsLeftException() { }

        public NoCardsLeftException(string message) : base(message) { }

        public NoCardsLeftException(string message, Exception inner) : base(message, inner) { }
    }
}
