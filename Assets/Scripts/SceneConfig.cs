using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Caseimport;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using System;

public class SceneConfig : MonoBehaviour
{  

    [Header("window variables")]
    public int activewindow;
    public int activewindowstatements;
    public int lastactivewindowstatements;
    public int maxwindow;

    [Header("Button cooldown")]
    public int buttoncooldowncounter;
    public int buttoncooldown;

    [Header("case variables")]
    public bool casefinished;
    public List<Case> avancement;
    public int linecounter;
    public int lineerrorcounter;
    public int caseID;
    private string Cname;
    public bool gameover;
    public bool intro;
    public List<int> advlist;
    public int CurrentHP;


    [Header("speaker variables")]
    public int currentWP;
    public int speakerID;
    public int IDtemoignageacomparer;
    public GameObject ShowedCharacter;


    [Header("error variables")]
    public int errorStart;
    public int errorEnd;
    public bool thereisamistake= false;

    [Header("Text variables")]
    private TextAsset text;
    public bool changetext;
    public bool changetextstatements;
    public bool moveThatIsWrongText;

    [Header("dialogue variables")]
    public bool isdialogue;
    public bool iserrordialogue;
    public int activedialoguespeaker;

    [Header("Image")]
    public GameObject ActiveImage;

    [Header("Misc")]
    public float fonduduration;
    public float floatdebug;

    [Header("Music")]
    public AudioSource music;

    private GameObject WitnessButton;
    private GameObject LeftButton;
    private GameObject RightButton;
    private GameObject NumPage;
    private GameObject nextpagedialogueButton;
    private GameObject mistakebutton;
    private GameObject statementscanvas;
    private GameObject statementsbutton;
    private GameObject optionsmenu;

    void Awake()
    {
        changetext = true;
    }

    void Start()
    {
        WitnessButton = GameObject.Find("WintessesButton");
        LeftButton = GameObject.Find("ButtonLeft");
        RightButton = GameObject.Find("ButtonRight");
        NumPage = GameObject.Find("numpage");
        nextpagedialogueButton = GameObject.Find("buttondialogue");
        mistakebutton= GameObject.Find("errorAllTextbutton");
        statementscanvas = GameObject.Find("StatementsCanvas");
        statementsbutton = GameObject.Find("StatementsButton");
        optionsmenu = GameObject.Find("OptionsMenu");
        optionsmenu.SetActive(false);
    }

