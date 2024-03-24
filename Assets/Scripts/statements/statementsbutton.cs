using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statementsbutton : MonoBehaviour
{
    public void statementbutton()
    {
        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetextstatements=true;
        if(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().lastactivewindowstatements==0)
        {
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().lastactivewindowstatements = 1;
        }
        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindowstatements = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().lastactivewindowstatements;
    }
}
