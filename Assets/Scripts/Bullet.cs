using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction = Vector3.zero;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.rigidbody.gameObject.name);
    }

    public void setup(Vector3 direction)
    {

    }

    private void Update()
    {
        this.transform.Translate(direction);
    }

}
