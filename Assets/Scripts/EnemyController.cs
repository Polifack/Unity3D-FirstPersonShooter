using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject target;
    
    void Start()
    {
        
    }

    private void Move()
    {
        //
    }

    void Update()
    {
        float tY = target.transform.position.y;
        float sY = transform.position.y;
        float tX = target.transform.position.x;
        float sX = transform.position.x;
        float angle = (Mathf.Atan2(tY - sY, tX - sX) * 180 / Mathf.PI);

        Vector3 eulers = new Vector3(0, -angle, 0);
        transform.localEulerAngles = eulers;
        
    }
}
