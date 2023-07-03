using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;


public class WireSplineController : MonoBehaviour
{
    // [Range(0, 1)]
    // [SerializeField] float fillValue = 0, emptyValue = 0, fillDistance = 0, emptyDistance = 0;

    [Header("Spline Hierarchy")]
    [SerializeField] Spline parent;
    [SerializeField] List<Spline> children = new List<Spline>();
    [SerializeField] int selectedChildIndex = 0;
    Material material;
    Spline spline;

    private void Awake()
    {
    }
    private void Start()
    {
        // material = GetComponentInChildren<MeshRenderer>().material;
        spline = GetComponent<Spline>();
        SetFillValue(0);
        SetEmptyValue(0);
    }

    private void Update()
    {

    }

    public void SetFillValue(float distance)
    {
        float value = distance / spline.Length;
        if (value > 1)
        {
            value = 1;
            if (children.Count > 0) WireSplineManager.SetFillingSpline(
                children[selectedChildIndex], 0);
        } else if (value < 0) 
        {
            value = 0;
            if (parent != null) WireSplineManager.SetFillingSpline(
                parent, parent.Length);
        }
        GetComponentInChildren<MeshRenderer>().material.SetFloat("_Fill", value);
    }

    public void SetEmptyValue(float distance)
    {
        float value = distance / spline.Length;
        if (value > 1)
        {
            value = 1;
            if (children.Count > 0) WireSplineManager.SetEmptyingSpline(
                children[selectedChildIndex], 0);
        } else if (value < 0) 
        {
            value = 0;
            if (parent != null) WireSplineManager.SetEmptyingSpline(
                parent, parent.Length);
        }
        GetComponentInChildren<MeshRenderer>().material.SetFloat("_Empty", value);
    }

    private void OnValidate()
    {
        spline = GetComponent<Spline>();
        foreach (Spline child in children)
        {
            child.nodes[0].Position = spline.nodes[spline.nodes.Count - 1].Position;
            child.nodes[0].Direction = spline.nodes[spline.nodes.Count - 1].Direction;
            child.gameObject.GetComponent<WireSplineController>().parent = this.spline;
        }
    }
}
