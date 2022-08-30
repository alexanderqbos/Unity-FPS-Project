using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreValue;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image crossHair;
    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private SettingsPopup settingsPopup;
    [SerializeField] private GameOverPopUp gameOverPopup;

    int popupsOpen = 0;

    public void UpdateScore(int newScore)
    {
        scoreValue.text = newScore.ToString();
    }

    public void SetGameActive(bool active)
    {
        if (active)
        {
            Time.timeScale = 1;                        // unpause the game 
            Cursor.lockState = CursorLockMode.Locked;  // show the cursor 
            crossHair.gameObject.SetActive(true);      // show the crosshair
            Cursor.visible = false; 
        }
        else
        {
            Time.timeScale = 0;                       // pause the game 
            Cursor.lockState = CursorLockMode.None;   // show the cursor 
            crossHair.gameObject.SetActive(false);    // turn off the crosshair 
            Cursor.visible = true;
        }
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvents.ENEMY_DEAD, onEnemyDead);
        Messenger<float>.AddListener(GameEvents.HEALTH_CHANGED, onHealthChanged);
        Messenger.AddListener(GameEvents.POPUP_OPEN, onPopUpOpen);
        Messenger.AddListener(GameEvents.POPUP_CLOSE, onPopUpClose);
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBar.fillAmount = 1;
        updateHealth(1.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateScore(score);
        if (Input.GetKeyDown(KeyCode.Escape) && popupsOpen == 0)
        {
            SetGameActive(false);
            optionsPopup.Open();
        }
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvents.ENEMY_DEAD, onEnemyDead);

        Messenger<float>.RemoveListener(GameEvents.HEALTH_CHANGED, onHealthChanged);
    }

    void onEnemyDead()
    {
        score += 1;
        UpdateScore(score);
    }

    void onHealthChanged(float percentage)
    {
        updateHealth(percentage);
    }

    void updateHealth(float percentage)
    {
        healthBar.fillAmount = percentage;
        healthBar.color = Color.Lerp(Color.red, Color.green, percentage);
    }
    
    public void ShowGameOverPopup()
    {
        gameOverPopup.Open();
    }


    void onPopUpOpen()
    {
        popupsOpen += 1;
        Debug.Log("Popup Opened, " + popupsOpen);
        if (popupsOpen > 0)
        {
            Messenger.Broadcast(GameEvents.GAME_PAUSED);
            SetGameActive(false);
        }
    }

    void onPopUpClose()
    {
        popupsOpen -= 1;
        Debug.Log("Popup Closed, " + popupsOpen);
        if (popupsOpen == 0)
        {
            Messenger.Broadcast(GameEvents.GAME_RESUMED);
            SetGameActive(true);
        }
    }
}
