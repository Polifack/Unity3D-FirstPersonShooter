using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRunData
{
    // Class that defines the data for the current run
    // This will be used to save afterwards and for the victory screen

    public int currentMoney;
    public int maxHP;
    public int maxTime;
    public int currentTime;
    public int currentFloor;
    public int enemiesKilled;

    public void setMaxData(int maxhp, int maxtime)
    {
        maxHP = maxhp;
        maxTime = maxtime;
    }

    public void setData(int m, int f, int e, int t)
    {
        currentMoney = m;
        currentFloor = f;
        enemiesKilled = e;
        currentTime = t;
    }

}

public class GameData
{
    public int money;
    public int maxHP = 100;
    public int maxTime = 100;

    public void updateData(int dm, int dt)
    {
        money += dm;
        maxTime += dt;
    }
    public void updateMoney(int dm)
    {
        money += dm;
    }
    public void updateTime(int dt)
    {
        maxTime += dt;
    }
    public void updateMaxHP(int dhp)
    {
        maxHP += dhp;
    }
}