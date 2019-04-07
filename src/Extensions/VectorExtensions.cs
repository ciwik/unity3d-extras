using UnityEngine;

namespace Extras.Extensions
{
    public static class VectorExtensions
    {
        // Converts Vector2 to Vector3
        public static Vector3 ToVector3(this Vector2 v2, float z = 0) => new Vector3(v2.x, v2.y, z);

        // Returns a dot product of two vectors
        public static float Dot(this Vector2 v2, Vector2 other) => Vector2.Dot(v2, other);
        public static float Dot(this Vector3 v3, Vector3 other) => Vector3.Dot(v3, other);

        // Returns a cross product of two vectors
        public static Vector3 Cross(this Vector3 v3, Vector3 other) => Vector3.Cross(v3, other);
    }
}