    void FixedUpdate()
    {
        caseID = GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID;

        if (Input.GetKeyDown("escape"))
        {
            if(optionsmenu.activeSelf==true)
            {
                optionsmenu.SetActive(false);
            }
            else
            {
                optionsmenu.SetActive(true);
            }
        }

        advlist = GetWitnessAdv();

        intro = GameObject.Find("MainConfig").GetComponent<MainConfig>().intro;

        if(buttoncooldowncounter>0)
        {
            buttoncooldowncounter--;
        }

        if(casefinished && GameObject.Find("Texte").GetComponent<displaytext>().textdoc.name[3] != '5')
        {
            loadlastdialogue(1);
        }


        if(!gameover && GameObject.Find("MainConfig").GetComponent<MainConfig>().CurrentHP<=0)
        {
            GameObject.Find("LifebarCanvas").SetActive(false);
            GameObject.Find("MainConfig").GetComponent<MainConfig>().CurrentHP = 1;
            TextAsset asset = (TextAsset)Resources.Load("GameOverDialogue");
            gameover = true;
            GameObject.Find("Texte").GetComponent<displaytext>().textdoc = asset;
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetext = true;
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().iserrordialogue = true;
            GameObject.Find("Texte").GetComponent<displaytext>().Initialisation(); //affiche le bon texte et le bon numero de page
        }

        if(GameObject.Find("BoiteDeDialogue")!=null)
        {
            text = GameObject.Find("Texte").GetComponent<displaytext>().textdoc;
            string nom = text.name;
            if (nom[4] == 'D')
            {
                isdialogue = true;
                iserrordialogue = false;
            }
            else if(nom[0] == 'M' || gameover)
            {
                isdialogue = false;
                iserrordialogue = true;
            }
            else
            {
                isdialogue = false;
                iserrordialogue = false;
            }



            if (isdialogue)
            {
                LeftButton.SetActive(false);
                RightButton.SetActive(false);
                NumPage.SetActive(false);
                WitnessButton.SetActive(false);
                mistakebutton.SetActive(false);
                GameObject.Find("Texte").GetComponent<RectTransform>().sizeDelta = new Vector2(800, 130);
                nextpagedialogueButton.SetActive(true);
                if ((int)Char.GetNumericValue(nom[3])==5 && (int)Char.GetNumericValue(nom[5])==2)
                {
                    statementsbutton.SetActive(false);
                }
                else
                {
                    statementsbutton.SetActive(true);
                }
            }
            else if(iserrordialogue)
            {
                LeftButton.SetActive(false);
                RightButton.SetActive(false);
                NumPage.SetActive(false);
                WitnessButton.SetActive(false);
                mistakebutton.SetActive(false);
                GameObject.Find("Texte").GetComponent<RectTransform>().sizeDelta = new Vector2(800, 130);
                nextpagedialogueButton.SetActive(true);
                if(statementscanvas!=null)
                {
                    statementscanvas.SetActive(false);
                }
                statementsbutton.SetActive(false);
            }
            else
            {
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
                NumPage.SetActive(true);
                mistakebutton.SetActive(true);
                WitnessButton.SetActive(true);
                GameObject.Find("Texte").GetComponent<RectTransform>().sizeDelta = new Vector2(615, 130);
                nextpagedialogueButton.SetActive(false);
                statementsbutton.SetActive(true);
            }

        }
    }

    public string getname(int speakerID)
    {

        if (speakerID == 0)
        {
            Cname = "Dick Shionary";
        }
        else if (speakerID == 1)
        {
            Cname = "Otto Graph";
        }
        else if (speakerID == 2)
        {
            Cname = "Prost Ecution";
        }
        else if (speakerID == 3)
        {
            Cname = "Hillary Vocab";
        }
        else
        {
            if (caseID == 0)
            {
                if (speakerID == 4)
                {
                    Cname = "Otto Graph 1";
                }
                else if (speakerID == 5)
                {
                    Cname = "Otto Graph 2";
                }
                else if (speakerID == 6)
                {
                    Cname = "Otto Graph 3";
                }
                else if (speakerID == 7)
                {
                    Cname = "Otto Graph 4";
                }
                else if (speakerID == 8)
                {
                    Cname = "Client Graph";
                }
            }
            else if (caseID == 1)
            {
                if (speakerID == 4)
                {
                    Cname = "Evan Quished";
                }
                else if (speakerID == 5)
                {
                    Cname = "Li Jisto";
                }
                else if (speakerID == 6)
                {
                    Cname = "Paul Hissman";
                }
                else if (speakerID == 7)
                {
                    Cname = "Reed Ired";
                }
                else if(speakerID == 8)
                {
                    Cname = "Bill Hard";
                }
            }
            else if (caseID == 2)
            {
                if (speakerID == 4)
                {
                    Cname = "Richard Gold";
                }
                else if (speakerID == 5)
                {
                    Cname = "Lou Ker";
                }
                else if (speakerID == 6)
                {
                    Cname = "Tess La";
                }
                else if (speakerID == 7)
                {
                    Cname = "Paul Hissman";
                }
                else if (speakerID == 8)
                {
                    Cname = "Richard Gold";
                }
            }
        }
        return Cname;
    }

    public void loadlastdialogue(int part)
    {
        int CaseID=GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID;
        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindow = 1;
        TextAsset asset = (TextAsset)Resources.Load("C"+CaseID+"W5D"+part);
        GameObject.Find("Texte").GetComponent<displaytext>().textdoc = asset;
        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetext = true;
        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().iserrordialogue = true;
        GameObject.Find("Texte").GetComponent<displaytext>().Initialisation(); //affiche le bon texte et le bon numero de page
    }

