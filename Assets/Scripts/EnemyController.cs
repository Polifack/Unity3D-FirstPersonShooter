using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject target;
    public GameObject sprite;
    public int maxHP = 10;

    private int currentHP;
    private Room belongingRoom;

    public void doInit(GameObject t, Room r)
    {
        target = t;
        belongingRoom = r;
        currentHP = maxHP;
    }

    private void doDie()
    {
        belongingRoom.doRemoveEnemy(gameObject);
        Destroy(gameObject);
    }

    public void takeDamage(int ammount)
    {
        currentHP -= ammount;
        if (currentHP <= 0)
        {
            doDie();
        }
    }

    private void doRotate()
    {
        float tY = target.transform.position.y;
        float sY = transform.position.y;
        float tX = target.transform.position.x;
        float sX = transform.position.x;
        float angle = (Mathf.Atan2(tY - sY, tX - sX) * 180 / Mathf.PI);

        Vector3 eulers = new Vector3(0, -angle, 0);
        sprite.transform.localEulerAngles = eulers;
    }

    void Update()
    {
        if (target != null){
            doRotate();
        }
    }
}
