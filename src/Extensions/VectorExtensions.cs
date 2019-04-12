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
        public static bool IsZero(this Vector2 v2) => v2 == Vector2.zero || Vector2.SqrMagnitude(v2) < 2f * float.Epsilon;
        public static bool IsZero(this Vector3 v3) => v3 == Vector3.zero || Vector3.SqrMagnitude(v3) < 2f * float.Epsilon;

        // Returns copy of vector with changed component
        public static Vector2 X(this Vector2 v2, float x) => new Vector2(x, v2.y);
        public static Vector3 X(this Vector3 v3, float x) => new Vector3(x, v3.y, v3.z);
        public static Vector2 Y(this Vector2 v2, float y) => new Vector2(v2.x, y);
        public static Vector3 Y(this Vector3 v3, float y) => new Vector3(v3.x, y, v3.z);
        public static Vector3 Z(this Vector3 v3, float z) => new Vector3(v3.x, v3.y, z);
        public static Vector3 XY(this Vector3 v3, float x, float y) => new Vector3(x, y, v3.z);
        public static Vector3 XZ(this Vector3 v3, float x, float z) => new Vector3(x, v3.y, z);
        public static Vector3 YZ(this Vector3 v3, float y, float z) => new Vector3(v3.x, y, z);
    }
}
