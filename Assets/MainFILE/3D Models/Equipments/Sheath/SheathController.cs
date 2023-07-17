using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheathController : MonoBehaviour
{

    public GameObject sheath;
    public GameObject sheathHighlighter;
    public GameObject uiGuide;
    public GameObject uiInserted;
    public GameObject sheathInAction;
    
    public GameObject CathetherWIRE2;
    public GameObject CathetherWIREHand;
    public GameObject CathetherWIREUI;
    public GameObject CathetherWIRECompletionUI;
    public Slider slider;

    public Animator SheathCUBEObject;
    public GameObject SheathCUBEObjectDisable;

    

    public GameObject CathetherCanvas;
    public GameObject SheathCanvas;


    public GameObject RemovalWIREHandHighlight;
    public GameObject RemovalWIREUI;
    
    


    public GameObject objectToDeActivate;
    public float delayInSeconds = 3f;

    public GameObject ReWIRECanvas;

    public DialougeSystem DiaSys;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sheath"))
        {
            sheath.SetActive(false);
            sheathHighlighter.SetActive(false);
            uiGuide.SetActive(false);
            uiInserted.SetActive(true);
            sheathInAction.SetActive(true);

            CathetherWIREHand.SetActive(true);
            CathetherWIREUI.SetActive(true);
            CathetherWIRE2.SetActive(true);
            

            slider.value = 0.0f;

            SheathCUBEObject.Play("SheathCUBE");

            DiaSys.ShowCaption("Insert the catheter wire with your right hand", 3.0f);






        }
    }

    private void Update()
    {
        if (slider.value >= 0.99f)
        {
            

            CathetherWIREHand.SetActive(false);
            CathetherWIREUI.SetActive(false);
            
            CathetherWIRECompletionUI.SetActive(true);
            uiGuide.SetActive(false);
            uiInserted.SetActive(false);


            StartCoroutine(ActivateObjectCoroutine());

            enabled = false;
        }


        // Check if the "SheathCUBE" animation has finished playing
        if (SheathCUBEObject.GetCurrentAnimatorStateInfo(0).IsName("SheathCUBE") && SheathCUBEObject.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            // Disable the GameObject
            SheathCUBEObjectDisable.SetActive(false);
        }
    }


    private System.Collections.IEnumerator ActivateObjectCoroutine()
    {
        yield return new WaitForSeconds(delayInSeconds);
        objectToDeActivate.SetActive(false);

        DiaSys.ShowCaption("Now, remove the guide wire!", 3.0f);

        CathetherWIRECompletionUI.SetActive(false);
        SheathCanvas.SetActive(false);
        CathetherCanvas.SetActive(false);



        RemovalWIREHandHighlight.SetActive(true);
        RemovalWIREUI.SetActive(true);

        ReWIRECanvas.SetActive(true);

        
        


        

        




    }


    
}
