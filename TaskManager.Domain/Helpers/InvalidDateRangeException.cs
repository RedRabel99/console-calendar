using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleCalendar.Domain.Helpers
{
    public class InvalidDateRangeException : Exception
    {
        public InvalidDateRangeException() { }

        public InvalidDateRangeException(string message)
            : base(message) { }

        public InvalidDateRangeException(string message, Exception inner)
            : base(message, inner) { }
    }
}
