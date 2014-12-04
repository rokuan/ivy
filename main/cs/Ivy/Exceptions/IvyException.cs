using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ivy.Exceptions
{
    class IvyException : Exception
    {
        public IvyException()
        {

        }

        public IvyException(String message)
            : base(message)
        {

        }
    }
}
