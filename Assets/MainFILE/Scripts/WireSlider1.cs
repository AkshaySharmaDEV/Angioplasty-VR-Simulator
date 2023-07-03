using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WireSlider1 : MonoBehaviour
{




    public Slider slider;
    public GameObject objectToMove;

    public Slider slider1;
    public GameObject objectToMove1;

    private void Start()
    {
        // Set the initial position of the object based on the slider value
        float initialValue = slider.value;
        float initialPosition = Mathf.Lerp(-14f, 69f, initialValue);
        objectToMove.transform.position = new Vector3(objectToMove.transform.position.x, initialPosition, objectToMove.transform.position.z);


        // Set the initial position of the object based on the slider value
        float initialValue1 = slider1.value;
        float initialPosition1 = Mathf.Lerp(14f, 80f, initialValue1);
        objectToMove1.transform.position = new Vector3(objectToMove1.transform.position.x, initialPosition1, objectToMove1.transform.position.z);
    }

    public void OnSliderValueChanged()
    {
        // Update the position of the object based on the slider value
        float sliderValue = slider.value;
        float newPosition = Mathf.Lerp(-14f, 69f, sliderValue);
        objectToMove.transform.position = new Vector3(objectToMove.transform.position.x, newPosition, objectToMove.transform.position.z);


        // Update the position of the object based on the slider value
        float sliderValue1 = slider1.value;
        float newPosition1 = Mathf.Lerp(14f, 80f, sliderValue1);
        objectToMove1.transform.position = new Vector3(objectToMove1.transform.position.x, newPosition1, objectToMove1.transform.position.z);
    }




}