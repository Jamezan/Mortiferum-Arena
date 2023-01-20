using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FPSControllerLPFP;
using UnityEngine.SceneManagement;

public class PlayerStatsHard : MonoBehaviour
{
    //Requires a health text with tag "HealthText"
    [SerializeField]
    private int health = 100;
    public int currentHealth;
    [SerializeField]
    public int damage = 25;

    private GameObject gameManager;
    TextMeshProUGUI HealthText;
    TextMeshProUGUI ShopText;
    public int currentPoints;
    public int healCost = 50;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        currentHealth = health;
        HealthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<TextMeshProUGUI>();
        HealthText.text = "HP: " + currentHealth;
        currentPoints = gameManager.GetComponent<GameManager>().PlayerCurrency;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.V)) {
            if(currentPoints >= healCost && currentHealth < health) {
                DrinkHPPotion();
                gameManager.GetComponent<GameManager>().ChangeCurrency(-50);
            }
        }
    }

    public void TakeDamage(int enemydamage)
    {
        currentHealth -= enemydamage;
        HealthText.text = "HP: " + currentHealth;

        if (currentHealth < 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void DrinkHPPotion()
    {
        currentHealth += 20;
        if (currentHealth > health)
        {
            currentHealth = health;
        }
        HealthText.text = "HP: " + currentHealth;
    }

}