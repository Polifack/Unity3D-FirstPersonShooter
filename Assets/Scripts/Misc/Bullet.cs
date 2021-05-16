using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Enemy Bullet Class

    private Vector3 direction = Vector3.zero;               // Direction that the bullet is being shot at
    private int damage;                                     // Damage that the bullet will do
    private float speed;                                    // Speed that the bullet will take

    private float timer = 1;                                // After 5 secs the bullet disapears
    private float timercounter = 0;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;
        LayerMask target = GameManager.instance.whatIsPlayer;
        if ((target & 1 << go.layer) == 1 << go.layer)
        {
            PlayerController p = go.GetComponent<PlayerController>();
            p.doTakeDamage(damage);
        }
        LayerMask shouldIDie = GameManager.instance.whatIsWall;
        if ((shouldIDie & 1 << go.layer) == 1 << go.layer)
        {
            Destroy(gameObject);
        }
        
    }

    public void setup(Vector3 direction, int damage, float speed)
    {
        this.direction = direction;
        this.damage = damage;
        this.speed = speed;
    }

    private void Update()
    {
        transform.Translate(direction*Time.deltaTime* speed);
        
        timercounter += Time.deltaTime;
        if (timercounter >= timer)
        {
            Destroy(gameObject);
        }
    }

}
