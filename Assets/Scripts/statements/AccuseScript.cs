using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class AccuseScript : MonoBehaviour
{
    public int newspeakerID;
    public TextMeshProUGUI text;
    public bool iswitness;
    public bool isculprit;
    public GameObject GuiltyCanvas;
    public GameObject LifebarCanvas;

    // Update is called once per frame
    void Awake()
    {
        if(iswitness)
        {
            text.text = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().getname(newspeakerID);

        }
    }

    public void FixedUpdate()
    {
        if (GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID == 0)
        {
            isculprit = true;
        }
        else if (GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID == 1 && newspeakerID == 4)
        {
            isculprit = true;
        }
        else if (GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID == 2 && newspeakerID == 6)
        {
            isculprit = true;
        }
        else
        {
            isculprit = false;
        }
    }

    public void Accuse()
    {
        Debug.Log("Zola");
        if(isculprit)
        {
            Debug.Log("Zola2");
            GuiltyCanvas.SetActive(false);
            LifebarCanvas.SetActive(false);
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().loadlastdialogue(2);
        }
        else
        {
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().wrongaccusechoice();
        }
    }

}