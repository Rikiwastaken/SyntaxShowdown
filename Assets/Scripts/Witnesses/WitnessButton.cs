using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WitnessButton : MonoBehaviour
{
    public int newspeakerID;
    private int activeSpeakerID;
    public int advancement;
    public TextMeshProUGUI text;
    public Image Image;

    // Update is called once per frame
    void FixedUpdate()
    {
        float incr = 0.02f;
        activeSpeakerID = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID;
        text.text = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().getname(newspeakerID);
        if(Image.color.a<0.99)
        {
            Image.color= new Color(Image.color.r,Image.color.g,Image.color.b,(Image.color.a+incr));
            text.color = new Color(text.color.r, text.color.g, text.color.b, (text.color.a + incr));
        }
        
    }

    public void ChangeSpeaker()
    {
        if(activeSpeakerID != newspeakerID)
        {
            if(GameObject.Find("MainConfig").GetComponent<MainConfig>().getWitnessProgression(newspeakerID) == 0)
            {
                GameObject.Find("CancelButton").GetComponent<WitnessCancelButton>().cancelWitnessButton();
                GameObject.Find("Pageturnsound").GetComponent<AudioSource>().Play();
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID = newspeakerID;
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activedialoguespeaker = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID;
                
                int CurrentCharacter = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID;
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindow = 1;
                GameObject.Find("Texte_Nom").GetComponent<NameDisplay>().refreshname(CurrentCharacter); // sert � afficher le bon nom
                TextAsset asset = GameObject.Find("MainConfig").GetComponent<MainConfig>().LoadCurrentWitnessDialogue();
                GameObject.Find("Texte").GetComponent<displaytext>().textdoc = asset;
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetext = true;
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().isdialogue = true;
                GameObject.Find("Texte").GetComponent<displaytext>().Initialisation(); //affiche le bon texte et le bon numero de page
            }
            else
            {
                GameObject.Find("CancelButton").GetComponent<WitnessCancelButton>().cancelWitnessButton();
                GameObject.Find("Pageturnsound").GetComponent<AudioSource>().Play();
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID = newspeakerID;
                TextAsset asset = (TextAsset)Resources.Load(GameObject.Find("MainConfig").GetComponent<MainConfig>().GetCurrentWitnessFile());
                GameObject.Find("Texte").GetComponent<displaytext>().textdoc = asset;
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetext = true;
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindow = 1;
                GameObject.Find("Texte").GetComponent<displaytext>().Initialisation(); //affiche le bon texte et le bon numero de page
                GameObject.Find("Texte_Nom").GetComponent<NameDisplay>().refreshname(newspeakerID - 3); // sert � afficher le bon nom
                int CurentCaracter = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID;
            }
        }
    }
}
