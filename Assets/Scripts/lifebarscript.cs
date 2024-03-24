using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lifebarscript : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = 100;
    }

    void FixedUpdate()
    {
        slider.value = GameObject.Find("MainConfig").GetComponent<MainConfig>().CurrentHP;
    }

}
