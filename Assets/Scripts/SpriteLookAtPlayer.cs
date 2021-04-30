using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLookAtPlayer : MonoBehaviour
{
    // Class that makes a sprite look always at player
    private GameObject target;
    public GameObject sr;
    
    private const float distance = 10; // Distance that the render will happen
    
    private void Start()
    {
        target = GameManager.instance.getPlayer();
    }
    private void doRotate()
    {
        float tY = target.transform.position.y;
        float sY = transform.position.y;
        float tX = target.transform.position.x;
        float sX = transform.position.x;
        float angle = (Mathf.Atan2(tY - sY, tX - sX) * 180 / Mathf.PI);

        Vector3 eulers = new Vector3(0, -angle, 0);
        sr.transform.localEulerAngles = eulers;
    }

    private void Update()
    {
        if (target == null || sr == null)
        {
            return;
        }

        if (Vector3.Distance(transform.position, target.transform.position) < distance)
        {
            doRotate();
        }
    }
}
