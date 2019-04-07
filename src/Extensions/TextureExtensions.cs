using System.IO;
using UnityEngine;

namespace Extras.Extensions
{
    public static class TextureExtensions
    {
        // Saves Texture2D to JPG file
        public static void SaveToJpg(this Texture2D texture, string filePath)
        {
            var data = texture.EncodeToJPG();
            File.WriteAllBytes(filePath, data);
        }

        // Saves Texture2D to PNG file
        public static void SaveToPng(this Texture2D texture, string filePath)
        {
            var data = texture.EncodeToPNG();
            File.WriteAllBytes(filePath, data);
        }
    }
}
