using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WitnessCancelButton : MonoBehaviour
{
    public void cancelWitnessButton()
    {
        TextMeshProUGUI text;
        Image image;
        for (int i = 1;i<5;i++)
        {
            image = GameObject.Find("Wintess" + i + "Button").GetComponent<Image>();
            text = GameObject.Find("Wintess" + i + "Button").GetComponentInChildren<TextMeshProUGUI>();
            image.color = new Color(image.color.r, image.color.g, image.color.b,0f);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
        }
        

    }
}
