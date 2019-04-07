using Extras.Diagnostics.Exceptions;
using Extras.Extensions;

namespace Extras.Diagnostics
{
    public static partial class Guard
    {
        public static void Null(object reference, string message = null)
        {
            if (reference.IsNotNull())
            {
                throw new GuardException(message);
            }
        }
    }
}
