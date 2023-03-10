using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponUpgrade : MonoBehaviour
{

    public int upgradeCost = 500;
    public float upgradeAmount = 1.5f;
    public GameObject player;

    private bool canUpgrade = false;
    private GameManager gameManager;
    private int playerPoints;
    private float playerDamage;
    private GameObject playerStats;

    public string damageText;

    void OnStart() {
        playerPoints = int.Parse(GameObject.Find("GameManager").GetComponent<GameManager>().PlayerCurrency.ToString());
        playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().damage;
        damageText = GameObject.Find("DamageBoostText").GetComponent<TextMeshProUGUI>().text;
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            canUpgrade = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            canUpgrade = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(canUpgrade && Input.GetKeyDown(KeyCode.L)) {
            playerPoints = int.Parse(GameObject.Find("GameManager").GetComponent<GameManager>().PlayerCurrency.ToString());
            if(playerPoints >= upgradeCost) {
                GameObject.Find("GameManager").GetComponent<GameManager>().ChangeCurrency(-500);
                playerDamage = playerDamage * upgradeAmount;
                GameObject.Find("DamageBoostText").GetComponent<TextMeshProUGUI>().text = "Damage: x" + playerDamage.ToString();
            }
        }
    }


}
