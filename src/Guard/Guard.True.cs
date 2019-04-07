using Extras.Diagnostics.Exceptions;

namespace Extras.Diagnostics
{
    public static partial class Guard
    {
        public static void True(bool condition, string message = null)
        {
            if (!condition)
            {
                throw new GuardException(message);
            }
        }
    }
}
