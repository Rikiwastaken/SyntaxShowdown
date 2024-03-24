using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


public class ErrorObject : MonoBehaviour
{

    private bool waittingforanimation;

    void FixedUpdate()
    {
        if(GameObject.Find("ThatIsWrongImage").GetComponent<ThatisWrongdeplacement>().movetonexttext && waittingforanimation)
        {
            waittingforanimation = false;
            loadnexttext();
        }
    }

    public void ErrorFound()
    {
        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().moveThatIsWrongText = true;
        waittingforanimation = true;
        GameObject.Find("Pageturnsound").GetComponent<AudioSource>().Play();
    }

    void loadnexttext()
    {
        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activedialoguespeaker = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID;
        GameObject.Find("MainConfig").GetComponent<MainConfig>().CurrentHP += 5;
        int CurrentCharacter = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID;
        int CaseID = GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID;

        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindow = 1;

        GameObject.Find("Texte_Nom").GetComponent<NameDisplay>().refreshname(CurrentCharacter); // sert � afficher le bon nom
        GameObject.Find("MainConfig").GetComponent<MainConfig>().appendWitness(CurrentCharacter);
        TextAsset asset = GameObject.Find("MainConfig").GetComponent<MainConfig>().LoadNextDialogue(CurrentCharacter);
        GameObject.Find("Texte").GetComponent<displaytext>().textdoc = asset;
        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().ChangeMusic(asset.name[5], 1); //typechange: 1: Play, 2: Fondu
        Debug.Log(Convert.ToChar((int)Char.GetNumericValue(asset.name[5]) - 1));
        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetext = true;
        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().isdialogue = true;
        GameObject.Find("Texte").GetComponent<displaytext>().Initialisation(); //affiche le bon texte et le bon numero de page
    }
}
