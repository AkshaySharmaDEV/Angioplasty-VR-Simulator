using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReMoveWIREComplete : MonoBehaviour
{
    public GameObject Yconnector;
    public GameObject UIDye;
    public Slider slider;

    public GameObject RemovalWIREHandHighlight;
    public GameObject RemovalWIREUI;
    public GameObject RemovalWIRECompleteUI;

    public GameObject objectToDeActivate;
    public float delayInSeconds = 3f;

    
    public GameObject objectToMove;

    public GameObject DeactivateWIRE;


    private void Start()
    {
        // Set the initial position of the object based on the slider value
        float initialValue = slider.value;
        float initialPosition = Mathf.Lerp(-14f, 69f, initialValue);
        objectToMove.transform.position = new Vector3(objectToMove.transform.position.x, initialPosition, objectToMove.transform.position.z);


      
    }

    // Update is called once per frame
    void Update()
    {

        // Update the position of the object based on the slider value
        float sliderValue = slider.value;
        float newPosition = Mathf.Lerp(-14f, 69f, sliderValue);
        objectToMove.transform.position = new Vector3(objectToMove.transform.position.x, newPosition, objectToMove.transform.position.z);


        

        if (slider.value <= 0.03f)
        {

            RemovalWIREHandHighlight.SetActive(false);
            RemovalWIREUI.SetActive(false);
            RemovalWIRECompleteUI.SetActive(true);


            /*DeactivateWIRE.SetActive(false);*/

            StartCoroutine(ActivateObjectCoroutine());


            
            enabled = false;
        }
    }

    private System.Collections.IEnumerator ActivateObjectCoroutine()
    {
        yield return new WaitForSeconds(delayInSeconds);
        objectToDeActivate.SetActive(false);

        RemovalWIRECompleteUI.SetActive(false);
        Yconnector.SetActive(true);
        UIDye.SetActive(true);


        

    }
}
