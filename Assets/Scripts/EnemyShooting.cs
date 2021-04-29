using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletGO;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameObject b = Instantiate(bulletGO, shootingPoint, this.transform);
            Bullet bullet = b.GetComponent<Bullet>();
            bullet.setup(new Vector3(1, 0, 0));

        }
    }
}
