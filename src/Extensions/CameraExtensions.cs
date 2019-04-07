using UnityEngine;

namespace Extras.Extensions
{
    public static class CameraExtensions
    {
        // Returns a snapshot of the camera view
        public static Texture2D TakeSnapshot(
            this Camera camera,
            int width = DEFAULT_TEXTURE_WIDTH,
            int height = DEFAULT_TEXTURE_HEIGHT)
        {
            var renderTexture = new RenderTexture(width, height, COLOR_DEPTH);
            var texture = new Texture2D(width, height, TextureFormat.RGB24, false);

            camera.targetTexture = renderTexture;
            camera.Render();
            RenderTexture.active = renderTexture;
            texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);

            RenderTexture.active = null;
            camera.targetTexture = null;
            GameObject.Destroy(renderTexture);

            return texture;
        }

        // TODO: Remove
        // Default values
        private const int COLOR_DEPTH            = 24;
        private const int DEFAULT_TEXTURE_WIDTH  = 1280;
        private const int DEFAULT_TEXTURE_HEIGHT = 720;
    }
}
