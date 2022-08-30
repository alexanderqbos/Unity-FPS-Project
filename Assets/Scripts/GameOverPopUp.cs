using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPopUp : basePopUp
{
    // public void Open()
    // {
    //     gameObject.SetActive(true);
    // }

    // public void Close()
    // {
    //     gameObject.SetActive(false);
    // }

    // public bool IsActive()
    // {
    //     return gameObject.activeSelf;
    // }

    public void OnStartAgainButton()
    {
        Debug.Log("Start Again clicked");
        Close();
        SceneManager.LoadScene(0);
    }

    public void OnExitGameButton()
    {
        Debug.Log("exit game");
        Application.Quit();
    }
}