    public void checkend()
    {
        bool end = true;
        if (GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID == 1)
        {
            if (GameObject.Find("MainConfig").GetComponent<MainConfig>().getWitnessProgression(4) != 3)
            {
                end = false;
            }
            if (GameObject.Find("MainConfig").GetComponent<MainConfig>().getWitnessProgression(5) != 4)
            {
                end = false;
            }
            if (GameObject.Find("MainConfig").GetComponent<MainConfig>().getWitnessProgression(6) != 3)
            {
                end = false;
            }
            if (GameObject.Find("MainConfig").GetComponent<MainConfig>().getWitnessProgression(7) != 3)
            {
                end = false;
            }
        }
        if (GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID == 0)
        {
            if (GameObject.Find("MainConfig").GetComponent<MainConfig>().getWitnessProgression(4) != 2)
            {
                end = false;
            }
            if (GameObject.Find("MainConfig").GetComponent<MainConfig>().getWitnessProgression(5) != 1)
            {
                end = false;
            }
            if (GameObject.Find("MainConfig").GetComponent<MainConfig>().getWitnessProgression(6) != 0)
            {
                end = false;
            }
            if (GameObject.Find("MainConfig").GetComponent<MainConfig>().getWitnessProgression(7) != 0)
            {
                end = false;
            }
        }
        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().casefinished = end;
    }

    public void SetMainIntro(bool intro)
    {
        GameObject.Find("MainConfig").GetComponent<MainConfig>().intro = intro;
    }

    public List<int> GetWitnessAdv()
    {
        List<int> liste = new List<int>();
        for (int i = 4;i<=7;i++)
        {
            liste.Add(GameObject.Find("MainConfig").GetComponent<MainConfig>().getWitnessProgression(i));
        }
        return liste;
    }

    public void ChangeMusic(char newmusic, int changetype) //typechange: 1: Play, 2: Fondu
    {
        AudioSource chmusic=GetMusicAudioSource(newmusic);
        AudioSource[] musics = GameObject.Find("Musics").GetComponentsInChildren<AudioSource>();
        foreach (AudioSource AS in musics)
        {
            if (changetype == 1)
            {
                if(chmusic==AS)
                {
                    AS.volume = GameObject.Find("MainConfig").GetComponent<MainConfig>().musicvol;
                    AS.Play();
                }
                else
                {
                    AS.volume = 0f;
                }
            }
            else if (changetype == 2)
            {
                if(chmusic!=AS)
                {
                    AS.GetComponentInParent<volmanager>().negfondu = true;
                }
                else if(chmusic ==AS)
                {
                    AS.GetComponentInParent<volmanager>().fonducnt = 0;
                }
                else
                {
                    AS.volume = 0f;
                }
            }
        } 
    }

    public AudioSource GetMusicAudioSource(char MusicID)
    {
        AudioSource Music=music;
        if (MusicID == '2')
        {
            Music = GameObject.Find("Tier1Music").GetComponent<AudioSource>();
        }
        if (MusicID == '3')
        {
            Music = GameObject.Find("Tier2Music").GetComponent<AudioSource>();
        }
        if (MusicID == '4')
        {
            Music = GameObject.Find("Tier3Music").GetComponent<AudioSource>();
        }
        if (MusicID == '5')
        {
            Music = GameObject.Find("Tier4Music").GetComponent<AudioSource>();
        }
        if (MusicID == 'c')
        {
            Music = GameObject.Find("CalmMusic").GetComponent<AudioSource>();
        }
        if (MusicID == 'g')
        {
            Music = GameObject.Find("GoofyMusic").GetComponent<AudioSource>();
        }
        if (MusicID == 's')
        {
            Music = GameObject.Find("SentimentalMusic").GetComponent<AudioSource>();
        }
        if (MusicID == 'i')
        {
            Music = GameObject.Find("IntensityMusic").GetComponent<AudioSource>();
        }
        music = Music;
        return Music;
    }

}

