using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    // This is the weapon data, could be abstracted elsewhere 
    // so it can be applied also to the player
    public int damage = 10;
    public float bulletSpeed = 6;
    public float range = 10;
    public float shootCadence = 1;

    // Enemy Shooting data
    public float shootingChance = 0.75f;

    // Weapon logic
    public Transform shootingPoint;
    public GameObject bulletGO;
    private float shootCounter = 0;

    private void Update()
    {
        // Weapon can shoot
        if (shootCounter < shootCadence)
        {
            shootCounter += Time.deltaTime;
            return;
        }

        // Get direction

        Vector3 playerPos = GameManager.instance.getPlayerPosition();
        Vector3 shootDirection = (playerPos - transform.position);
        float distance = Vector3.Distance(transform.position, playerPos);
        
        // Bullet will be shoot
        if (distance<range)
        {
            float doShoot = Random.Range(0, 1);
            // Will shoot
            if (doShoot < shootingChance)
            {
                GameObject b = Instantiate(bulletGO, shootingPoint.position, Quaternion.identity);
                Bullet bullet = b.GetComponent<Bullet>();

                bullet.setup(shootDirection, damage, bulletSpeed);
                shootCounter = 0;
            }
        }
    }
}
