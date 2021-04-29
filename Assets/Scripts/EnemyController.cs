using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHP = 10;
    private int currentHP;
    private Room belongingRoom;

    public void doInit(GameObject t, Room r)
    {
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

    void Update()
    {
    }
}
