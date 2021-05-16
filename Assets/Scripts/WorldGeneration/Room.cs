using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public List<GameObject> Enemies;
    public List<GameObject> Doors;
    public RoomEntryChecker entryPoint;
    public Transform playerSpawnPoint;
    
    public GameObject firstFloor;
    public GameObject secondFloor;
    public GameObject thirdFloor;

    public Vector2 roomWorldSize;

    public float x, y, z;

    public void setGridData(Vector2 rws)
    {
        roomWorldSize = rws;
    }

    public void doRemoveEnemy(GameObject enemy)
    {
        Enemies.Remove(enemy);
        if (Enemies.Count == 0)
        {
            doOpenDoors();
        }
    }
    public void doOpenDoors()
    {
        foreach (GameObject door in Doors)
        {
            Door d = door.GetComponent<Door>();
            d.Open();
        }
    }

    public void activateRoom()
    {
        foreach (GameObject door in Doors)
        {
            Door d = door.GetComponent<Door>();
            d.Close();
        }

        foreach (GameObject enemy in Enemies)
        {
            enemy.SetActive(true);
            AbstractEnemyController e = enemy.GetComponentInChildren<AbstractEnemyController>();
            e.activateEnemy();
        }
        
    }

    public void addDoor(GameObject door)
    {
        Doors.Add(door);
    }

    public Transform getSpawnPoint()
    {
        return playerSpawnPoint;
    }

    private void Start()
    {
        foreach (GameObject enemy in Enemies)
        {
            AbstractEnemyController ec = enemy.GetComponentInChildren<AbstractEnemyController>();
            ec.doInit(GameManager.instance.getPlayer(),this, firstFloor.GetComponent<PathRequestManager>());
            enemy.SetActive(false);
        }

        if (entryPoint!=null)
            entryPoint.setRoom(this);
    }
}
