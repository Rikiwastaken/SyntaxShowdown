using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NameDisplay : MonoBehaviour
{

    private int speakerID;


    public TextMeshProUGUI text;

    void Start()
    {
        speakerID = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID;
        refreshname(speakerID);
    }

    void FixedUpdate()
    {
        speakerID = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID;
        if(text.text!= GameObject.Find("SceneConfig").GetComponent<SceneConfig>().getname(speakerID))
        {
            refreshname(speakerID);
        }
    }

    public void refreshname(int speakerID)
    {
        if(speakerID == 9)
        {
            text.text = "";
        }
        else
        {
            text.text = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().getname(speakerID);
        }
    } 
}
