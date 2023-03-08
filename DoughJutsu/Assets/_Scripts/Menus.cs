using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menus : MonoBehaviour
{

    public GameObject helpPage;
    public void Play()
    {
        SceneManager.LoadScene("ModeMenu");
    }

    public void Leaderboard()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Kitchen()
    {
        SceneManager.LoadScene("KitchenMenu");
    }

    public void Raid()
    {
        SceneManager.LoadScene("RaidMenu");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void House()
    {
        SceneManager.LoadScene("HouseStage");
    }

    public void OpenHelp()
    {
        helpPage.SetActive(true);
    }
    public void CloseHelp()
    {
        helpPage.SetActive(false);
    }
}
