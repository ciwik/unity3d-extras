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

        // Returns a minimal component of vector
        public static float Min(this Vector2 v2) => Mathf.Min(v2.x, v2.y);
        public static float Min(this Vector3 v3) => Mathf.Min(v3.x, Mathf.Min(v3.y, v3.z));

        // Returns a maximal component of vector
        public static float Max(this Vector2 v2) => Mathf.Max(v2.x, v2.y);
        public static float Max(this Vector3 v3) => Mathf.Max(v3.x, Mathf.Max(v3.y, v3.z));

        // Checks that vector length is close to zero
        public static bool IsZero(this Vector2 v2) => v2 == Vector2.zero || Vector2.SqrMagnitude(v2) < float.Epsilon;
        public static bool IsZero(this Vector3 v3) => v3 == Vector3.zero || Vector3.SqrMagnitude(v3) < float.Epsilon;
    }
}
