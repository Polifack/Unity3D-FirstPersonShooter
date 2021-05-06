using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public List<GameObject> Enemies;
    public List<GameObject> Doors;
    public Transform playerSpawnPoint;
    public Transform entryPoint;

    public GameObject firstFloor;
    public GameObject secondFloor;
    public GameObject thirdFloor;

    public Vector2 roomWorldSize;

    public float x, y, z;

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
        foreach (GameObject door in Doors)
        {
            Door d = door.GetComponent<Door>();
            d.Open();
        }
    }


    public void checkPlayerEnter()
    {
        // If the room is cleared or is already beeing played, exit
        if (isCleared || isPlaying)
            return;
        
        // If the room has no entry point, exit
        if (entryPoint == null)
        {
            return;
        }
        
        // Raycast a ray in the entry of the room
        // This can be replaced by a "collider trigger enter"

        Ray r = new Ray(entryPoint.position, new Vector3(0, 2, 0));
        bool rh = Physics.Raycast(r, 2, GameManager.instance.whatIsPlayer);
        if (rh)
        {
            foreach (GameObject door in Doors)
            {
                Door d = door.GetComponent<Door>();
                d.Close();
                isPlaying = true;
            }
            foreach (GameObject enemy in Enemies)
            {
                EnemyController e = enemy.GetComponentInChildren<EnemyController>();
                e.activateEnemy();
            }
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
        Doors.Add(door);
    }

    public Transform getSpawnPoint()
    {
        // Returns the player default spawn point
        // useful for future developments and init rooms
        return playerSpawnPoint;
    }

    private void Start()
    {
        foreach (GameObject enemy in Enemies)
        {
            EnemyController ec = enemy.GetComponentInChildren<EnemyController>();
            ec.doInit(GameManager.instance.getPlayer(),this, firstFloor.GetComponent<PathRequestManager>());
        }
    }
    private void Update()
    {
        checkPlayerEnter();
    }
}
