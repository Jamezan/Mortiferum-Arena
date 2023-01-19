using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    }

    /*private void Update()
    {

    }*/

    public void ChangeCurrency(int currency)
    {
        CurrencyText = GameObject.FindGameObjectWithTag("CurrencyText").GetComponent<TextMeshProUGUI>();
        PlayerCurrency += currency;
        CurrencyText.text = "Currency: " + PlayerCurrency;
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
