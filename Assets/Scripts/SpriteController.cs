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
        Anim.SetBool("laughing", false);
        Anim.SetBool("idea", false);
        Anim.SetBool("showevidence", false);
        Anim.SetBool("despair", false);
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
    public void Laughing()
    {
        Idle();
        Anim.SetBool("laughing", true);
    }
    public void Idea()
    {
        Idle();
        Anim.SetBool("idea", true);
    }
    public void ShowEvidence()
    {
        Idle();
        Anim.SetBool("showevidence", true);
    }
    public void Despair()
    {
        Idle();
        Anim.SetBool("despair", true);
    }
}
