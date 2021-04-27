using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public GameObject[] roomList;

    private Dictionary<Vector3, Room> roomDictionary;
    private Vector2 delta = new Vector2(1, 1);          //Vector that indicates the "difference" between the player enter room
    private void Start()
    {
        roomDictionary = new Dictionary<Vector3, Room>();
        Vector3 startPoint = Vector3.zero;

        foreach (GameObject room in roomList){
            GameObject roomGO = Instantiate(room, startPoint, Quaternion.identity);
            Room r = roomGO.GetComponent<Room>();
            Vector2 roomSize = r.roomWorldSize;


            roomDictionary.Add(startPoint, r);
            startPoint += new Vector3(roomSize.x, 0, 0);
        }
    }

    private bool checkInside(Vector3 b1, Vector3 b2, Vector3 p)
    {
        return (p.x > (b1.x + delta.x) && p.y > (b1.y + delta.y) && p.x < (b2.x - delta.x) && p.y < (b2.y-delta.y));
    }

    private void Update()
    {
        foreach (Vector3 sp in roomDictionary.Keys)
        {
            Vector2 rws = roomDictionary[sp].roomWorldSize;
            Vector3 startingBounds = sp;
            Vector3 endingBounds = new Vector3(rws.x+sp.x, rws.y+sp.y, sp.z);
            Vector3 playerPos = GameManager.instance.getPlayer().transform.position;
            if(checkInside(startingBounds, endingBounds, playerPos))
            {
                roomDictionary[sp].playerEnterRoom();
            }
        }
    }
}
