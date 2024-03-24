using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadingscreen : MonoBehaviour
{

    private Rigidbody2D rb;
    public float angvel;
    private bool startedloading = false;
    public float loadProgress;
    private UnityEngine.AsyncOperation loadingOperation;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        
    }

    void FixedUpdate()
    {
        rb.angularVelocity = angvel;
        if(!startedloading)
        {
            loadingOperation = SceneManager.LoadSceneAsync("Test temoignage");
            loadingOperation.allowSceneActivation = false;

            startedloading = true;
        }
        loadProgress = loadingOperation.progress;
        Debug.Log(loadProgress);
        if(loadProgress >= 0.9f)
        {
            loadingOperation.allowSceneActivation = true;
        }
    }
}
