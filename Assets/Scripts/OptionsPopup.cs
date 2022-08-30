using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPopup : basePopUp
{
    [SerializeField] private SettingsPopup settingsPopup;
    [SerializeField] private UIController uiController;
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

    public void OnSettingsButton()
    {
        Debug.Log("settings clicked");
        Close();
        settingsPopup.Open();
    }

    public void OnExitGameButton()
    {
        Debug.Log("exit game");
        Application.Quit();
    }

    public void OnReturnToGameButton()
    {
        Debug.Log("return to game");
        uiController.SetGameActive(true);
        Close();
    }
}
