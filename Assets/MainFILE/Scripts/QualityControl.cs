using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QualityControl : MonoBehaviour
{
    public void low()
    {
        QualitySettings.SetQualityLevel(1);
    }

    public void med()
    {
        QualitySettings.SetQualityLevel(4);
    }

    public void high()
    {
        QualitySettings.SetQualityLevel(5);
    }
}
