using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThatisWrongdeplacement : MonoBehaviour
{

    public Rigidbody2D RB;
    public bool move;
    public bool movetonexttext;

    private float currentvel;
    public float minvel;
    public int slowpoint;
    public float maxvel;
    public float slowvel;
    public int slowvelduration;
    public int slowvelcnt=-1;

    public float posx;


    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        move = GameObject.Find("SceneConfig").GetComponent<SceneConfig>().moveThatIsWrongText;


        posx =GetComponent<RectTransform>().position.x - GameObject.Find("ThatIsWrong").GetComponent<RectTransform>().position.x;
        if(RB==null)
        {
            RB = this.GetComponent<Rigidbody2D>();
        }

        if(move)
        {
            currentvel = RB.velocity.x;
            if (posx < slowpoint)
            {
                if (currentvel <= 0)
                {
                    RB.velocity = new Vector2(minvel, 0);
                }
                else
                {
                    if (currentvel < maxvel)
                        RB.velocity = new Vector2(currentvel * 2, 0);
                }
            }
            else if (posx >= slowpoint && slowvelcnt == -1)
            {
                slowvelcnt = 0;
                RB.velocity = new Vector2(slowvel, 0);
                GameObject.Find("thatiswrongsound").GetComponent<AudioSource>().Play();
                
            }
            else if (slowvelcnt < slowvelduration)
            {
                slowvelcnt += 1;
                RB.velocity = new Vector2(slowvel, 0);
            }
            else
            {
                movetonexttext = true;
                RB.velocity = new Vector2(currentvel * 2, 0);
            }
            if(posx>2000)
            {
                slowvelcnt = -1;
                RB.velocity= new Vector2(0,0);
                this.transform.position = new Vector2(GameObject.Find("ThatIsWrong").transform.position.x - 2000, GameObject.Find("ThatIsWrong").transform.position.y);
                GameObject.Find("SceneConfig").GetComponent<SceneConfig>().moveThatIsWrongText = false;
                movetonexttext = false;
            }
        }
        else
        {
            slowvelcnt = -1;
            RB.velocity = new Vector2(0, 0);
            movetonexttext = false;
            this.transform.position = new Vector2(GameObject.Find("ThatIsWrong").transform.position.x - 2000 + slowpoint, GameObject.Find("ThatIsWrong").transform.position.y);
        }
    }
}
