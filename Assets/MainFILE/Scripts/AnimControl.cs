using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimControl : MonoBehaviour
{
    

    private Animator anim;
    public Slider slider;
    public Slider slider1;//Assign the UI slider of your scene in this slot 
    public GameObject needlecheckobject;
    public GameObject sheathcheckobject;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(needlecheckobject.activeSelf)
        {
            anim.Play("XRAYCam", -1, slider.normalizedValue);
        }
        else if (sheathcheckobject.activeSelf)
        {
            anim.Play("XRAYCam", -1, slider1.normalizedValue);
        }
    }

}