using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        CurrencyText = GameObject.FindGameObjectWithTag("CurrencyText").GetComponent<TextMeshProUGUI>();
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.SetActive(false);
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
}
