using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntryChecker : MonoBehaviour
{
    private Room currentRoom;

    public void setRoom(Room r)
    {
        currentRoom = r;
    }

    private void OnTriggerEnter(Collider other)
    {
        int layerMask = GameManager.instance.whatIsPlayer;
        if ((layerMask & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            currentRoom.activateRoom();
            Destroy(this);
        }
    }
}
