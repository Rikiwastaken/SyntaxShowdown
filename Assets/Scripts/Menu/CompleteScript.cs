using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteScript : MonoBehaviour
{

    List<int> advlist;
    public int witnessID;
    public bool show;
    public bool iscomplete;

    // Update is called once per frame
    void FixedUpdate()
    {

        advlist = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().advlist;

        if(iscomplete )
        {
            if(GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID == 0)
            {
                if (witnessID == 4 && advlist[0] == 2)
                {
                    show = true;
                }
                if (witnessID == 5 && advlist[1] == 1)
                {
                    show = true;
                }
                if (witnessID == 6 && advlist[2] == 0)
                {
                    show = true;
                }
                if (witnessID == 7 && advlist[3] == 0)
                {
                    show = true;
                }
            }
            if (GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID == 1)
            {
                if(witnessID == 4 && advlist[0]==3)
                {
                    show = true;
                }
                if (witnessID == 5 && advlist[1] == 4)
                {
                    show = true;
                }
                if (witnessID == 6 && advlist[2] == 3)
                {
                    show = true;
                }
                if (witnessID == 7 && advlist[3] == 3)
                {
                    show = true;
                }
            }
        }
        else
        {
            if (GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID == 0)
            {
                if (witnessID == 4 && advlist[0] == 1)
                {
                    show = true;
                }
                if (witnessID == 5 && advlist[1] == 0)
                {
                    show = true;
                }
                if (witnessID == 6 && advlist[2] == 0)
                {
                    show = true;
                }
                if (witnessID == 7 && advlist[3] == 0)
                {
                    show = true;
                }
            }
            if (GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID == 1)
            {
                if (witnessID == 4 && advlist[0] == 3)
                {
                    show = true;
                }
                if (witnessID == 5 && advlist[1] == 3)
                {
                    show = true;
                }
                if (witnessID == 6 && advlist[2] == 2)
                {
                    show = true;
                }
                if (witnessID == 7 && advlist[3] == 3)
                {
                    show = true;
                }
            }
            Image[] IMGCollection = GameObject.Find("CgtTemoin").GetComponentsInChildren<Image>();
            foreach (Image img in IMGCollection)
            {
                GameObject parent = img.gameObject;
                if (parent.GetComponent<CompleteScript>()!=null && parent.name!=this.name)
                {
                    if (parent.GetComponent<CompleteScript>().witnessID == witnessID && parent.GetComponent<CompleteScript>().show)
                    {
                        show = false;
                        break;
                    }
                }
            }
        }


        if(show)
        {
            Color color = GetComponent<Image>().color;
            GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1f);
        }
        else
        {
            Color color = GetComponent<Image>().color;
            GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0f);
        }
    }
}
