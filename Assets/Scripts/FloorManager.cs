using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public GameObject[] roomList;

    private void Start()
    {
        Vector3 startPoint = Vector3.zero;

        foreach (GameObject room in roomList){
            GameObject roomGO = Instantiate(room, startPoint, Quaternion.identity);
            Room r = roomGO.GetComponent<Room>();
            Vector2 roomSize = r.roomWorldSize;
            startPoint += new Vector3(roomSize.x, 0, 0);
        }
    }
}
