using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class InflaterController : MonoBehaviour
{
    

   
    public SteamVR_Input_Sources inputSource;
    public SteamVR_Action_Single gripAction;
    public Slider slider;

    public Animator animator;
    public Animator animator1;


    public static float gripValue;

    public SkinnedMeshRenderer skinnedMeshRenderer;

    public SkinnedMeshRenderer PledgeskinnedMeshRenderer;
    public SkinnedMeshRenderer PledgeskinnedMeshRenderer1;


    public GameObject targetObject;


    private float originalValue; // The original value between 80 and 100
    private float mappedValue;


    private int caseV = 0;


    public GameObject BalloonInflated;

    // Start is called before the first frame update
    void Start()
    {


        animator.speed = 0;
        animator1.speed = 0;

        

    }

    float shapeKeyValue1;

    // Update is called once per frame
    void Update()
    {


        gripValue = gripAction.GetAxis(inputSource);
        slider.value = gripValue;

        animator.Play("InflaterINOUT", -1, slider.normalizedValue);
        animator1.Play("InfalterPointer", -1, slider.normalizedValue);

        // Update the shape key value based on the slider value
        float shapeKeyValue = Mathf.Lerp(100f, 80f, slider.value);
        skinnedMeshRenderer.SetBlendShapeWeight(0, shapeKeyValue);

        


        // Clamping the original value to make sure it stays within the original range
        float clampedValue = Mathf.Clamp(shapeKeyValue, 80f, 100f);

        // Mapping the clamped value to the target range
        mappedValue = Mathf.Lerp(0f, 60f, (clampedValue - 80f) / (100f - 80f));

        if (targetObject != null)
        {
            if (targetObject.activeSelf && caseV == 0)
            {
               

                
                PledgeskinnedMeshRenderer.SetBlendShapeWeight(1, mappedValue);

                PledgeskinnedMeshRenderer1.SetBlendShapeWeight(1, mappedValue);

                if (mappedValue == 0.0f)
                {
                    caseV = 1;
                    BalloonInflated.SetActive(true);
                }
            }
            
        }

    }
}
