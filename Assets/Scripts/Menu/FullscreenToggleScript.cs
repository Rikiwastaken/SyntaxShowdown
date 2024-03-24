using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FullscreenToggleScript : MonoBehaviour
{

    public Toggle toggle;
    public TMP_Dropdown DD;
    public bool isfullscreentoggle;
    private bool isfullscreen;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (toggle.isOn && Screen.fullScreen == false)
        {
            Screen.fullScreen = true;
            isfullscreen = true;
            toggle.isOn = true;
        }
        else if(!toggle.isOn && Screen.fullScreen == true)
        {
            Screen.fullScreen = false;
            isfullscreen = false;
            toggle.isOn = false;
        }
        if (DD != null)
        {
            if (DD.value == 0)
            {
                if(Screen.resolutions.Length !=1920)
                {
                    Screen.SetResolution(1920, 1080, isfullscreen);
                }
            }
            if(DD.value == 1)
            {
                if (Screen.resolutions.Length != 1600)
                {
                    Screen.SetResolution(1600, 900, isfullscreen);
                }
            }
            if (DD.value == 2)
            {
                if (Screen.resolutions.Length != 1280)
                {
                    Screen.SetResolution(1280, 720, isfullscreen);
                }
            }
            if (DD.value == 3)
            {
                if (Screen.resolutions.Length != 640)
                {
                    Screen.SetResolution(640, 360, isfullscreen);
                }
            }
        }
    }
}
