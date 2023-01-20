using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTaker : MonoBehaviour
{

    TextMeshProUGUI points;
    TextMeshProUGUI kills;
    string pointsText;
    string killsText;
    GameManager gamemanager;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        points = GameObject.Find("PointsText").GetComponent<TextMeshProUGUI>();
        kills = GameObject.Find("EnemiesText").GetComponent<TextMeshProUGUI>();
        pointsText = gamemanager.currencyCollected.ToString();
        killsText = gamemanager.spidersKilled.ToString();
        points.text = "Points: " + pointsText;
        kills.text = "Enemies Killed: " + killsText;
    }

}
