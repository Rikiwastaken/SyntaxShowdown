using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class VolOptions : MonoBehaviour
{
    public UnityEngine.UI.Slider slider;
    public TMPro.TextMeshProUGUI text;
    public bool isSE;

    public int val;
    


    void Awake()
    {
        if(isSE)
        {
            slider.value = GameObject.Find("MainConfig").GetComponent<MainConfig>().SEvol;
        }
        else
        {
            slider.value = GameObject.Find("MainConfig").GetComponent<MainConfig>().musicvol;
        }
    }

    void FixedUpdate()
    {
        if (isSE)
        {
            GameObject.Find("MainConfig").GetComponent<MainConfig>().SEvol = slider.value;
        }
        else
        {
            GameObject.Find("MainConfig").GetComponent<MainConfig>().musicvol = slider.value;
        }
        val = (int)Math.Round(slider.value * 100);
        text.text = val.ToString();


    }
}
