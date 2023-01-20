using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Requires tag "CurrencyText"
    public int PlayerCurrency = 0;
    public static GameManager Instance;
    TextMeshProUGUI CurrencyText;

    public int currentLevel = 1;

    //3rd stage
    public int spidersKilled = 0;
    public int currencyCollected = 0;

    //2nd stage
    public int spidersKilledThisRound = 0;
    public int currencyCollectedThisRound = 0;

    bool isPaused = false;
    private GameObject pauseMenu;

    public enum DifficultyLevel {Easy, Medium, Hard};

    public float enemyHealthModifier = 1f;
    public float playerDamageModifier = 1f;
    public float playerHealthModifier = 1f;

    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    public string sceneName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().ToString();
        easyButton = GameObject.Find("EasyButton").GetComponent<Button>();
        mediumButton = GameObject.Find("MediumButton").GetComponent<Button>();
        hardButton = GameObject.Find("HardButton").GetComponent<Button>();
        if(sceneName != "Level1") {
            easyButton.onClick.AddListener(delegate { SetDifficulty(DifficultyLevel.Easy); });
            mediumButton.onClick.AddListener(delegate { SetDifficulty(DifficultyLevel.Medium); });
            hardButton.onClick.AddListener(delegate { SetDifficulty(DifficultyLevel.Hard); });
        }

        else if(sceneName == "Level1") {
            CurrencyText = GameObject.FindGameObjectWithTag("CurrencyText").GetComponent<TextMeshProUGUI>();
            pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
            pauseMenu.SetActive(false); 
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            isPaused = !isPaused;
            if(isPaused) {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }

            else {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
        }
    }

    public void ChangeCurrency(int currency)
    {
        CurrencyText = GameObject.FindGameObjectWithTag("CurrencyText").GetComponent<TextMeshProUGUI>();
        PlayerCurrency += currency;
        CurrencyText.text = PlayerCurrency.ToString();
    }

    public void resetRoundStats()
    {
        spidersKilledThisRound = 0;
        currencyCollectedThisRound = 0;
    }

    public void ClearAll()
    {
        PlayerCurrency = 0;
        spidersKilled = 0 - spidersKilledThisRound;
        resetRoundStats();
    }

    public void SetDifficulty(DifficultyLevel difficulty)
    {
        switch (difficulty)
        {
            case DifficultyLevel.Easy:
                enemyHealthModifier = 0.8f;
                playerDamageModifier = 1.2f;
                playerHealthModifier = 1.5f;
                break;
            case DifficultyLevel.Medium:
                enemyHealthModifier = 1f;
                playerDamageModifier = 1f;
                playerHealthModifier = 1f;
                break;
            case DifficultyLevel.Hard:
                enemyHealthModifier = 1.2f;
                playerDamageModifier = 0.8f;
                playerHealthModifier = 0.5f;
                break;
        }
    }

    public void ChangeSceneToLevel1Easy() {
        SceneManager.LoadScene("Level1");
    }

    public void ChangeSceneToLevel1Medium() {
        SceneManager.LoadScene("Level1");
    }

    public void ChangeSceneToLevel1Hard() {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
