using UnityEngine;

public abstract class AbstractEnemyController : MonoBehaviour
{
    // Common components from all enemies
    public PathRequestManager pathRequest;
    public GameObject enemyTarget;
    public Room belongingRoom;
    public Animator animator;

    // Common stats for all enemies
    public int maxHealth;
    private int currentHealth;
    public float movementSpeed;
    public int damage;

    // EnemyState
    EnemyState currentState;

    // State functions 
    public abstract void setDeathState();
    public abstract void setInitialState();

    // Function to activate enemy
    public abstract void activateEnemy();

    // Function to check when to attaack
    public abstract void checkAttackCondition();


    // Function to allow subclasses change private state
    public void goToState(EnemyState targetState)
    {
        currentState = currentState.toState(targetState);
    }
    public void setState(EnemyState newState)
    {
        currentState = newState;
    }


    // Common behaviour
    public void doInit(GameObject t, Room r, PathRequestManager prm)
    {
        // Common function
        if (animator == null) Debug.Log("animator null");
        
        pathRequest = prm;
        enemyTarget = t;
        belongingRoom = r;
        currentHealth = maxHealth;

        // The setting of the initial state is delegated to each subclass
        setInitialState();
        currentState.ec = this;
    }
    public void takeDamage(int ammount)
    {
        // Common function

        currentHealth -= ammount;
        if (currentHealth <= 0)
        {
            belongingRoom.doRemoveEnemy(gameObject);
            setDeathState();
        }
    }
    public void doDie()
    {
        // Common function
        belongingRoom.doRemoveEnemy(transform.parent.gameObject);
        Destroy(gameObject);
    }

    // Update the state
    private void Update()
    {
        // Common
        if (currentState!=null)
            currentState.stateUpdate();
    }
}
