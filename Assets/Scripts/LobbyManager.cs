using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public Room lobbyRoom;
    void Start()
    {
        GameManager.instance.getPlayer().transform.position = lobbyRoom.getSpawnPoint().position;
    }
}
