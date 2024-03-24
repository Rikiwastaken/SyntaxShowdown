using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchwindow : MonoBehaviour
{
    private int activewindow;
    private int maxwindow;
    private int activewindowstatements;


    public void FixedUpdate()
    {
        maxwindow = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().maxwindow;
        activewindow = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindow;
        activewindowstatements = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindowstatements;
    }

    public void pressleft()
    {
        if(!GameObject.Find("SceneConfig").GetComponent<SceneConfig>().isdialogue)
        {
            if (activewindow > 1)
            {
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindow -= 1;
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetext = true;
                GameObject.Find("Pageturnsound").GetComponent<AudioSource>().Play();
            }
            else
            {
                GameObject.Find("badselectsound").GetComponent<AudioSource>().Play();
            }
        }
    }

    public void pressright()
    {
        if (!GameObject.Find("SceneConfig").GetComponent<SceneConfig>().isdialogue)
        {
            if (activewindow < maxwindow)
            {
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindow += 1;
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetext = true;
                GameObject.Find("Pageturnsound").GetComponent<AudioSource>().Play();
            }
            else
            {
                GameObject.Find("badselectsound").GetComponent<AudioSource>().Play();
            }
        }
    }

    public void pressleftstatements()
    {
        if (activewindowstatements > 1)
        {
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindowstatements -= 1;
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetextstatements = true;
            GameObject.Find("Pageturnsound").GetComponent<AudioSource>().Play();
        }
        else
        {
            GameObject.Find("badselectsound").GetComponent<AudioSource>().Play();
        }
    }

    public void pressrightstatements()
    {
        if (activewindowstatements < 4)
        {
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindowstatements += 1;
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetextstatements = true;
            GameObject.Find("Pageturnsound").GetComponent<AudioSource>().Play();
        }
        else
        {
            GameObject.Find("badselectsound").GetComponent<AudioSource>().Play();
        }
    }
}
