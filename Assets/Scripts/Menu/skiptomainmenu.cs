using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class skiptomainmenu : MonoBehaviour
{
    public int waitframes;
    private int counter;
    public Image title;
    public Image BG;
    public TMPro.TextMeshProUGUI text1;
    public TMPro.TextMeshProUGUI text2;

    // Update is called once per frame
    void FixedUpdate()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
