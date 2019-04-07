namespace Extras.Extensions
{
    public static class StringExtensions
    {
        // Checks that a string is null or empty
        public static bool IsEmpty(this string value) => string.IsNullOrEmpty(value);

        // Checks that a string is not null and not empty
        public static bool IsNotEmpty(this string value) => !string.IsNullOrEmpty(value);

        // Checks that a string is null, empty or contains only whitespace characters
        public static bool IsBlank(this string value) => string.IsNullOrWhiteSpace(value);

        // Checks that a string is not null, not empty and contains not only whitespace characters
        public static bool IsNotBlank(this string value) => !string.IsNullOrWhiteSpace(value);
    }
}
