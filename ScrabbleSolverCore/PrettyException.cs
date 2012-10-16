using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrabbleSolver
{
    class PrettyException : Exception
    {
        public PrettyException(string formattedMessage, params object[] args)
            : base(string.Format(formattedMessage, args))
        {
        }
    }
}
