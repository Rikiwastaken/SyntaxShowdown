using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentScript : MonoBehaviour
{
    private int speakertomcompare;
    private int comparewindow;


    public void Present()
    {
        if(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().isdialogue)
        {
            GameObject.Find("badselectsound").GetComponent<AudioSource>().Play();
        }
        else
        {
            string newtext = GameObject.Find("MainConfig").GetComponent<MainConfig>().GetCurrentWitnessFile();
            TextAsset asset = (TextAsset)Resources.Load(newtext);
            Char cara = asset.text[4];
            comparewindow = (int)Char.GetNumericValue(cara);
            cara = asset.text[3];
            speakertomcompare = (int)Char.GetNumericValue(cara);
            if ((GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindow == comparewindow && GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindowstatements + 3 == speakertomcompare))
            {
                GameObject.Find("MainConfig").GetComponent<MainConfig>().CurrentHP += 5;
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activedialoguespeaker = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID;
                int CurrentCharacter = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID;
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindow = 1;
                GameObject.Find("Texte_Nom").GetComponent<NameDisplay>().refreshname(CurrentCharacter); // sert a afficher le bon nom
                GameObject.Find("MainConfig").GetComponent<MainConfig>().appendCurrentWitness();
                TextAsset text = GameObject.Find("MainConfig").GetComponent<MainConfig>().LoadNextDialogue();
                GameObject.Find("Texte").GetComponent<displaytext>().textdoc = text;
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetext = true;
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().isdialogue = true;
                GameObject.Find("Texte").GetComponent<displaytext>().Initialisation(); //affiche le bon texte et le bon numero de page
                GameObject.Find("StatementsCanvas").SetActive(false);
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().ChangeMusic(text.name[5], 1);

            }
            else if(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().caseID != 0)
            {
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activedialoguespeaker = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID;
                int CurrentCharacter = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID;
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindow = 1;
                GameObject.Find("Texte_Nom").GetComponent<NameDisplay>().refreshname(CurrentCharacter); // sert a afficher le bon nom
                asset = (TextAsset)Resources.Load("Mistake2");
                GameObject.Find("Texte").GetComponent<displaytext>().textdoc = asset;
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetext = true;
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().iserrordialogue = true;
                GameObject.Find("Texte").GetComponent<displaytext>().Initialisation(); //affiche le bon texte et le bon numero de page
                GameObject.Find("MainConfig").GetComponent<MainConfig>().CurrentHP -= 20;
                GameObject.Find("StatementsCanvas").SetActive(false);
            }
            else
            {
                GameObject.Find("badselectsound").GetComponent<AudioSource>().Play();
            }
        }
    }
}
