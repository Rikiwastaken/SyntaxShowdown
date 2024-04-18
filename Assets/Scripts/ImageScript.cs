using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class ImageScript : MonoBehaviour
{
    private Image Image;
    private UnityEngine.Color color;
    public bool isblackscreen;
    private bool fondu;
    private bool fonduterminé;
    private float posx;
    void Awake()
    {
        posx = transform.position.x;
    }
    void FixedUpdate()
    {
        
        if(Image == null)
        {
            Image = GetComponent<Image>();
        }
        color = Image.color;
        if (isblackscreen )
        {

            if (GameObject.Find("SceneConfig").GetComponent<SceneConfig>().ActiveImage != null)
            {
                if (this.name == GameObject.Find("SceneConfig").GetComponent<SceneConfig>().ActiveImage.name)
                { 
                    if (Image.color.a < 1f && !fondu && !fonduterminé)
                    {
                        this.transform.position = new Vector2(555, this.transform.position.y);
                        Image.color = new UnityEngine.Color(color.r, color.g, color.b, 1f);
                        fondu = true;
                    }
                    else if(Image.color.a > 0f && fondu)
                    {
                        Image.color = new UnityEngine.Color(color.r, color.g, color.b, color.a-0.02f);
                        if(Image.color.a <= 0f)
                        {
                            fondu=false;
                            fonduterminé= true;
                            this.transform.position = new Vector2(10000, this.transform.position.y);
                        }
                    }
                }
                else
                {
                    if (Image.color.a > 0f)
                    {
                        Image.color = new UnityEngine.Color(color.r, color.g, color.b, color.a - 0.05f);
                    }
                }
            }
            else
            {
                this.transform.position = new Vector2(10000, this.transform.position.y);
                fonduterminé = false;
                if (Image.color.a > 0f)
                {
                    Image.color = new UnityEngine.Color(color.r, color.g, color.b, color.a - 0.05f);
                }
            }
        }
        else
        {
            if (GameObject.Find("SceneConfig").GetComponent<SceneConfig>().ActiveImage != null)
            {
                if (this.name == GameObject.Find("SceneConfig").GetComponent<SceneConfig>().ActiveImage.name)
                {
                    transform.position = new Vector2(posx, transform.position.y);
                    if (Image.color.a < 1f)
                    {
                        Image.color = new UnityEngine.Color(color.r, color.g, color.b, color.a + 0.05f);
                    }
                }
                else
                {
                    if (Image.color.a > 0f)
                    {
                        Image.color = new UnityEngine.Color(color.r, color.g, color.b, color.a - 0.05f);
                    }
                    if(Image.color.a <= 0f)
                    {
                        this.transform.position = new Vector2(10000, this.transform.position.y);
                    }
                }
            }
            else
            {
                if (Image.color.a > 0f)
                {
                    Image.color = new UnityEngine.Color(color.r, color.g, color.b, color.a - 0.05f);
                }
            }
        }
        if (Image.color.a <= 0.05f)
        {
            this.transform.position = new Vector2(10000, this.transform.position.y);
        }
    }
}
