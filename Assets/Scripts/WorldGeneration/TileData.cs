using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileData : ScriptableObject, IHeapItem<TileData>
{
    // Scriptable object that stores:
    // -> List of tiles
    // -> Gameobject to render ABOVE those tiles
    // -> Ladder check
    // -> Walkable check
    // -> Weight on the pathfinding algorithm


    public TileBase[] tiles;
    public GameObject renderObject;

    private int heapIndex;

    public bool isLadder;
    public bool isWalkable;

    public int gCost;
    public int hCost;

    public int fCost { get { return gCost + hCost; } }
    public int HeapIndex { get { return heapIndex; } set { heapIndex = value; } }

    public int CompareTo(TileData other)
    {
        int compare = (fCost.CompareTo(other.fCost));

        //Si el fCost es igual en ambos nodos, comparamos usando el hCost.
        if (compare == 0)
        {
            compare = hCost.CompareTo(other.hCost);
        }

        //Como nos va a interesar el nodo con los menores valores, devolvemos el resultado negativo.
        return -compare;
    }

}
