using UnityEngine;

public class TriggerCheck : MonoBehaviour
{
    public GameObject CorrectPositionUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BalloonTrigger1"))
        {
            // Code to execute when the collision occurs with an object tagged as "BalloonTrigger1"
            Debug.Log("Collision with BalloonTrigger1 detected!");
            CorrectPositionUI.SetActive(true);
            // Additional code or actions can be added here.
        }
        else
        {
            CorrectPositionUI.SetActive(false);
        }

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BalloonTrigger1"))
        {
            // Code to execute when the collision occurs with an object tagged as "BalloonTrigger1"
            Debug.Log("Collision with BalloonTrigger1 detected!");
            CorrectPositionUI.SetActive(true);
            // Additional code or actions can be added here.
        }
        else
        {
            CorrectPositionUI.SetActive(false);
        }

    }



}