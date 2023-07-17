using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DyeController : MonoBehaviour
{


    public float fillStart = 0.2f;
    public float fillEnd = 1.0f;
    public float animationDuration = 1.5f;


    public float fillStart1 = 0.0f;
    public float fillEnd1 = 1.0f;
    public float animationDuration1 = 2.2f;

    public Renderer renderer;
    public Renderer renderer1;
    private float timer;

    public Animator anim;

    public string rightWireTagName = "RIghtHANDTrigger";

    public GameObject UIInserting;
    public GameObject UIGuide;


    public GameObject Balloon;
    public GameObject PathToHeart;
    public GameObject PathToWireBack;
    public GameObject InflatorConnector;
    public GameObject BalloonWIREHighlightHAND;
    public GameObject BalloonWIREUI;
    public GameObject Inflater;
    public GameObject InflateUI;
    public Slider slider;
    public GameObject BalloonWIRE;


    public GameObject objectToMove;



    private bool isAnimating;

    public DialougeSystem DiaSys;

    public GameObject MagView;

    private void Start()
    {


        DiaSys.ShowCaption("Insert the dye, to see blockage!", 1.5f);


        timer = 0f;
        isAnimating = false;


        

        slider.value = 0.0f;

        // Set the initial position of the object based on the slider value
        float initialValue = slider.value;
        float initialPosition = Mathf.Lerp(-14f, 70.9739f, initialValue);
        objectToMove.transform.position = new Vector3(objectToMove.transform.position.x, initialPosition, objectToMove.transform.position.z);
    }

    public void OnSliderValueChanged()
    {
        // Update the position of the object based on the slider value
        float sliderValue = slider.value;
        float newPosition = Mathf.Lerp(-14f, 70.9739f, sliderValue);
        objectToMove.transform.position = new Vector3(objectToMove.transform.position.x, newPosition, objectToMove.transform.position.z);

    }

    private void OnTriggerEnter(Collider other)
    {
        timer = 0f;

        if (other.CompareTag(rightWireTagName))
        {

            MagView.SetActive(true);

            anim.Play("DyeInsertRotate");

            timer = 0f;
            isAnimating = true;

            UIGuide.SetActive(false);
            
            Balloon.SetActive(true);
            PathToHeart.SetActive(true);
            PathToWireBack.SetActive(true);
            InflatorConnector.SetActive(true);
            BalloonWIREHighlightHAND.SetActive(true);
            BalloonWIREUI.SetActive(true);
            Inflater.SetActive(true);

            

            DiaSys.ShowCaption("Now insert the balloon, you can check balloon inflation before inserting the balloon, just pickup the inflater from my hand and press grip button.", 1.5f);


        }


    }


    


    private void Update()
    {


        // Update the position of the object based on the slider value
        float sliderValue = slider.value;
        float newPosition = Mathf.Lerp(-14f, 70.9739f, sliderValue);
        objectToMove.transform.position = new Vector3(objectToMove.transform.position.x, newPosition, objectToMove.transform.position.z);



        if (slider.value >= 0.99f)
        {
            

            InflateUI.SetActive(true);


            BalloonWIREHighlightHAND.SetActive(false);

            BalloonWIREUI.SetActive(false);

            


        }

        if (isAnimating)
        {
            if (timer < animationDuration)
            {
                timer += Time.deltaTime;
                float t = Mathf.Clamp01(timer / animationDuration);
                float fillValue = Mathf.Lerp(fillStart, fillEnd, t);
                renderer.material.SetFloat("_Fill", fillValue);
                UIInserting.SetActive(false);

                float t1 = Mathf.Clamp01(timer / animationDuration);
                float fillValue1 = Mathf.Lerp(fillStart1, fillEnd1, t1);
                renderer1.material.SetFloat("_Fill", fillValue1);
                UIInserting.SetActive(true);
            }
            else
            {
                isAnimating = false;
                UIInserting.SetActive(false);

                renderer.material.SetFloat("_Fill", fillStart);
                renderer1.material.SetFloat("_Fill", fillStart1);

            }

        }



    }


    

   

    

    
}
