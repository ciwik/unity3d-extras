using Extras.Diagnostics.Exceptions;
using Extras.Extensions;

namespace Extras.Diagnostics
{
    public static partial class Guard
    {
        public static void NotNull(object reference, string message = null)
        {
            if (reference.IsNull())
            {
                throw new GuardException(message);
            }
        }
    }
}
