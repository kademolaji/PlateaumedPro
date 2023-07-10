using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateaumedPro.Common
{
    public class PlateaumedProException : Exception
    {
        public PlateaumedProException() : base()
        {
            // Noop
        }

        public PlateaumedProException(string message) : base(message)
        {
            // Noop
        }

        public PlateaumedProException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
            // Noop
        }

        public PlateaumedProException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

