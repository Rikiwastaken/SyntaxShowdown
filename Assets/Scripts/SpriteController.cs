using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{

    private Animator Anim;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Anim == null)
        {
            Anim = GetComponent<Animator>();
        }
    }

    public void Idle()
    {
        Anim.SetBool("accuse", false);
        Anim.SetBool("thinking", false);
        Anim.SetBool("surprised", false);
        Anim.SetBool("determined", false);
    }

    public void Thinking()
    {
        Idle();
        Anim.SetBool("thinking", true);
    }

    public void Surprised()
    {
        Idle();
        Anim.SetBool("surprised", true);
    }

    public void Accuse()
    {
        Idle();
        Anim.SetBool("accuse", true);
    }

    public void Determined()
    {
        Idle();
        Anim.SetBool("determined", true);
    }
}
