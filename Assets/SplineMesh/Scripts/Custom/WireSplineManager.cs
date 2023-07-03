using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class WireSplineManager : MonoBehaviour
{
    [SerializeField] float wireSpeed = 1, wireLength = 1;
    [SerializeField] Spline initialSpline;
    static float fillDistance = 0, emptyDistance = 0;
    static Spline fillingSpline, emptyingSpline;
    // Start is called before the first frame update
    void Start()
    {
        fillDistance = wireLength;
        fillingSpline = initialSpline;
        emptyingSpline = initialSpline;
        WireLengthSetup();
    }

    // Update is called once per frame
    void Update()
    {
        float wireVelocity = Input.GetAxis("Vertical") * wireSpeed;
        fillDistance += wireVelocity;
        emptyDistance += wireVelocity;
        // if(fillingSpline.gameObject.GetComponent<WireSplineController>().GetComponentInParent != null
        // )
        fillingSpline.gameObject
            .GetComponent<WireSplineController>().SetFillValue(fillDistance);
        emptyingSpline.gameObject
            .GetComponent<WireSplineController>().SetEmptyValue(emptyDistance);
    }

    private void WireLengthSetup()
    {
        
        float wireRemaining = wireLength;
        do
        {
            if (fillingSpline.Length > wireRemaining) fillingSpline.gameObject
                .GetComponent<WireSplineController>().SetFillValue(wireRemaining);
            else  fillingSpline.gameObject
                .GetComponent<WireSplineController>().SetFillValue(fillingSpline.Length);

            wireRemaining -= fillingSpline.Length;
        } while (wireRemaining > 0);
    }

    public static void SetFillingSpline(Spline a_spline, float a_distance){
        fillingSpline = a_spline;
        fillDistance = a_distance;
    }

    public static void SetEmptyingSpline(Spline a_spline, float a_distance){
        emptyingSpline = a_spline;
        emptyDistance = a_distance;
    }
}
