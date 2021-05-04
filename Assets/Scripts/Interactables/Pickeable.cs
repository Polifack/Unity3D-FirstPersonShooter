using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// Pickeable main class
public class Pickeable : MonoBehaviour
{
    private ActionAbstract action;
    
    private void Awake()
    {
        if (action == null)
        {
            action = GetComponent<ActionAbstract>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        LayerMask target = GameManager.instance.whatIsPlayer;
        if ((target & 1 << go.layer) == 1 << go.layer)
        {
            action.execute();
            Destroy(gameObject);
        }
    }
}
