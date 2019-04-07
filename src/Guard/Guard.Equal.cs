using Extras.Diagnostics.Exceptions;

namespace Extras.Diagnostics
{
    public static partial class Guard
    {
        public static void Equal(object actual, object expected, string message = null)
        {
            if (!Equals(actual, expected))
            {
                throw new GuardException(message);
            }
        }
    }
}
