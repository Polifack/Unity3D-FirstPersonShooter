using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject target;

    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Move()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
