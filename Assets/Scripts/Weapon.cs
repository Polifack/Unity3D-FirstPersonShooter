using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shooter;

    // data
    public float MaxCD = 10;
    public float currentCD = 0;

    bool isShooting()
    {
        return (Input.GetMouseButton(0));
    }
    Ray getShootingRay()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        return ray;
    }
    LayerMask getLayerMask()
    {
        return GameManager.instance.whatIsEnemy;
    }
    void Update()
    {
        if (currentCD < MaxCD)
        {
            currentCD+=Time.deltaTime;
            return;
        }

       if (isShooting())
       {
            Ray shootingRay = getShootingRay();
            RaycastHit hitInfo;
            if (Physics.Raycast(shootingRay, out hitInfo)){
                LayerMask target = getLayerMask();
                if ((target & 1 << hitInfo.collider.gameObject.layer) == 1 << hitInfo.collider.gameObject.layer)
                {
                    EnemyController ec = hitInfo.collider.gameObject.GetComponentInParent<EnemyController>();
                    GameObject b = Instantiate(bullet, hitInfo.point, shooter.transform.rotation);

                    ec.takeDamage(5);
                }
            }
            currentCD = 0;
       }
    }
}
