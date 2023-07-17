using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialougeSystem : MonoBehaviour
{
    public TextMeshProUGUI captionText;
    private Coroutine currentCoroutine;



    public void ShowCaption(string caption, float revealDuration)
    {
        // Stop any previous coroutine
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        // Start a coroutine to reveal the caption gradually
        currentCoroutine = StartCoroutine(RevealCaption(caption, revealDuration));
    }

    private IEnumerator RevealCaption(string caption, float revealDuration)
    {
        captionText.text = ""; // Clear the text initially

        float revealInterval = revealDuration / caption.Length;
        for (int i = 0; i < caption.Length; i++)
        {
            captionText.text += caption[i]; // Add the next character to the text
            yield return new WaitForSeconds(revealInterval); // Wait for the reveal interval
        }

        currentCoroutine = null;
    }

}
