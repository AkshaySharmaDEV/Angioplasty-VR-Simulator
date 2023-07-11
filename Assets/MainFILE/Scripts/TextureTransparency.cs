using UnityEngine;

public class RenderTextureTransparency : MonoBehaviour
{
    public RenderTexture targetRenderTexture;
    public Color transparentColor = Color.white;
    public Renderer renderer;

    private void Start()
    {
        if (renderer == null)
        {
            renderer = GetComponent<Renderer>();
        }

        ApplyRenderTextureTransparency();
    }

    private void ApplyRenderTextureTransparency()
    {
        // Create a new RenderTexture with the same dimensions as the original RenderTexture
        RenderTexture modifiedRenderTexture = new RenderTexture(targetRenderTexture.width, targetRenderTexture.height, 0);
        modifiedRenderTexture.Create();

        // Set the modified RenderTexture as the target for rendering
        RenderTexture.active = modifiedRenderTexture;

        // Read the original RenderTexture pixels into a Texture2D
        Texture2D originalTexture = new Texture2D(targetRenderTexture.width, targetRenderTexture.height);
        originalTexture.ReadPixels(new Rect(0, 0, targetRenderTexture.width, targetRenderTexture.height), 0, 0);
        originalTexture.Apply();

        // Get the pixels from the Texture2D
        Color[] originalPixels = originalTexture.GetPixels();

        // Iterate through each pixel
        for (int i = 0; i < originalPixels.Length; i++)
        {
            Color pixelColor = originalPixels[i];

            // Check if the pixel color matches the transparent color
            if (pixelColor.Equals(transparentColor))
            {
                // Set the alpha channel to 0 (fully transparent)
                pixelColor.a = 0;
            }

            // Assign the modified pixel color to the Texture2D
            originalPixels[i] = pixelColor;
        }

        // Assign the modified pixels to the Texture2D
        originalTexture.SetPixels(originalPixels);
        originalTexture.Apply();

        // Assign the modified Texture2D to the renderer's material
        renderer.material.mainTexture = originalTexture;

        // Assign the modified Texture2D to the modified RenderTexture
        Graphics.Blit(originalTexture, modifiedRenderTexture);

        // Assign the modified RenderTexture to the camera
        renderer.material.SetTexture("_MainTex", modifiedRenderTexture);
    }
}
