using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReRightHandSlider1 : MonoBehaviour
{
    public Slider slider;
    public GameObject PreviousTrigger;
    public GameObject NextTrigger;
    public GameObject LeftHAND;

    private int touchedObjectsCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger collider is touched by one of the objects
        if (other.gameObject.CompareTag("RightWIRE"))
        {

            if(!LeftHAND.activeSelf)
            {
                // Increase the slider value
                slider.value -= 0.015f;

                PreviousTrigger.SetActive(false);
                NextTrigger.SetActive(true);
                gameObject.SetActive(false);
            }
            

            
            
        }
    }
}
