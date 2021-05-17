using System.Collections;
using UnityEngine;


public class ShootingEnemyController : AbstractEnemyController
{
    // Shooting point and bullet
    public GameObject bulletGO;
    public Transform shootingPoint;

    // States
    EnemyShootState shootState = new EnemyShootState();
    EnemyIdleState idleState = new EnemyIdleState();
    EnemyWalkState walkState = new EnemyWalkState();
    EnemyDieState dieState = new EnemyDieState();

    // Shooting enemy unique stats
    public float bulletSpeed;
    public float weaponRange;
    
    public float shootingTime;
    private float shootingCounter;
    public int maxNumberShoots;
    private float numberShots;
    private int numberShootsCounter;

    public override void activateEnemy()
    {
        goToState(walkState);
    }
    public override void setDeathState()
    {
        AudioManager.instance.playEnemyDeathSFX();
        goToState(dieState);
    }

    public void doShoot()
    {
        AudioManager.instance.playEnemyShootSFX();

        Vector3 playerPos = GameManager.instance.getPlayerPosition();
        Vector3 shootDirection = (playerPos - shootingPoint.transform.position);

        GameObject b = Instantiate(bulletGO, shootingPoint.position, Quaternion.identity);
        Bullet bullet = b.GetComponent<Bullet>();
        bullet.setup(shootDirection, damage, bulletSpeed);

        shootingCounter = 0;
    }

    public void getNextState()
    {
        if (numberShootsCounter < numberShots)
        {
            goToState(shootState);
            numberShootsCounter++;
        }
        else
        {
            goToState(walkState);
        }
    }

    public override void checkAttackCondition()
    {
        if (shootingCounter > shootingTime * Random.Range(0.7f, 1.3f))
        {
            numberShots = Random.Range(1, maxNumberShoots);
            numberShootsCounter = 0;
            goToState(shootState);
        }
        else
        {
            shootingCounter += Time.deltaTime;
        }
    }

    public override void setInitialState(){ setState(idleState); }
}
