using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public List<RoomData> midRoomList;
    public List<RoomData> initRoomList;
    public List<RoomData> bossRoomList;

    // Room dictionary for future functionalities
    private Dictionary<Vector3, Room> roomDictionary;

    private Vector3 instantiatePoint = Vector3.zero;
    
    
    private void Start()
    {
        roomDictionary = new Dictionary<Vector3, Room>();
        generate();
    }

    public void Shuffle<T>(IList<T> list)
    {
        // Function that randomices a list based on fisher-yates 
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private Room instantiateRoom(RoomData room)
    {
        GameObject roomGO = Instantiate(room.roomObject, instantiatePoint, Quaternion.identity);
        Room r = roomGO.GetComponent<Room>();
        Vector2 roomSize = r.roomWorldSize;

        roomDictionary.Add(instantiatePoint, r);
        instantiatePoint += new Vector3(roomSize.x, 0, 0);

        return r;
    }

    private void generate()
    {
        Vector3 startPoint = Vector3.zero;

        List<RoomData> shuffledMidRoom = midRoomList;
        List<RoomData> shuffledInitRooms = initRoomList;
        List<RoomData> shuffledBossRooms = bossRoomList;

        Shuffle(shuffledMidRoom);
        Shuffle(shuffledInitRooms);
        Shuffle(shuffledBossRooms);

        // Instantiate init room
        RoomData initRoomData = shuffledInitRooms[0];
        Room initRoom = instantiateRoom(initRoomData);
        // Set player in position
        GameManager.instance.getPlayer().transform.position = initRoom.getSpawnPoint().position;

        // Instantiate middle room
        foreach (RoomData room in shuffledMidRoom)
        {
            instantiateRoom(room);
        }

        // Instantiate boss room
        RoomData bossRoomData = shuffledBossRooms[0];
        instantiateRoom(bossRoomData);
    }
}
