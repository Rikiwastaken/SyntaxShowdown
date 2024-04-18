using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class displaytext : MonoBehaviour
{
    [Header("Text variables")]
    public TextAsset textdoc;
    public TextMeshProUGUI text;
    private string fulltext;
    private int textlength;
    public string currentext;
    public int activedialogue;
    public bool readyusetext = false;
    public bool usetext = false;
    private bool changetext;

    [Header("error variables")]
    public int posofbase;
    public int posofstart;
    public int posofend;
    public bool mistake = false;
    public GameObject mistakeObj;
    private List<object> errors = new List<object>();
    private List<int> lignes = new List<int>();
    private int debutmot;
    private int not_real_character_counter;

    [Header("statements")]
    public bool isstatement;

    [Header("Background")]
    public int backgroundID;

    public float animationspeed;

    class TrupleError
    {
        public int NumLigne;
        public int StartError;
        public int EndError;

        public TrupleError(int NumLigne, int StartError, int EndError)
        {
            this.NumLigne = NumLigne;
            this.StartError = StartError;
            this.EndError = EndError;
        }
    }

    void Awake()
    {
        init();
    }

    public void Initialisation()
    {
        usetext=false;
        readyusetext=false;
        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().maxwindow = 0;
        fulltext = textdoc.text;
        textlength = fulltext.Length;
        if (GameObject.Find("SceneConfig").GetComponent<SceneConfig>().isdialogue || GameObject.Find("SceneConfig").GetComponent<SceneConfig>().iserrordialogue)
        {
            backgroundID = (int)Char.GetNumericValue(fulltext[2]);
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID = (int)Char.GetNumericValue(fulltext[3]);
        }
        else
        {
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID = (int)Char.GetNumericValue(fulltext[0]);
            backgroundID = (int)Char.GetNumericValue(fulltext[1]);
        }

        ChangeBackground(backgroundID);
        Debug.Log("SpeakerID : "+GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID);
        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().ShowedCharacter = ChangeCharacter(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID, GameObject.Find("SceneConfig").GetComponent<SceneConfig>().caseID);

        for (int i = 0; i < textlength; i++)
        {
            char letter = fulltext[i];

            if (letter == '#')
            {
                int currentmax= (int)Char.GetNumericValue(fulltext[i + 1]);
                 if (currentmax ==-1)
                {
                    currentmax = fulltext[i + 1]- 87;
                }
                if (currentmax <= 0)
                {
                    currentmax = fulltext[i + 1] - 29; //corresponds à A, B ,C, ..., Z
                }
                if (GameObject.Find("SceneConfig").GetComponent<SceneConfig>().maxwindow < currentmax)
                {
                    GameObject.Find("SceneConfig").GetComponent<SceneConfig>().maxwindow = currentmax;
                }
            }
        }
    }

    void init()
    {
        int CaseID = GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID;

        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindow = 1;

        GameObject.Find("Texte_Nom").GetComponent<NameDisplay>().refreshname(1); // sert � afficher le bon nom
        TextAsset asset;
        if (GameObject.Find("MainConfig").GetComponent<MainConfig>().intro)
        {
            asset = (TextAsset) Resources.Load(string.Format("C{0}W0D0",CaseID));
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().isdialogue = true;
        }
        else
        {
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID=4;
            asset = (TextAsset) Resources.Load(GameObject.Find("MainConfig").GetComponent<MainConfig>().GetCurrentWitnessFile());
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().isdialogue = false;
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().ShowedCharacter = ChangeCharacter(4,CaseID);

        }
        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetext = true;
        textdoc=asset;
        
        

        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().iserrordialogue = false;
        Initialisation(); //affiche le bon texte et le bon numero de page
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().ShowedCharacter = ChangeCharacter(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID, GameObject.Find("SceneConfig").GetComponent<SceneConfig>().caseID);

        activedialogue = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindow;
        changetext = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetext;

        fulltext = textdoc.text;
        textlength = fulltext.Length;
        usetext=false;
        readyusetext=false;
        if (changetext)
        {
            not_real_character_counter = 0;
            DestroyErrorButton();
            lignes.Clear();
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().thereisamistake = false;
            currentext = "";
            for (int i = 0; i < textlength; i++) // Parcours de l'ensemble du fichier texte.
            {

                char letter = fulltext[i];

                if (letter == '#') //Recherche de la bonne fenêtre
                {
                    //entiers de 1 à 9 puis lettres minuscules puis lettres majuscules
                    int currenttextwindow = (int)Char.GetNumericValue(fulltext[i + 1]); //corresponds à 1, 2 ,3, ..., 9
                    if (currenttextwindow == -1)
                    {
                        currenttextwindow = fulltext[i + 1] - 87; //corresponds à a, b, c, ..., z
                    }
                    if(currenttextwindow <= 0)
                    {
                        currenttextwindow = fulltext[i + 1] - 29; //corresponds à A, B ,C, ..., Z
                    }
                    readyusetext = (currenttextwindow == activedialogue); //Autorisé l'écriture du message
                }
                else if (readyusetext) //Si nous pouvons écrire
                {
                    if (GameObject.Find("SceneConfig").GetComponent<SceneConfig>().isdialogue || GameObject.Find("SceneConfig").GetComponent<SceneConfig>().iserrordialogue)
                    {
                        if (fulltext[i - 1] == '#')
                        {
                            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().ActiveImage = changeImage((int)Char.GetNumericValue(fulltext[i + 6]));
                            ChangeBackground((int)Char.GetNumericValue(fulltext[i + 1]));
                            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID = (int)Char.GetNumericValue(fulltext[i + 2]);
                            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().ShowedCharacter = ChangeCharacter(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().speakerID, GameObject.Find("SceneConfig").GetComponent<SceneConfig>().caseID);
                            if(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().ShowedCharacter!=null)
                            {
                                ChangeAnimation(GameObject.Find("SceneConfig").GetComponent<SceneConfig>().ShowedCharacter, (int)Char.GetNumericValue(fulltext[i + 3]));
                            }
                            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().ChangeMusic(fulltext[i + 4], (int)Char.GetNumericValue(fulltext[i + 5]));

                        }
                    }
                    if (letter == '\n') //Recherche du début d'écriture
                    {
                        usetext = !(usetext); //Arrivé dans ou Sortie de la partie utile du message
                        if (usetext)
                        {
                            posofbase = i + 1; // Nous cherchons la position de départ du message dans le texte total
                            debutmot = i + 1;
                            lignes.Add(debutmot);
                        }
                        else
                        {
                            usetext = false;
                            readyusetext = false;
                        }
                    }
                    else if (letter == '/')
                    {
                        if (!mistake)
                        {
                            posofstart = i; // Nous regardons ou est le début de la faute dans le texte
                            mistake = !mistake;
                            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().thereisamistake = true;
                        }
                        else
                        {
                            posofend = i - 2; // Nous regardons ou est la fin de la faute dans le texte
                            mistake = !mistake;
                        }
                        not_real_character_counter++;

                    }
                    else if (usetext)
                    {
                        currentext += letter; //Ecriture du caractère
                        if (letter == ' ')
                        {
                            if (i - not_real_character_counter - lignes[lignes.Count - 1] > 42)
                            {
                                if (i - not_real_character_counter - lignes[lignes.Count - 1] == 43)
                                {
                                    lignes.Add(i - not_real_character_counter + 1);
                                }
                                else
                                {
                                    lignes.Add(debutmot);
                                }
                            }
                            debutmot = i - not_real_character_counter + 1;

                        }
                    }
                }
            }
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetext = false;
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().linecounter = lignes.Count;
            if (GameObject.Find("SceneConfig").GetComponent<SceneConfig>().thereisamistake)
            {
                CopyErrorButton();
            }
        }
        text.text = currentext;
    }

    private GameObject changeImage(int ImageID)
    {
        Color couleur;
        GameObject Image = null;
        SpriteRenderer[] SRCollection = GameObject.Find("ImageCanvas").GetComponentsInChildren<SpriteRenderer>();
        if(ImageID > 1)
        {
            foreach (SpriteRenderer SR in SRCollection)
            {
                couleur = SR.color;
                SR.color = new Color(couleur.r, couleur.g, couleur.b, 0);
            }
        }
        if(ImageID == 1)
        {
            Image = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().ActiveImage;
        }
        if( ImageID == 2)
        {
            Image = GameObject.Find("C1Map");
        }
        if (ImageID == 3)
        {
            Image = GameObject.Find("C1Map-annotee");
        }
        if (ImageID == 4)
        {
            Image = GameObject.Find("BlackScreen");
            UnityEngine.UI.Image Imagecp = Image.GetComponent<UnityEngine.UI.Image>();
            Imagecp.color = new Color(Imagecp.color.r, Imagecp.color.g, Imagecp.color.b, 0.99f);
        }
        if (ImageID == 5)
        {
            Image = GameObject.Find("C2Picture");
        }
        return Image;
    }

    private GameObject ChangeCharacter(int SpeakerID, int CaseID)
    {
        Color couleur;
        GameObject Character = GameObject.Find("OldNoctis");
        SpriteRenderer[] SRCollection = GameObject.Find("CharacterCanvas").GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer SR in SRCollection)
        {
            couleur = SR.color;
            SR.color = new Color(couleur.r, couleur.g, couleur.b, 0);
        }
        if (SpeakerID == 0) //Dick Shionary
        {
            Character = GameObject.Find("DickShionary");

        }
        if (SpeakerID == 1) //Otto Graph
        {
            Character = GameObject.Find("OttoGraph");

        }
        if (SpeakerID == 2) //Prost Ecution
        {
            Character = GameObject.Find("ProstEcution");

        }
        if (SpeakerID == 3) // Hillary Vocab
        {
            Character = GameObject.Find("HillaryVocab");
        }
        if(SpeakerID == 9)
        {
            Character = null;
        }
        if (CaseID == 0)
        {
            if (SpeakerID >= 4 && SpeakerID <=8) //Otto Graph
            {
                Character = GameObject.Find("OttoGraph");
            }
        }
        if (CaseID == 1)
        {
            if (SpeakerID == 4) //Evan Quished
            {
                Character = GameObject.Find("EvanQuished");
            }
            if (SpeakerID == 5) //Li Jisto
            {
                Character = GameObject.Find("LiJisto");
            }
            if (SpeakerID == 6) //Paul Hissman
            {
                Character = GameObject.Find("PaulHissman");
            }
            if (SpeakerID == 7) //Reed Hired
            {
                Character = GameObject.Find("ReedIred");  
            }
            if (SpeakerID == 8) //Bill Hard
            {
                Character = GameObject.Find("BillHard");
            }
        }
        if (CaseID == 2)
        {
            if (SpeakerID == 4) //Richard Gold
            {
                Character = GameObject.Find("RichardGold");
            }
            if (SpeakerID == 5) //Boris Neigh
            {
                Character = GameObject.Find("BorisNeigh");
            }
            if (SpeakerID == 6) //Tess La
            {
                Character = GameObject.Find("TessLa");
            }
            if (SpeakerID == 7) //Paul Hissman
            {
                Character = GameObject.Find("PaulHissman");
            }
            if (SpeakerID == 8) //Richard Gold
            {
                Character = GameObject.Find("RichardGold");
            }
        }

        if (Character!=null)
        {
            couleur = Character.GetComponent<SpriteRenderer>().color;
            Character.GetComponent<SpriteRenderer>().color = new Color(couleur.r, couleur.g, couleur.b, 1);
        } 
        return Character;
    }

    private void ChangeAnimation(GameObject Character, int AnimID)
    {
        if(Character.GetComponent<SpriteController>() != null)
        {
            if(AnimID == 0)
            {
                Character.GetComponent<SpriteController>().Idle();
            }
            if (AnimID == 1)
            {
                Character.GetComponent<SpriteController>().Accuse();
            }
            if (AnimID == 2)
            {
                Character.GetComponent<SpriteController>().Determined();
            }
            if (AnimID == 3)
            {
                Character.GetComponent<SpriteController>().Surprised();
            }
            if (AnimID == 4)
            {
                Character.GetComponent<SpriteController>().Thinking();
            }
            if (AnimID == 5)
            {
                Character.GetComponent<SpriteController>().Laughing();
            }
            if (AnimID == 6)
            {
                Character.GetComponent<SpriteController>().Idea();
            }
            if (AnimID == 7)
            {
                Character.GetComponent<SpriteController>().ShowEvidence();
            }
            if (AnimID == 8)
            {
                Character.GetComponent<SpriteController>().Despair();
            }
        }
        Character.GetComponent<Animator>().speed = animationspeed;
    }

    private void ChangeBackground(int backgroundID)
    {
        Color couleur;
        SpriteRenderer[] SRCollection = GameObject.Find("BackgroundCanvas").GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer SR in SRCollection)
        {
            couleur = SR.color;
            SR.color = new Color(couleur.r, couleur.g, couleur.b, 0);
        }

        if (backgroundID == 1)
        {
            couleur = GameObject.Find("BackgroundLawyer").GetComponent<SpriteRenderer>().color;
            GameObject.Find("BackgroundLawyer").GetComponent<SpriteRenderer>().color = new Color(couleur.r, couleur.g, couleur.b, 1);
            couleur = GameObject.Find("BackgroundLawyerFront").GetComponent<SpriteRenderer>().color;
            GameObject.Find("BackgroundLawyerFront").GetComponent<SpriteRenderer>().color = new Color(couleur.r, couleur.g, couleur.b, 1);
        }
        if (backgroundID == 2)
        {
            couleur = GameObject.Find("BackgroundWitness").GetComponent<SpriteRenderer>().color;
            GameObject.Find("BackgroundWitness").GetComponent<SpriteRenderer>().color = new Color(couleur.r, couleur.g, couleur.b, 1);
            couleur = GameObject.Find("BackgroundWitnessFront").GetComponent<SpriteRenderer>().color;
            GameObject.Find("BackgroundWitnessFront").GetComponent<SpriteRenderer>().color = new Color(couleur.r, couleur.g, couleur.b, 1);
        }
        if(backgroundID == 3)
        {
            couleur = GameObject.Find("JudgeBackground").GetComponent<SpriteRenderer>().color;
            GameObject.Find("JudgeBackground").GetComponent<SpriteRenderer>().color = new Color(couleur.r, couleur.g, couleur.b, 1);
            couleur = GameObject.Find("JudgeBGFront").GetComponent<SpriteRenderer>().color;
            GameObject.Find("JudgeBGFront").GetComponent<SpriteRenderer>().color = new Color(couleur.r, couleur.g, couleur.b, 1);
        }
        if (backgroundID == 4)
        {
            couleur = GameObject.Find("BackgroundProsecutor").GetComponent<SpriteRenderer>().color;
            GameObject.Find("BackgroundProsecutor").GetComponent<SpriteRenderer>().color = new Color(couleur.r, couleur.g, couleur.b, 1);
            couleur = GameObject.Find("BackgroundProsecutorFront").GetComponent<SpriteRenderer>().color;
            GameObject.Find("BackgroundProsecutorFront").GetComponent<SpriteRenderer>().color = new Color(couleur.r, couleur.g, couleur.b, 1);
        }
        if(backgroundID == 5)
        {
            couleur = GameObject.Find("OutsideBackground").GetComponent<SpriteRenderer>().color;
            GameObject.Find("OutsideBackground").GetComponent<SpriteRenderer>().color = new Color(couleur.r, couleur.g, couleur.b, 1);
        }
        if (backgroundID == 6)
        {
            couleur = GameObject.Find("BackgroundPhone").GetComponent<SpriteRenderer>().color;
            GameObject.Find("BackgroundPhone").GetComponent<SpriteRenderer>().color = new Color(couleur.r, couleur.g, couleur.b, 1);
            couleur = GameObject.Find("BackgroundPhoneFront").GetComponent<SpriteRenderer>().color;
            GameObject.Find("BackgroundPhoneFront").GetComponent<SpriteRenderer>().color = new Color(couleur.r, couleur.g, couleur.b, 1);
        }
        if(backgroundID == 7)
        {
            couleur = GameObject.Find("AnteChamberBackground").GetComponent<SpriteRenderer>().color;
            GameObject.Find("AnteChamberBackground").GetComponent<SpriteRenderer>().color = new Color(couleur.r, couleur.g, couleur.b, 1);
        }
    }

    private void CopyErrorButton()
    {
        mistakeObj = GameObject.Find("Error");
        GameObject Canvas = GameObject.Find("BoiteDeDialogue");

        //Il y a 43 charactère par lignes
        //Un Charactère est vaut 14.3 de largeur
        //Le milieur est au 22 ieme charactère
        //Le changement de lignge vaut x 

        //Regardons combien de Zones nous faut-il

        List<TrupleError> ligneserror = new List<TrupleError>();
        int temp_error_start = posofstart;
        for (int i=0;i<lignes.Count;i++)
        {
            //trouvons la ligne de départ
            if (temp_error_start<lignes[i]) 
            {
                //On sait que l'erreur est à la ligne precedente
                // Deux cas soit l'erreur se termine sur la ligne precedente, soit l'erreur se propage
                if (posofend<lignes[i])// L'erreur est sur la meme ligne
                {
                    TrupleError trupleError = new TrupleError(i-1,temp_error_start-lignes[i-1],posofend-lignes[i-1]);
                    temp_error_start = int.MaxValue;
                    ligneserror.Add(trupleError);
                }
                else // L'erreur se retrouve sur plusieurs lignes
                {
                    TrupleError trupleError = new TrupleError(i-1,temp_error_start-lignes[i-1],lignes[i]-1-lignes[i-1]);
                    temp_error_start = lignes[i];
                    ligneserror.Add(trupleError);
                }
            }
            if ((i== lignes.Count-1) && temp_error_start!= int.MaxValue ) // L'erreur est sur la dernier ligne
            {
                TrupleError trupleError = new TrupleError(i,temp_error_start-lignes[i],posofend-lignes[i]);
                temp_error_start = int.MaxValue;
                ligneserror.Add(trupleError);
            }
        }
        GameObject.Find("SceneConfig").GetComponent<SceneConfig>().lineerrorcounter = ligneserror.Count;

        for (int i=0;i<ligneserror.Count;i++)
        {
            GameObject newError = Instantiate(mistakeObj);
            newError.transform.SetParent(Canvas.transform);
            this.errors.Add(newError);

            //premier caractère première ligne -> -300, -104
            //Deuxième ligne -> -300, -133
            //Troisième Ligne -> -166
            // y = -104 - 29 * ligne
            // x = -300 + 14.3 * + debut - 1 + ((fin-debut)/2 + 0,5 si pair 1 sinon)
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().errorStart = ligneserror[i].StartError;
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().errorEnd = ligneserror[i].EndError;
            double decalage;
            if ((ligneserror[i].EndError - ligneserror[i].StartError) % 2 == 1)
            {
                decalage = 0.5;
            }
            else
            {
                decalage = 1;
            }
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().floatdebug = (float)(-300 + 14.3 *(ligneserror[i].StartError - 1 + (ligneserror[i].EndError - ligneserror[i].StartError + 1) / 2 + decalage));
            newError.transform.localPosition =  new Vector3((float)(-300 + 14.3 *(ligneserror[i].StartError - 1+ (ligneserror[i].EndError - ligneserror[i].StartError + 1) / 2 + decalage)),(float)(-103 - 29 * ligneserror[i].NumLigne ),(float)0);
            newError.transform.localScale = new Vector3(ligneserror[i].EndError - ligneserror[i].StartError + 1,(float)1.1,1);
        }
        
    }

    private void DestroyErrorButton()
    {
        for (int i=0;i<errors.Count;i++)
        {
            Destroy((GameObject)errors[i]);
        }
        errors.Clear();
    }

}


