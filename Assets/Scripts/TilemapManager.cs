using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    private Tilemap tilemap;
    private Dictionary<TileBase, TileData> tileDictionary;

    [SerializeField]
    private List<TileData> tileDatas;



    private void Awake()
    {
        // Get component and initialize dict
        tilemap = GetComponent<Tilemap>();
        tileDictionary = new Dictionary<TileBase, TileData>();

        // Create a reference for each tileData and tiles
        foreach (TileData td in tileDatas){
            foreach (TileBase tb in td.tiles)
            {
                Debug.Log("[*] Adding to dict " + tb);
                tileDictionary.Add(tb, td);
            }
        }
    }

    private void Start()
    {
        // Parse the whole tilemap
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.size.x; x++)
        {
            for (int y = bounds.yMin; y < bounds.size.y; y++)
            {
                // Get the tile at position
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(tilePosition);

                if (tile != null)
                {
                    // If tile exists check if it is in dictionary
                    if (tileDictionary.ContainsKey(tile))
                    {
                        //Render the gameobject assigned
                        TileData td = tileDictionary[tile];
                        GameObject go = td.renderObject;

                        // Move the tile position to the center
                        Vector3 cSize = tilemap.cellSize/2;
                        Vector3 tPosition = tilePosition+cSize;

                        // Instantiate the gameobject in the data and set it as a child
                        GameObject wall = Instantiate(go, tPosition, Quaternion.identity);
                        wall.transform.SetParent(this.transform);
                    }
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
