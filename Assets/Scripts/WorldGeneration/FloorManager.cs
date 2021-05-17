using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class FloorManager : MonoBehaviour
{
    public static FloorManager instance;

    // First floor rooms
    public List<RoomData> f1MidRoomList;
    public List<RoomData> f1InitRoomList;
    public List<RoomData> f1BossRoomList;

    // Second floor rooms
    public List<RoomData> f2MidRoomList;
    public List<RoomData> f2InitRoomList;
    public List<RoomData> f2BossRoomList;

    // Room dictionary for future functionalities
    private Dictionary<Vector3, Room> roomDictionary;

    private Vector3 instantiatePoint = Vector3.zero;
    
    
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        roomDictionary = new Dictionary<Vector3, Room>();
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

    private void disableAllTilemapRenderers()
    {
        List<GameObject> rootObjectsInScene = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjectsInScene);

        for (int i = 0; i < rootObjectsInScene.Count; i++)
        {
            TilemapRenderer[] allComponents = rootObjectsInScene[i].GetComponentsInChildren<TilemapRenderer>(true);
            for (int j = 0; j < allComponents.Length; j++)
            {
                Destroy(allComponents[j]);
            }
        }
    }

    public void generate(int floorNumber)
    {
        Vector3 startPoint = Vector3.zero;
        
        List<RoomData> shuffledMidRoom = null;
        List<RoomData> shuffledInitRooms = null;
        List<RoomData> shuffledBossRooms = null;

        switch (floorNumber)
        {
            case 1:
                shuffledMidRoom = f1MidRoomList;
                shuffledInitRooms = f1InitRoomList;
                shuffledBossRooms = f1BossRoomList;
                break;

            case 2:
                shuffledMidRoom = f2MidRoomList;
                shuffledInitRooms = f2InitRoomList;
                shuffledBossRooms = f2BossRoomList;
                break;

        }

        Shuffle(shuffledMidRoom);
        Shuffle(shuffledInitRooms);
        Shuffle(shuffledBossRooms);

        // Instantiate init room
        RoomData initRoomData = shuffledInitRooms[0];
        Room initRoom = instantiateRoom(initRoomData);

        // Instantiate middle room
        foreach (RoomData room in shuffledMidRoom)
        {
            instantiateRoom(room);
        }
        
        // Instantiate boss room
        RoomData bossRoomData = shuffledBossRooms[0];
        instantiateRoom(bossRoomData);

        // Set player in position
        GameManager.instance.getPlayer().transform.position = initRoom.getSpawnPoint().position;
        disableAllTilemapRenderers();
    }
}
