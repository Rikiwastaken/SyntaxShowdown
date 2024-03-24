using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class volmanager : MonoBehaviour
{

    public bool ismusic;
    private float musicvol;
    private float SEvol;
    private float fonduduration;
    public float fonducnt = 100000;
    public bool negfondu;
    public float negfonducnt = 100000;
    public float soundadj;
    public AudioSource source;
    public bool ismainmenu;

    void Awake()
    {
        if (ismainmenu)
        {
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ismainmenu)
        {
            if(SceneManager.GetActiveScene().name == "Test temoignage")
            {
                source.volume = source.volume - 0.02f;
            }
            else if(source.volume <= 0.0f)
            {
                source.volume = soundadj* GameObject.Find("MainConfig").GetComponent<MainConfig>().musicvol;
                source.Play();
            }
            else
            {
                source.volume = soundadj * GameObject.Find("MainConfig").GetComponent<MainConfig>().musicvol;
            }
        }
        else
        {
            if (GameObject.Find("SceneConfig") != null)
            {
                fonduduration = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().fonduduration;
            }
            musicvol = GameObject.Find("MainConfig").GetComponent<MainConfig>().musicvol;
            SEvol = GameObject.Find("MainConfig").GetComponent<MainConfig>().SEvol;

            if (ismusic)
            {

                if (negfondu && source.volume > 0)
                {
                    negfondu = false;
                    negfonducnt = 0;
                }

                if (negfonducnt <= 2*fonduduration)
                {
                    if (source.volume > 0)
                    {
                        source.volume = source.volume - (soundadj * musicvol * negfonducnt / (2*GameObject.Find("SceneConfig").GetComponent<SceneConfig>().fonduduration));
                        negfonducnt++;
                    }
                    else
                    {
                        negfonducnt = 10000;
                    }

                }




                if (fonducnt <= fonduduration)
                {
                    source.volume = soundadj * musicvol * fonducnt / GameObject.Find("SceneConfig").GetComponent<SceneConfig>().fonduduration;
                    fonducnt++;
                }
                else if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "ChooseCaseMenu")
                {
                    source.volume = soundadj * musicvol;
                }
                else if (source == GameObject.Find("SceneConfig").GetComponent<SceneConfig>().music)
                {
                    source.volume = soundadj * musicvol;
                }
                else if (negfonducnt > fonduduration)
                {
                    source.volume = 0f;
                }
            }
            else
            {
                source.volume = soundadj * SEvol;
            }
        }
    }
}
