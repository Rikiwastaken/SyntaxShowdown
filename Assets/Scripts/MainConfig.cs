using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using UnityEditor;
using Caseimport;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainConfig : MonoBehaviour
{

    public float SEvol;
    public float musicvol;
    public int caseID;
    public int caseprogression;
    public List<Case> save;
    public int resolution;
    public int fullscreen;

    public bool loadallowed = false;
    public bool intro = false;
    public int CurrentHP;

    void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
        DontDestroyOnLoad(this);
        Application.targetFrameRate = 60;
        save = new List<Case>(); // Initialisation de base sans sauvegarde
        Case Case0 = new Case(4);
        save.Add(Case0);
        Case Case1 = new Case(4);
        save.Add(Case1);
        CurrentHP = 100;
        LoadJSON();
        SaveJSON();
        if (resolution == 0)
        {
            if (Screen.resolutions.Length != 1920)
            {
                Screen.SetResolution(1920, 1080, fullscreen==1);
            }
        }
        if (resolution == 1)
        {
            if (Screen.resolutions.Length != 1600)
            {
                Screen.SetResolution(1600, 900, fullscreen==1);
            }
        }
        if (resolution == 2)
        {
            if (Screen.resolutions.Length != 1280)
            {
                Screen.SetResolution(1280, 720, fullscreen==1);
            }
        }
        if (resolution == 3)
        {
            if (Screen.resolutions.Length != 640)
            {
                Screen.SetResolution(640, 360, fullscreen==1);
            }
        }
    }

    public void UnlockAllCases()
    {
        caseprogression =2;
    }

    public void CaseOver()
    {
        if (caseID == caseprogression)
        {caseprogression += 1;}
    }

    public int SpeakerToWitnessConversion(int SpeakerID)
    {
        return SpeakerID - 4;
    }

    public void appendCurrentWitness()
    {
        Debug.Log(save[caseID].characters[SpeakerToWitnessConversion(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID)]);
        save[caseID].characters[SpeakerToWitnessConversion(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID)]++;
        Debug.Log(SpeakerToWitnessConversion(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID));
        Debug.Log(save[caseID].characters[SpeakerToWitnessConversion(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID)]);
    }

    public void appendWitness(int SpeakerID)
    {
        Debug.Log(save[caseID].characters[SpeakerToWitnessConversion(SpeakerID)]);
        save[caseID].characters[SpeakerToWitnessConversion(SpeakerID)]++;
        Debug.Log(SpeakerToWitnessConversion(SpeakerID));
        Debug.Log(save[caseID].characters[SpeakerToWitnessConversion(SpeakerID)]);
    }

    public TextAsset LoadNextDialogue()
    {
        GameObject SceneConf = GameObject.Find("SceneConfig");
        String newtextname= "C" + caseID + "W" + (SpeakerToWitnessConversion(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID) + 1) + "D" + (getCurrentWitnessProgression() + 1);
        TextAsset newtext = (TextAsset)Resources.Load(newtextname);
        return newtext;
    }

    public TextAsset LoadNextDialogue(int SpeakerID)
    {
        GameObject SceneConf = GameObject.Find("SceneConfig");
        String newtextname = "C" + caseID + "W" + (SpeakerToWitnessConversion(SpeakerID) + 1) + "D" + (getCurrentWitnessProgression() + 1);
        TextAsset newtext = (TextAsset)Resources.Load(newtextname);
        return newtext;
    }

    public TextAsset LoadNextWitness()
    {
        GameObject SceneConf = GameObject.Find("SceneConfig");
        String newtextname = "C" + caseID + "W" + (SpeakerToWitnessConversion(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID) + 1) + "P" + (getCurrentWitnessProgression() + 1);
        TextAsset newtext = (TextAsset)Resources.Load(newtextname);
        return newtext;
    }

    public TextAsset LoadCurrentWitness()
    {
        GameObject SceneConf = GameObject.Find("SceneConfig");
        String newtextname = "C" + caseID + "W" + (SpeakerToWitnessConversion(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID) + 1) + "P" + (getCurrentWitnessProgression()+1);
        TextAsset newtext = (TextAsset)Resources.Load(newtextname);
        return newtext;
    }

    public TextAsset LoadCurrentWitnessDialogue()
    {
        GameObject SceneConf = GameObject.Find("SceneConfig");
        String newtextname = "C" + caseID + "W" + (SpeakerToWitnessConversion(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID) + 1) + "D" + (getCurrentWitnessProgression() + 1);
        TextAsset newtext = (TextAsset)Resources.Load(newtextname);
        return newtext;
    }

    public TextAsset LoadCurrentWitness2()
    {
        GameObject SceneConf = GameObject.Find("SceneConfig");
        String newtextname = GetCurrentWitnessFile();
        TextAsset newtext = (TextAsset)Resources.Load(newtextname);
        return newtext;
    }

    public TextAsset LoadNextWitness(int SpeakerID)
    {
        GameObject SceneConf = GameObject.Find("SceneConfig");
        String newtextname = "C" + caseID + "W" + (SpeakerToWitnessConversion(SpeakerID) + 1) + "P" + (getCurrentWitnessProgression() + 1);
        TextAsset newtext = (TextAsset)Resources.Load(newtextname);
        return newtext;
    }

    public int getCurrentWitnessProgression()
    {
        return save[caseID].characters[SpeakerToWitnessConversion(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID)];
    }

    public int getWitnessProgression(int SpeakerID)
    {
        return save[caseID].characters[SpeakerToWitnessConversion(SpeakerID)];
    }

    public void LoadJSON()
    {
        // Nous avons un formalisme dans notre JSON,
        /*
        [
            caseprogression, -> int
            volumemusic, -> float
            volumesound, -> float
            resolution -> int
            fullscreen -> int
            [ // Save
                [ case0 -> Case or null
                health, -> int
                [ 
                    int,
                    int,
                    int,
                    int,
                ]
                ],
            ]
            resolution -> int (pas encore implémenté)
            fullscreen -> int (pas encore implémenté)
        ]
        */
        //Nous allons donc load tout en verifiant que la sauvegarde n'est pas corrompue
        if (File.Exists(Application.persistentDataPath + "/data.json"))
        {
            StreamReader sr = new StreamReader(Application.persistentDataPath + "/data.json");
            string json = sr.ReadToEnd();
            int state = 0; // States: 0 Default, 1 SearchCP, 2 SearchVM, 3 SearchVS, 4 SearchL, 5 SearchC, 6 SearchV, 7 SearchCh, 8 SearchChs, 9 SearchEndC, 10 SearchSepC, 11 SearchEndDefault, 12 NoSearch, 13 SearchRes, 14 SearchFS
            List<float> temps = new List<float>();
            temps.Add(0); //cp
            temps.Add(0); //vm
            temps.Add(0); //vs
            temps.Add(0); //res
            temps.Add(0); //fs
            int mantisse_counter = 0;
            bool mantisse_enabler = false;
            List<Case> l=new List<Case>();
            Case currentCase = new Case();
            int buffer_character = 0;
            for (int readpointer = 0; readpointer<json.Length; readpointer ++)
            {
                if ((state==0 || state==4 || state == 7 || state == 5 ) && json[readpointer]=='[')
                {
                    if (state == 5)
                    {
                        currentCase = new Case();
                    }
                    state++;
                }
                else if ((state==1 || state==2 || state==6 || state==13 ) && json[readpointer]==',')
                {
                    state++;
                    mantisse_counter=0;
                    mantisse_enabler=false;
                }
                else if ( state==3  && json[readpointer]==',')
                {
                    state = 13;
                    mantisse_counter=0;
                    mantisse_enabler=false;
                }
                else if ( state==14  && json[readpointer]==',')
                {
                    state = 4;
                    mantisse_counter=0;
                    mantisse_enabler=false;
                }
                else if ((state==1 || state==2 || state==3) && json[readpointer]>='0' && json[readpointer]<='9')
                {
                    if (!mantisse_enabler)
                    {
                        temps[state-1]= temps[state-1] * 10 + json[readpointer] - '0';
                    }
                    else
                    {
                        mantisse_counter++;
                        temps[state-1]= temps[state-1] + (json[readpointer] - '0') * (float) Math.Pow(10 ,(- mantisse_counter));
                    }
                }
                else if ((state==1 || state==2 || state==3) && json[readpointer]=='.')
                {
                    mantisse_enabler=true;
                }
                else if ((state==6) && json[readpointer]>='0' && json[readpointer]<='9')
                {
                    currentCase.healthpoints = currentCase.healthpoints*10 + json[readpointer] - '0';
                }
                else if ((state==8) && json[readpointer]>='0' && json[readpointer]<='9')
                {
                    buffer_character = buffer_character*10 + json[readpointer] - '0';
                }
                else if (state == 8 && json[readpointer]==',')
                {
                    currentCase.characters.Add(buffer_character);
                    buffer_character = 0;
                }
                else if (state == 8 && json[readpointer]==']')
                {
                    currentCase.characters.Add(buffer_character);
                    state = 9;
                }
                else if ((state == 9) && json[readpointer]==']')
                {
                    state++;
                }
                else if (state == 5 && readpointer<=json.Length-4  && json[readpointer]=='n' && json[readpointer+1]=='u' && json[readpointer+2]=='l' && json[readpointer+3]=='l')
                {
                    currentCase = null;
                    state = 10;
                }
                else if (state == 10 && json[readpointer]==',' )
                {
                    l.Add(currentCase);
                    state = 5;
                }
                else if (state == 10 && json[readpointer]==']' )
                {
                    l.Add(currentCase);
                    state ++;
                }
                else if ((state == 11) && json[readpointer]==']')
                {
                    this.caseprogression= (int) temps[0];
                    this.musicvol=temps[1];
                    this.SEvol = temps[2];
                    this.resolution = (int) temps[3];
                    this.fullscreen = (int) temps[4];
                    this.loadallowed = true;
                    save = l;
                    sr.Close() ;
                    return;
                }
                else if ((state == 10) && (json[readpointer]=='u' || json[readpointer]=='l'))
                {}
                else if (json[readpointer]==' ' || json[readpointer]=='\n' || json[readpointer]=='\0' )
                {}
                else
                {
                    Debug.Log("Error Loading File , eror c : "+ readpointer+ " "+ json[readpointer]+ " " + (int)json[readpointer] + " state : "+ state);
                    sr.Close();
                    break;
                }
            }
        }
        if(SceneManager.GetActiveScene().name=="MainMenu")
        {
            Image component = GameObject.Find("Load").GetComponent<Image>();
            component.color = new Color(component.color.r, component.color.g, component.color.b, 0.75f);
        }
        this.musicvol= (float) 0.5;
        this.SEvol = (float)  0.5;
        //this.resolution = 1;
        //this.fullscreen = 0;
        CreateNewPlayer();

        
    }
    
    public void CreateNewPlayer()
    {
        this.caseprogression= 0;
        this.save = new List<Case>();
        for (int i=0;i<5;i++)
        {
            this.save.Add(null);
        }
    }

    public void SaveJSON()
    {
        StreamWriter sw= new StreamWriter(Application.persistentDataPath+"/data.json");
        string filetext = "[";
        filetext += this.caseprogression + "," + musicvol.ToString(CultureInfo.InvariantCulture) + "," + SEvol.ToString(CultureInfo.InvariantCulture) +  "," + resolution + "," + fullscreen+ ",[" ;
        for (int caseid=0;caseid<save.Count;caseid++)
        {

            if (save[caseid]!= null)
            {
            filetext += "["+save[caseid].healthpoints+",[" ;
            for (int characterid = 0; characterid < save[caseid].characters.Count; characterid++)
            {
                filetext += save[caseid].characters[characterid];
                if (characterid<save[caseid].characters.Count-1)
                {
                    filetext += ",";
                }
            }
            filetext += "]]";
            }
            else
            {
                filetext += "null" ;
            }
            if (caseid<save.Count-1)
            {
                filetext += ",";
            }
        }
        filetext += "]]";
        sw.Write(filetext);
        sw.Close();
    }

    public string GetCurrentWitnessFile()
    {
        return "C" + caseID + "W" + (SpeakerToWitnessConversion(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID) +1) + "P" + (getCurrentWitnessProgression() + 1);
    }

    public string GetWitnessFile(int SpeakerID)
    {
        return "C" + caseID + "W" + (SpeakerToWitnessConversion(SpeakerID) + 1) + "P" + (getWitnessProgression(SpeakerID) + 1);
    }

    public string GetCurrentComparisonFile()
    {
        return "C" + caseID + "comp";
    }

    public string GetCurrentDialogueFile()
    {
        return "C" + caseID + "W" + (SpeakerToWitnessConversion(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID) +1) + "D" + (getCurrentWitnessProgression() + 1);
    }

    public void InitCurentCaseIfNeeded()
    {
        if (save[caseID]==null)
        {
            save[caseID] = new Case(4);
            intro = true;
        }
        
    }

    public void ResetCurrentCaseSave()
    {
        this.save[this.caseID]=null;
    }

    public void ShowCurrentProgessionInMenu()
    {
        for (int i = 0; i<3 ;i++)
        {
            if (i>caseprogression)
            {
                if (GameObject.Find(string.Format("Case{0}",i))!= null)
                {
                    Image component = GameObject.Find(string.Format("Case{0}",i)).GetComponent<Image>();
                    component.color = new Color(component.color.r,component.color.g,component.color.b,0.75f);
                    GameObject.Find(string.Format("Case{0}",i)).GetComponent<Button>().interactable = false;
                }
                else
                {
                    Debug.Log("Pas trouvé "+string.Format("Case{0}",i));
                }
            }

        }
        
    }
}
