using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class SplineAnimator : MonoBehaviour
{
    [SerializeField] float animationDuration = .4f;
    Spline spline;
    List<List<Vector3>> initials = new List<List<Vector3>>(), destinations = new List<List<Vector3>>();
    void Start()
    {
        foreach (Transform child in transform)
        {
            List<Vector3> initial = new List<Vector3>(), dest = new List<Vector3>();
            spline = child.gameObject.GetComponent<Spline>();
            foreach (SplineNode n in spline.nodes)
            {
                initial.Add(n.Position);
                dest.Add(n.Position + n.Up);
            }
            initials.Add(initial);
            destinations.Add(dest);
        }
    }

    void Update()
    {
        float t = Mathf.PingPong(Time.time, animationDuration) / animationDuration;
        int counter = 0;
        foreach (Transform child in transform)
        {
            if (counter > 1)
            {
                spline = child.gameObject.GetComponent<Spline>();
                for (int i = 0; i < spline.nodes.Count; i++)
                {
                    spline.nodes[i].Position = Vector3.
                        Lerp(initials[counter][i], destinations[counter][i], t);
                }
            }
            counter++;
        }
    }
}
