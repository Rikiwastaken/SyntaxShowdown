using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class numpage : MonoBehaviour
{
    private int activewindow;
    private int maxwindow;
    private TextMeshProUGUI text;

    void Awake()
    {
        maxwindow = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().maxwindow;
    }
    void FixedUpdate()
    {
        if (text==null)
        {
            text=this.GetComponent<TextMeshProUGUI>();
        }
        if(this.name == "numtexteassess")
        {
            activewindow = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindowstatements;
            text.text = activewindow + "/" + 4;
        }
        else
        {
            maxwindow = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().maxwindow;
            activewindow = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindow;
            text.text = activewindow + "/" + maxwindow;
        }
        
    }
}
