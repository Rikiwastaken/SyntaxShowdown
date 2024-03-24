using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class erroralltextbutton : MonoBehaviour
{
    public void MadeAnMistake()
    {
        if(!GameObject.Find("SceneConfig").GetComponent<SceneConfig>().isdialogue && !GameObject.Find("SceneConfig").GetComponent<SceneConfig>().iserrordialogue && GameObject.Find("SceneConfig").GetComponent<SceneConfig>().buttoncooldowncounter==0 && GameObject.Find("SceneConfig").GetComponent<SceneConfig>().caseID !=0)
        {
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activedialoguespeaker = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID;
            int CurrentCharacter = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID;
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindow = 1;
            GameObject.Find("Texte_Nom").GetComponent<NameDisplay>().refreshname(CurrentCharacter); // sert a afficher le bon nom
            TextAsset asset = (TextAsset)Resources.Load("Mistake1");
            GameObject.Find("Texte").GetComponent<displaytext>().textdoc = asset;
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetext = true;
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().iserrordialogue = true;
            GameObject.Find("Texte").GetComponent<displaytext>().Initialisation(); //affiche le bon texte et le bon numero de page
            GameObject.Find("MainConfig").GetComponent<MainConfig>().CurrentHP -= 10;
        }
        
    }
}
