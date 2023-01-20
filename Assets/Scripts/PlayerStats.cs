using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FPSControllerLPFP;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        currentHealth = health;
        HealthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<TextMeshProUGUI>();
        HealthText.text = "HP: " + currentHealth;

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
        currentHealth += 50;
        if (currentHealth > health)
        {
            currentHealth = health;
        }
        HealthText.text = "HP: " + currentHealth;
    }

}