using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsPopup : basePopUp
{
    [SerializeField] private OptionsPopup OptionsPopup;
    [SerializeField] private TextMeshProUGUI difficultyLabel;
    [SerializeField] private Slider difficultySlider;

    public override void Open()
    {
        base.Open();
        OptionsPopup.Close();
        
        difficultySlider.value = PlayerPrefs.GetInt("difficulty");
        UpdateDifficulty(difficultySlider.value);
    }

    public override void Close()
    {
        base.Close();
        OptionsPopup.Open();
    }

    // public bool IsActive()
    // {
    //     return gameObject.activeSelf;
    // }
    
    private void Start() {
        //init slider values
        difficultySlider.value = PlayerPrefs.GetInt("difficulty");
        UpdateDifficulty(difficultySlider.value);
    }

    public void OnOKButton()
    {
        PlayerPrefs.SetInt("difficulty", (int)difficultySlider.value);
        Close();
    }

    public void OnCancelButton()
    {
        Close();
    }

    public void UpdateDifficulty(float difficulty)
    {
        difficultyLabel.text = "Difficulty: " + ((int)difficulty).ToString();
    }

    public void OnDifficultyValueChanged(float difficulty)
    {
        UpdateDifficulty(difficulty);
        Messenger<int>.Broadcast(GameEvents.DIFFICULTY_CHANGED, (int)difficultySlider.value);
    }
}
