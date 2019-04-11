namespace Extras.Extensions
{
    public static class NumberExtensions
    {
        // Checks if value is in range
        public static bool Within(this byte value, byte a, byte b) => value >= a && value <= b;
        public static bool Within(this short value, short a, short b) => value >= a && value <= b;
        public static bool Within(this int value, int a, int b) => value >= a && value <= b;
        public static bool Within(this long value, long a, long b) => value >= a && value <= b;
        public static bool Within(this float value, float a, float b) => value >= a && value <= b;
        public static bool Within(this double value, double a, double b) => value >= a && value <= b;
        public static bool Within(this decimal value, decimal a, decimal b) => value >= a && value <= b;

        // Returns proportion
        public static float Rate(this byte c, byte a, byte b) => (c - a) / (float)(b - a);
        public static float Rate(this short c, short a, short b) => (c - a) / (float)(b - a);
        public static float Rate(this int c, int a, int b) => (c - a) / (float)(b - a);
        public static float Rate(this long c, long a, long b) => (c - a) / (float)(b - a);
        public static float Rate(this float c, float a, float b) => (c - a) / (b - a);
        public static float Rate(this double c, double a, double b) => (float)((c - a) / (b - a));
        public static float Rate(this decimal c, decimal a, decimal b) => (float)((c - a) / (b - a));
    }
}
