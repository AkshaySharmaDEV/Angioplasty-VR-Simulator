using UnityEngine;
using UnityEngine.UI;

using TMPro;


public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    private float startTime;
    private bool isTiming;

    private void Start()
    {
        // Start timing as soon as the scene starts
        StartTimer();
    }

    private void Update()
    {
        if (isTiming)
        {
            // Calculate the elapsed time
            float elapsedTime = Time.time - startTime;

            // Update the timer text
            timerText.text = elapsedTime.ToString("F2");
        }
    }

    public void StartTimer()
    {
        // Reset the timer and start timing
        startTime = Time.time;
        isTiming = true;
    }

    public void StopTimer()
    {
        // Stop timing
        isTiming = false;
    }
}
