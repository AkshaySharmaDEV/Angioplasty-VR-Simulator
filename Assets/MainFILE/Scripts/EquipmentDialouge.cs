using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EquipmentDialouge : MonoBehaviour
{

    public DialougeSystem DiaSys;



    public string caption;
    public float revealDuration;



   


    public void CaptionShow()
    {

        
        DiaSys.ShowCaption(caption, revealDuration);
    }

    
}
