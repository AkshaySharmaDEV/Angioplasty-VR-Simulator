
using UnityEngine;

public class RenderTextureToGrayscale : MonoBehaviour
{
    public RenderTexture sourceTexture;

    private void Start()
    {
        if (sourceTexture == null)
        {
            Debug.LogError("Source texture is missing!");
            return;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // Create a new RenderTexture for grayscale conversion
        RenderTexture grayscaleTexture = new RenderTexture(sourceTexture.width, sourceTexture.height, 0);

        // Set the active RenderTexture as the grayscale texture
        RenderTexture.active = grayscaleTexture;

        // Create a temporary Texture2D to store the grayscale pixels
        Texture2D tempTexture = new Texture2D(sourceTexture.width, sourceTexture.height, TextureFormat.RGBA32, false);

        // Read the pixels from the source RenderTexture
        RenderTexture.active = sourceTexture;
        tempTexture.ReadPixels(new Rect(0, 0, sourceTexture.width, sourceTexture.height), 0, 0);
        tempTexture.Apply();

        // Convert the pixels to grayscale
        Color32[] pixels = tempTexture.GetPixels32();
        for (int i = 0; i < pixels.Length; i++)
        {
            byte gray = (byte)(0.299f * pixels[i].r + 0.587f * pixels[i].g + 0.114f * pixels[i].b);
            pixels[i] = new Color32(gray, gray, gray, pixels[i].a);
        }

        // Set the modified pixels back to the temporary texture
        tempTexture.SetPixels32(pixels);
        tempTexture.Apply();

        // Display the grayscale texture on the screen
        Graphics.Blit(tempTexture, destination);

        // Clean up the created objects
        Destroy(grayscaleTexture);
        Destroy(tempTexture);
    }
}
