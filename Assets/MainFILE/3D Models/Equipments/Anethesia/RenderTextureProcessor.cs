using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;

public class RenderTextureProcessor : MonoBehaviour
{
    public RenderTexture sourceTexture;
    public Material baseMapMaterial;
    public Color chromaKeyColor;
    public float tolerance = 0.1f;

    private Material chromaKeyMaterial;

    private void Start()
    {
        // Check if the required components are assigned
        if (sourceTexture == null || baseMapMaterial == null)
        {
            UnityEngine.Debug.LogError("Source texture or base map material is not assigned!");
            enabled = false; // Disable the script to prevent further errors
            return;
        }

        // Check if the shader is valid
        Shader chromaKeyShader = Shader.Find("Hidden/ChromaKeyShader");
        if (chromaKeyShader == null)
        {
            UnityEngine.Debug.LogError("ChromaKeyShader not found!");
            enabled = false; // Disable the script to prevent further errors
            return;
        }

        // Create the chroma key material
        chromaKeyMaterial = new Material(chromaKeyShader);
    }

    private void OnDestroy()
    {
        // Destroy the chroma key material to release resources
        if (chromaKeyMaterial != null)
            Destroy(chromaKeyMaterial);
    }

    private void Update()
    {
        // Check if the required components are assigned
        if (sourceTexture == null || baseMapMaterial == null)
        {
            UnityEngine.Debug.LogError("Source texture or base map material is not assigned!");
            return;
        }

        // Create a temporary render texture
        RenderTexture tempRenderTexture = RenderTexture.GetTemporary(sourceTexture.width, sourceTexture.height, 0, sourceTexture.format, RenderTextureReadWrite.Default);

        // Set the active render texture to the temporary render texture
        RenderTexture.active = tempRenderTexture;

        // Draw the source texture to the temporary render texture
        Graphics.Blit(sourceTexture, tempRenderTexture);

        // Set the active render texture to the source texture
        RenderTexture.active = sourceTexture;

        // Clear the source texture
        GL.Clear(true, true, Color.clear);

        // Set the chroma key material parameters
        chromaKeyMaterial.SetColor("_ChromaKeyColor", chromaKeyColor);
        chromaKeyMaterial.SetFloat("_Tolerance", tolerance);

        // Render the temporary render texture to the source texture with chroma key
        Graphics.Blit(tempRenderTexture, sourceTexture, chromaKeyMaterial);

        // Assign the source texture to the base map material
        baseMapMaterial.mainTexture = sourceTexture;

        // Clean up the temporary render texture
        RenderTexture.ReleaseTemporary(tempRenderTexture);
    }






}
