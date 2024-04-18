using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dialoguepagecgt : MonoBehaviour
{

    public GameObject GuiltyCanvas;

    private TextAsset newtext;
    public void changepage()
    {
        int activewindow = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindow;
        int maxwindow = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().maxwindow;
        if (activewindow < maxwindow)
        {
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindow += 1;
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetext = true;
        }
        else
        {


            
             if (GameObject.Find("SceneConfig").GetComponent<SceneConfig>().gameover)
            {
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().resetcase();
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().save();
                SceneManager.LoadScene("MainMenu");
            }
            else if (GameObject.Find("Texte").GetComponent<displaytext>().textdoc.name[3] == '5')
            {
                if(GameObject.Find("Texte").GetComponent<displaytext>().textdoc.name[5] == '1' && GuiltyCanvas.activeSelf==false)
                {
                    GuiltyCanvas.SetActive(true);
                    GameObject.Find("SceneConfig").GetComponent<SceneConfig>().ChangeMusic('d', 1); //typechange: 1: Play, 2: Fondu
                    
                }
                else if(GameObject.Find("Texte").GetComponent<displaytext>().textdoc.name[5] != '1')
                {
                    GameObject.Find("SceneConfig").GetComponent<SceneConfig>().resetcase();
                    GameObject.Find("SceneConfig").GetComponent<SceneConfig>().save();
                    GameObject.Find("SceneConfig").GetComponent<SceneConfig>().caseover();
                    SceneManager.LoadScene("MainMenu");
                }
            }
            else
            {
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().checkend();
                if (!GameObject.Find("SceneConfig").GetComponent<SceneConfig>().casefinished)
                {
                    GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activedialoguespeaker;
                    GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindow = 1;
                    if (GameObject.Find("SceneConfig").GetComponent<SceneConfig>().isdialogue)
                    {
                        if (GameObject.Find("SceneConfig").GetComponent<SceneConfig>().intro)
                        {
                            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID = 4;
                            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activedialoguespeaker = 4;
                            newtext = GameObject.Find("MainConfig").GetComponent<MainConfig>().LoadCurrentWitnessDialogue();
                            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().SetMainIntro(false);
                        }
                        else
                        {
                            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().buttoncooldowncounter = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().buttoncooldown;
                            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activedialoguespeaker;
                            newtext = GameObject.Find("MainConfig").GetComponent<MainConfig>().LoadNextWitness();
                            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().isdialogue = false;
                        }

                    }
                    else
                    {
                        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().buttoncooldowncounter = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().buttoncooldown;
                        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().iserrordialogue = false;
                        newtext = GameObject.Find("MainConfig").GetComponent<MainConfig>().LoadCurrentWitness();
                    }
                    GameObject.Find("Texte").GetComponent<displaytext>().textdoc = newtext;
                    GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetext = true;
                    GameObject.Find("Texte").GetComponent<displaytext>().usetext = false;
                    GameObject.Find("Texte").GetComponent<displaytext>().readyusetext = false;
                    GameObject.Find("Texte").GetComponent<displaytext>().Initialisation(); //affiche le bon texte et le bon numero de page
                    GameObject.Find("SceneConfig").GetComponent<SceneConfig>().save();
                }
            }
        }
        GameObject.Find("Pageturnsound").GetComponent<AudioSource>().Play();
    }
}
