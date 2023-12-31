﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using SplineMesh;


[ExecuteInEditMode]
[RequireComponent(typeof(Spline))]
public class WireSplineMeshGenerator : MonoBehaviour
{
    private GameObject generated;
    private Spline spline;
    private MeshBender meshBender;

    public Mesh mesh;
    public Material material;
    public Vector3 rotation;
    public Vector3 scale;

    public float startScale = 1, endScale = 1;

    Vector3[] vertices;
    Vector2[] uv;
    int[] triangles;
    public int meshCircumference = 20, meshLength = 20;
    const float TAU = 6.283185f;

    private void OnEnable()
    {
        Init();
#if UNITY_EDITOR
        EditorApplication.update += EditorUpdate;
#endif
    }

    void OnDisable()
    {
#if UNITY_EDITOR
        EditorApplication.update -= EditorUpdate;
#endif
    }

    private void OnValidate()
    {
        Init();
    }

    private void Update()
    {
        EditorUpdate();
    }

    void EditorUpdate()
    {
        Contort();
    }

    private void Contort()
    {
        float nodeDistance = 0;
        int i = 0;
        foreach (var n in spline.nodes)
        {
            float nodeDistanceRate = nodeDistance / spline.Length;
            // float nodeScale = startScale * (1 - nodeDistanceRate);
            float nodeScale = Mathf.Lerp(
                (startScale <= 0 ? 1 : startScale),
                (endScale <= 0 ? 1 : endScale),
                nodeDistanceRate
            );

            n.Scale = new Vector2(nodeScale, nodeScale);
            if (i < spline.curves.Count)
            {
                nodeDistance += spline.curves[i++].Length;
            }
        }

        if (generated != null)
        {
            meshBender.SetInterval(spline, 0, spline.Length);
            meshBender.ComputeIfNeeded();
        }
    }

    private void Init()
    {
        string generatedName = "generated by " + GetType().Name;
        var generatedTranform = transform.Find(generatedName);
        generated = generatedTranform != null ? generatedTranform.gameObject : UOUtility.Create(generatedName, gameObject,
            typeof(MeshFilter),
            typeof(MeshRenderer),
            typeof(MeshBender));

        generated.GetComponent<MeshRenderer>().material = material;

        meshBender = generated.GetComponent<MeshBender>();
        spline = GetComponent<Spline>();

        mesh = new Mesh();
        UpdateMesh();

        meshBender.Source = SourceMesh.Build(mesh)
            .Rotate(Quaternion.Euler(rotation))
            .Scale(scale);
        meshBender.Mode = MeshBender.FillingMode.StretchToInterval;
        meshBender.SetInterval(spline, 0, 0.01f);
    }



    void CreateShape()
    {
        vertices = new Vector3[(meshCircumference+1) * (meshLength + 1)];
        uv = new Vector2[(meshCircumference+1) * (meshLength + 1)];

        for (int i = 0, y = 0; y <= meshLength; y++)
        {
            for (int x = 0; x <= meshCircumference; x++)
            {
                float angle = TAU * (float)x / (float)meshCircumference;
                vertices[i] = new Vector3(Mathf.Cos(angle), y, Mathf.Sin(angle));
                uv[i] = new Vector2((float)x/(float)meshCircumference, (float)y/(float)meshLength);
                i++;
            }
        }

        int vert = 0, tris = 0;
        triangles = new int[6 * meshCircumference * meshLength];
        for (int y = 0; y < meshLength; y++)
        {
            for (int x = 0; x < meshCircumference; x++)
            {
                int vNext = vert + 1;
                triangles[tris] = vert;
                triangles[tris + 1] = vert + meshCircumference + 1;
                triangles[tris + 2] = vNext;
                triangles[tris + 3] = vNext;
                triangles[tris + 4] = vert + meshCircumference + 1;
                triangles[tris + 5] = vNext + meshCircumference + 1;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        CreateShape();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}

