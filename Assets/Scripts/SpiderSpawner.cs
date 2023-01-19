using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawner : MonoBehaviour
{
    //Put this script on an empty game object
    //Requires Waypoints with "Waypoint" tag
    [SerializeField]
    private GameObject SpiderType;
    [SerializeField]
    private int SpidersLeftToSpawn;
    [SerializeField]
    private int maxSpidersAliveAtOnce;
    [SerializeField]
    private float originalSpawnTimer = 0f;
    [SerializeField]
    private float timerBetweenNewSpiders = 3.0f;

    public int currentAliveSpiderCount;
    private GameObject gameManager;
    private GameObject[] waypointList;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        waypointList = GameObject.FindGameObjectsWithTag("Waypoint");
        currentAliveSpiderCount = maxSpidersAliveAtOnce;
        StartCoroutine(startSpawnSpider(originalSpawnTimer));
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAliveSpiderCount < maxSpidersAliveAtOnce && SpidersLeftToSpawn > 0)
        {
            SpidersLeftToSpawn--;
            currentAliveSpiderCount++;
            StartCoroutine(spawnSpider(timerBetweenNewSpiders));
        }
    }

    IEnumerator spawnSpider(float timer)
    {
        yield return new WaitForSeconds(timer);
        Vector3 randomSpawn = waypointList[Random.Range(0, waypointList.Length)].transform.position;
        Instantiate(SpiderType, randomSpawn, Quaternion.identity);
    }

    IEnumerator startSpawnSpider(float timer)
    {
        for (int i = maxSpidersAliveAtOnce; i > 0; i--)
        {
            yield return new WaitForSeconds(timer);
            Vector3 randomSpawn = waypointList[Random.Range(0, waypointList.Length)].transform.position;
            Instantiate(SpiderType, randomSpawn, Quaternion.identity);
        }
    }
}
