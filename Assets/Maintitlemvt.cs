using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Maintitlemvt : MonoBehaviour
{

    public bool isshadow;
    public float speed;
    public int startx;
    private Rigidbody2D RB;
    private bool decalage;
    private GameObject star;
    public int turnlength;
    public int turncnt;

    void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        transform.localPosition = new Vector2(startx, transform.localPosition.y);
        star = GameObject.Find("star");
        star.GetComponent<Image>().color = new Color(star.GetComponent<Image>().color.r, star.GetComponent<Image>().color.g, star.GetComponent<Image>().color.b,0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!decalage)
        {
            if (transform.localPosition.x < 0)
            {
                RB.velocity = new Vector2(speed, 0);
            }
            else
            {
                transform.localPosition = new Vector2(0, transform.localPosition.y);
                if(isshadow && transform.localPosition.x<5)
                {
                    decalage = true;
                }
                
            }
        }
        if(decalage)
        {
            RB.velocity = new Vector2(10, 0);
            if(transform.localPosition.x >= 5)
            {
                RB.velocity = Vector2.zero;
                if(star.GetComponent<Image>().color.a<1f)
                {
                    star.GetComponent<Rigidbody2D>().angularVelocity = -300f;
                    star.GetComponent<Image>().color = new Color(star.GetComponent<Image>().color.r, star.GetComponent<Image>().color.g, star.GetComponent<Image>().color.b, star.GetComponent<Image>().color.a+0.01f);
                    turncnt = turnlength;
                }
                else if(star.GetComponent<Rigidbody2D>().rotation>5 || star.GetComponent<Rigidbody2D>().rotation < -5)
                {
                    star.GetComponent<Rigidbody2D>().angularVelocity = star.GetComponent<Rigidbody2D>().angularVelocity+50f;
                    if (turncnt > 0)
                    {
                        turncnt--;
                    }
                    if (turncnt == 0)
                    {
                        star.GetComponent<Rigidbody2D>().rotation = 0f;
                        star.GetComponent<Rigidbody2D>().angularVelocity = 0f;
                    }
                }
                else
                {
                    star.GetComponent<Rigidbody2D>().angularVelocity = 0f;
                }
            }
            
        }

        
    }
}
