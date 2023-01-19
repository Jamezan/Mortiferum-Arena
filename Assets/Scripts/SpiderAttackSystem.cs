using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderAttackSystem : MonoBehaviour
{
    //Requirements for functionality: Player with tag "Player" & PlayerStats script
    //Bullet with "Bullet tag"
    //Spider Spawner with "Spider Spawner"
    private GameObject player;
    private NavMeshAgent enemyAgent;
    private GameObject spiderSpawner;
    private GameObject gameManager;
    public bool targettingPlayer = false;

    [SerializeField]
    GameObject gemDrop;
    [SerializeField]
    private float attackCooldown = 1f;
    [SerializeField]
    private int health = 200;
    [SerializeField]
    private int damage = 50;
    [SerializeField]
    private bool isBoss = false;
    public int currentHealth = 0;
    private float originalSpeed;
    public bool isBusy = false;
    private Animation anim;
    private float attackRadius = 2;
    private bool inRange = false;
    [SerializeField]
    private bool WillNotMoveIfSafe = true;
    public bool damagedByPlayer;
    private bool isdead;
    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        player = GameObject.FindGameObjectWithTag("Player");
        spiderSpawner = GameObject.FindGameObjectWithTag("SpiderSpawner");
        enemyAgent = this.gameObject.GetComponent<NavMeshAgent>();
        currentHealth = health;
        originalSpeed = enemyAgent.speed;
        anim = this.gameObject.GetComponent<Animation>();
        anim.Play("run");
        if (isBoss == true)
        {
            attackRadius = 4;
        }
    }

    /*private void Update()
    {

    }*/

    IEnumerator attackPlayer()
    {
        isBusy = true;
        enemyAgent.speed = 0;
        player.GetComponent<PlayerStats>().TakeDamage(damage);
        anim.Play("attack1");
        yield return new WaitForSeconds(attackCooldown);
        setSpeed();
        anim.Play("run");
        isBusy = false;

        if (inRange == true && targettingPlayer == true && isBusy == false && currentHealth > 0)
        {
            StartCoroutine(attackPlayer());
        }
    }

    public IEnumerator spiderDeath()
    {
        isdead = true;
        isBusy = true;
        targettingPlayer = false;
        anim.Play("death2");
        yield return new WaitForSeconds(3);
        Instantiate(gemDrop, this.gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (spiderSpawner != null)
        {
            spiderSpawner.GetComponent<SpiderSpawner>().currentAliveSpiderCount--;
        }

        if (damagedByPlayer == true)
        {
            gameManager.GetComponent<GameManager>().spidersKilled += 1;
            gameManager.GetComponent<GameManager>().spidersKilledThisRound += 1;
        }
    }

    public void setSpeed()
    {
        if (currentHealth <= (health / 2) && currentHealth > 0) {
            enemyAgent.speed = originalSpeed / 2;
        }
        else if (currentHealth <= 0 || isdead == true || isBusy == true)
        {
            enemyAgent.speed = 0;
        }
        else
        {
            enemyAgent.speed = originalSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            currentHealth -= player.GetComponent<PlayerStats>().damage;
            setSpeed();
            damagedByPlayer = true;
            if(currentHealth <= 0)
            {
                StartCoroutine(spiderDeath());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHitbox" && targettingPlayer == true && isBusy == false && currentHealth > 0)
        {
            inRange = true;
            StartCoroutine(attackPlayer());

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerHitbox")
        {
            inRange = false;
        }
    }
}