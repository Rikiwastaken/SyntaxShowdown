using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseCaseMenu : MonoBehaviour
{
    void Awake()
    {
        GameObject.Find("MainConfig").GetComponent<MainConfig>().ShowCurrentProgessionInMenu();
    }

    public void Case0()
    {
        GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID = 0;
        GameObject.Find("MainConfig").GetComponent<MainConfig>().InitCurentCaseIfNeeded();
        SceneManager.LoadScene("Test temoignage");
    }

    public void Case1()
    {
        GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID = 1;
        GameObject.Find("MainConfig").GetComponent<MainConfig>().InitCurentCaseIfNeeded();
        SceneManager.LoadScene("Test temoignage");
    }

    public void Case2()
    {
        GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID = 2;
        GameObject.Find("MainConfig").GetComponent<MainConfig>().InitCurentCaseIfNeeded();
        SceneManager.LoadScene("Test temoignage");
    }

    public void Case3()
    {
        GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID = 3;
        GameObject.Find("MainConfig").GetComponent<MainConfig>().InitCurentCaseIfNeeded();
        SceneManager.LoadScene("Test temoignage");
    }
    public void Case4()
    {
        GameObject.Find("MainConfig").GetComponent<MainConfig>().caseID = 4;
        GameObject.Find("MainConfig").GetComponent<MainConfig>().InitCurentCaseIfNeeded();
        SceneManager.LoadScene("Test temoignage");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
