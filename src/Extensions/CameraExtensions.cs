using UnityEngine;

namespace Extras.Extensions
{
    public static class CameraExtensions
    {
        // Returns a snapshot of the camera view
        public static Texture2D TakeSnapshot(
            this Camera camera,
            int? width = null,
            int? height = null,
            TextureFormat format = DEFAULT_TEXTURE_FORMAT,
            int colorDepth = DEFAULT_COLOR_DEPTH)
        {
            var pixelWidth = width ?? camera.pixelWidth;
            var pixelHeight = height ?? camera.pixelHeight;

            var renderTexture = new RenderTexture(pixelWidth, pixelHeight, colorDepth);
            var texture = new Texture2D(pixelWidth, pixelHeight, format, false);

            camera.targetTexture = renderTexture;
            camera.Render();
            RenderTexture.active = renderTexture;
            texture.ReadPixels(new Rect(0, 0, pixelWidth, pixelHeight), 0, 0);

            RenderTexture.active = null;
            camera.targetTexture = null;
            GameObject.Destroy(renderTexture);

            return texture;
        }

        // Default values
        private const int DEFAULT_COLOR_DEPTH = 24;
        private const TextureFormat DEFAULT_TEXTURE_FORMAT = TextureFormat.RGB24;
    }
}
