using UnityEngine;
using UnityEngine.UI;

public class BlackAndWhiteEffect : MonoBehaviour
{
    public RenderTexture inputTexture;  // The input Render Texture
    public RenderTexture outputTexture; // The output Render Texture
    public Material blackAndWhiteMaterial; // The material with black and white shader

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // Set the active Render Texture as the output texture
        RenderTexture.active = outputTexture;

        // Clear the output texture to black
        GL.Clear(true, true, Color.black);

        // Set the input texture to the material
        blackAndWhiteMaterial.SetTexture("_MainTex", inputTexture);

        // Render the input texture to the output texture using the black and white material
        Graphics.Blit(inputTexture, outputTexture, blackAndWhiteMaterial);

        // Set the active Render Texture back to the screen
        RenderTexture.active = null;

        // Blit the output texture to the destination (screen)
        Graphics.Blit(outputTexture, destination);
    }
}
