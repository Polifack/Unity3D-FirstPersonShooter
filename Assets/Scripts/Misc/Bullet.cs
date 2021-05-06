using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Enemy Bullet Class

    private Vector3 direction = Vector3.zero;               // Direction that the bullet is being shot at
    private int damage;                                     // Damage that the bullet will do
    private float speed;                                    // Speed that the bullet will take

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.rigidbody.gameObject;
        LayerMask target = GameManager.instance.whatIsPlayer;
        if ((target & 1 << go.layer) == 1 << go.layer)
        {
            PlayerController p = go.GetComponent<PlayerController>();
            p.takeDamage(damage);
        }
        Destroy(gameObject);
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
    }

}
