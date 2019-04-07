using System;

namespace Extras.Diagnostics.Exceptions
{
    public class GuardException : Exception
    {
        public GuardException(string message) : base(message)
        {
        }
    }
}
