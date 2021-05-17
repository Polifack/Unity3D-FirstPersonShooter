using System.Collections;
using UnityEngine;


public class BossEnemyController : AbstractEnemyController
{
    // Not common
    EnemyBiteState biteState = new EnemyBiteState(); 
    EnemyIdleState idleState = new EnemyIdleState();
    EnemyWalkState walkState = new EnemyWalkState();
    EnemyDieState dieState = new EnemyDieState();

    public float biteRange;
    public float biteDamageRange;

    public float canBiteTime;
    private float canBiteCounter = 0;
    
    public int maxNumberBites;
    private int numberBites;
    private int numberBitesCounter;

    public override void activateEnemy()
    {
        goToState(walkState);
    }
    public override void setDeathState()
    {
        AudioManager.instance.playEnemyDeathSFX();
        goToState(dieState);
    }
    

    public void doBite()
    {
        AudioManager.instance.playBossGrowlSFX();
        canBiteCounter = 0;

        // If the player moves quickly dont deal him damage
        if (Vector3.Distance(GameManager.instance.getPlayerPosition(), transform.position) > biteRange)
        {
            return;
        }

        // Deal damage
        GameManager.instance.getPlayer().GetComponent<PlayerController>().doTakeDamage(damage);
    }

    public void getNextState()
    {
        if (numberBitesCounter < numberBites)
        {
            goToState(biteState);
            numberBitesCounter++;
        }
        else
        {
            goToState(walkState);
        }
    }

    public override void checkAttackCondition()
    {
        // If we are out of time dont waste time checking
        if (canBiteCounter < canBiteTime)
        {
            canBiteCounter += Time.deltaTime;
            return;
        }

        // If we are out of range just exit
        if (Vector3.Distance(GameManager.instance.getPlayerPosition(), transform.position) > biteRange)
        {
            return;
        }

        // Compute the number of bites and go to state
        numberBites = Random.Range(1, maxNumberBites);
        numberBitesCounter = 0;
        goToState(biteState);
    }

    public override void setInitialState() { setState(idleState); }
}
