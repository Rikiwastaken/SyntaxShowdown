using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class statements : MonoBehaviour
{
    public TextAsset textdoc;
    public TextMeshProUGUI text;
    private string fulltext;
    private int textlength;
    public string currentext;
    public int activedialogue=1;
    public bool readyusetext = false;
    public bool usetext = false;
    private bool changetext;

    private List<int> lignes = new List<int>();

    private int debutmot;

    private int not_real_character_counter;

    private List<Boolean> etatcase = new List<Boolean>();



    // Update is called once per frame
    void FixedUpdate()
    {
        activedialogue = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().activewindowstatements;
        changetext = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetextstatements;
        String newtext=GameObject.Find("MainConfig").GetComponent<MainConfig>().GetCurrentComparisonFile();
        TextAsset asset = (TextAsset)Resources.Load(newtext);
        textdoc = asset;
        etatcase.Clear();
        for (int i = 0; i < 4; i++)
        {
            GameObject MainConfig = GameObject.Find("MainConfig");
            string newparse = GameObject.Find("MainConfig").GetComponent<MainConfig>().GetWitnessFile(i+4);
            TextAsset newasset = (TextAsset)Resources.Load(newparse);
            Char idcompletion = newasset.text[2];
            if((int)Char.GetNumericValue(idcompletion) ==1)
            {
                etatcase.Add(true);
            }
            else
            {
                etatcase.Add(false);
            }
        }

        fulltext = textdoc.text;
        textlength = fulltext.Length;

        if (changetext || text.text=="")
        {
            not_real_character_counter = 0;
            lignes.Clear();
            currentext = "";
            for (int i = 0; i < textlength; i++) // Parcours de l'ensemble du fichier texte.
            {

                char letter = fulltext[i];
                if (letter == '#') //Recherche de la bonne fenêtre
                {
                    if (etatcase[activedialogue-1])
                    {
                        readyusetext = ((int)Char.GetNumericValue(fulltext[i + 1]) == activedialogue);
                    }
                    else
                    {
                        readyusetext = false;
                    }
                }
                else if (readyusetext) //Si nous pouvons écrire
                {
                    if (letter == '\n') //Recherche du début d'écriture
                    {
                        usetext = !(usetext); //Arrivé dans ou Sortie de la partie utile du message
                        if (usetext)
                        {
                            debutmot = i + 1;
                            lignes.Add(debutmot);
                        }
                        else
                        {
                            usetext = false;
                            readyusetext = false;
                        }
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
            GameObject.Find("SceneConfig").GetComponent<SceneConfig>().changetextstatements = false;
            //GameObject.Find("SceneConfig").GetComponent<SceneConfig>().linecounter = lignes.Count;
        }
        if(currentext=="")
        {
            currentext = "Undiscovered Testimony";
        }
        text.text = currentext;
    }

}
