using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionsmenubutton : MonoBehaviour
{
    public GameObject optionsmenu;
    public Toggle fullscreentoggle;
    public void optionsbutton()
    {
        optionsmenu.SetActive(true);
        if(Screen.fullScreen==true)
        {
            fullscreentoggle.isOn = true;
        }
        else
        {
            fullscreentoggle.isOn = false;
        }
    }
}
