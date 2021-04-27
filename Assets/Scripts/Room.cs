using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public List<GameObject> Enemies;
    public List<GameObject> Doors;

    public List<TileData> tileDatas;
    public Vector2 roomWorldSize;

    private bool isCleared = false;
    private bool isPlaying = false;
    public void setGridData(Vector2 rws)
    {
        roomWorldSize = rws;
    }

    public void doRemoveEnemy(GameObject enemy)
    {
        Enemies.Remove(enemy);
        if (Enemies.Count == 0)
        {
            isCleared = true;
            doOpenDoors();
        }
    }
    public void doOpenDoors()
    {
        Debug.Log("Opening doors");
        foreach (GameObject door in Doors)
        {
            Door d = door.GetComponent<Door>();
            d.Open();
        }
    }

    public void playerEnterRoom()
    {
        // Check if it is already cleared or is already being played
        if (isCleared || isPlaying)
            return;
        foreach (GameObject door in Doors)
        {
            Door d = door.GetComponent<Door>();
            d.Close();
            isPlaying = true;
        }
    }

    public void addDoor(GameObject door)
    {
        Debug.Log("Adding doors");
        Doors.Add(door);
    }

    private void Start()
    {
        Debug.Log(roomWorldSize);
        // Set target in the enemies
        Debug.Log(transform.position);
        
        foreach (GameObject enemy in Enemies)
        {
            EnemyController ec = enemy.GetComponent<EnemyController>();
            ec.doInit(GameManager.instance.getPlayer(),this);
        }
    }
}
