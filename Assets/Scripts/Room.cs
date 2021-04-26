using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject[] Door;

    public List<TileData> tileDatas;
    public Vector2 roomWorldSize;

    public void setGridData(Vector2 rws)
    {
        roomWorldSize = rws;
        Debug.Log(rws);
    }
}
