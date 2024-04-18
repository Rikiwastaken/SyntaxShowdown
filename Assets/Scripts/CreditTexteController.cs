using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditTexteController : MonoBehaviour
{
    float speed = 60F;
    int start_y = -175;
    int end_y= 1592;
    private Rigidbody2D Element;
    float counter = 0;
    int end_counter = 5;


    void Awake()
    {
        Element= GetComponent<Rigidbody2D>();
        transform.localPosition = new Vector2(transform.localPosition.x, start_y);
    }

    

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y < end_y)
        {
            Element.velocity = new Vector2(0,speed);
        }
        else
        {
            Element.velocity = new Vector2(0,0);
            counter = counter + 0.1f;
            if ( counter > end_counter )
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        
    }
}
