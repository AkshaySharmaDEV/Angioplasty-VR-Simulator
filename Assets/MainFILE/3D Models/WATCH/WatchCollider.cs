using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchCollider : MonoBehaviour
{

    public GameObject UI;
    public GameObject LogoUIImage;

    public float delayInSeconds = 3f;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger collider is touched by one of the objects
        if (other.gameObject.CompareTag("RightWIRE") || other.gameObject.CompareTag("RIghtHANDTrigger"))
        {
            LogoUIImage.SetActive(false);

            UI.SetActive(true);
            StartCoroutine(ActivateObjectCoroutine());



        }
    }

    private System.Collections.IEnumerator ActivateObjectCoroutine()
    {
        yield return new WaitForSeconds(delayInSeconds);

        LogoUIImage.SetActive(true);

        UI.SetActive(false);



    }
}