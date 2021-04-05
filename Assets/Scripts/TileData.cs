using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    // Scriptable object that stores a list of tiles
    // and the gameobject to render ABOVE those tiles

    public TileBase[] tiles;
    public GameObject renderObject;

}
