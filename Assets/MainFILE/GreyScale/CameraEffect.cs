using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraEffect : MonoBehaviour
{
    Material material;


    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        material = Resources.Load<Material>("CameraMaterial");

        if (material == null)
        {
            Graphics.Blit(src, dest);
            return;
        }

        Graphics.Blit(src, dest, material);
    }

    private void OnEnable() {
        material = Resources.Load<Material>("CameraMaterial");
    }
}
