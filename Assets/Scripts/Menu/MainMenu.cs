using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame() //fct qui se d�clenche quand on presse sur "new game"
    {
        GameObject.Find("MainConfig").GetComponent<MainConfig>().CreateNewPlayer();
        SceneManager.LoadScene("ChooseCaseMenu");
    }

    public void Load() //fct qui se d�clenche quand on presse sur "Load"
    {
        SceneManager.LoadScene("ChooseCaseMenu");
    }

    public void Quit() //fct qui se d�clenche quand on presse sur "Quit"
    {
        Application.Quit();
    }

    public void playSE()
    {
        GameObject.Find("Pageturnsound").GetComponent<AudioSource>().Play();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void UnlockCases()
    {
        GameObject.Find("MainConfig").GetComponent<MainConfig>().UnlockAllCases();
    }

    public void RefreshHP()
    {
        GameObject.Find("MainConfig").GetComponent<MainConfig>().CurrentHP=100;
    }

}
