using System.Collections;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public PathRequestManager pathRequest;
    public GameObject enemyTarget;
    public Room belongingRoom;
    public Animator animator;
    public GameObject bulletGO;
    public Transform shootingPoint;
    
    EnemyState currentState;
    EnemyShootState shootState = new EnemyShootState();
    EnemyIdleState idleState = new EnemyIdleState();
    EnemyWalkState walkState = new EnemyWalkState();
    EnemyDieState dieState = new EnemyDieState();

    public int maxHealth;
    private int currentHealth;

    public float movementSpeed;
    public int damage;
    public float bulletSpeed;
    public float weaponRange;
    
    public float shootingTime;
    private float shootingCounter;
    public int numberShoots;
    private int numberShootsCounter;

    public void doInit(GameObject t, Room r, PathRequestManager prm)
    {
        pathRequest = prm;
        enemyTarget = t;
        belongingRoom = r;
        currentHealth = maxHealth;
        
        currentState = idleState;
        currentState.ec = this;
    }
    
    public void activateEnemy()
    {
        currentState = currentState.toState(walkState);
    }

    public void takeDamage(int ammount)
    {
        currentHealth -= ammount;
        if (currentHealth <= 0)
        {
            belongingRoom.doRemoveEnemy(gameObject);
            currentState = currentState.toState(dieState);
        }
    }

    public void doDie()
    {
        belongingRoom.doRemoveEnemy(transform.parent.gameObject);
        Destroy(gameObject);
    }

    public void doShoot()
    {
        Vector3 playerPos = GameManager.instance.getPlayerPosition();
        Vector3 shootDirection = (playerPos - transform.position);

        GameObject b = Instantiate(bulletGO, shootingPoint.position, Quaternion.identity);
        Bullet bullet = b.GetComponent<Bullet>();

        bullet.setup(shootDirection, damage, bulletSpeed);

        shootingCounter = 0;
    }

    public void getNextState()
    {
        if (numberShootsCounter < numberShoots)
        {
            Debug.Log(numberShootsCounter);
            currentState = currentState.toState(shootState);
            numberShootsCounter++;
        }
        else
        {
            currentState = currentState.toState(walkState);
        }
    }

    public void checkShoot()
    {
        if (shootingCounter > shootingTime * Random.Range(0.7f, 1.3f))
        {
            numberShoots = Random.Range(1, numberShoots);
            numberShootsCounter = 0;
            currentState = currentState.toState(shootState);
        }
        else
        {
            shootingCounter += Time.deltaTime;
        }
    }

    private void Update()
    {
        currentState.stateUpdate();
    }
}
