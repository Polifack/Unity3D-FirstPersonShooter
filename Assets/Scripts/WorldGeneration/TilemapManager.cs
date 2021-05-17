using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public Room belongingRoom;
    public bool showGrid = true;

    public float nodeSize;

    private Tilemap tilemap;
    private Dictionary<TileBase, TileData> tileDictionary;
    private Node[,] nodeGrid;

    private Vector2Int gridStartPosition;       // Position of the first tile
    private Vector2Int gridSize;                // Vector that indicates the "shape" of the grid
    private Vector2 gridWorldSize;              // Vector that stores height and weight of the grid

    private List<TileData> tileDatas;
    private int penaltyMin = 00;
    private int penaltyMax = 99;

    private void OnDrawGizmos()
    {
        //Marco princpal de la grid
        //El valor de Y es 1 y el valor de Z es el gridWorld.y porque estamos trabajando en un espacio tridimensional.
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        //Dibujamos los nodos de la grid asignandoles un color segun sean 'walkables' o no
        if (nodeGrid != null)
        {
            foreach (Node node in nodeGrid)
            {
                if (showGrid && node!=null)
                {
                    //Obtenemos el color del nodo relativo a su peso 
                    Color weight = Color.Lerp(Color.white, Color.black, Mathf.InverseLerp(penaltyMin, penaltyMax,
                        node.movementPenalty));

                    Gizmos.color = (node.walkable ? weight : Color.red);
                    Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeSize));
                }
            }
        }
    }

    private void Awake()
    {
        // Get tiledata from room
        tileDatas = GameManager.instance.tileDatas;

        // Get component and initialize dict
        tilemap = GetComponent<Tilemap>();
        nodeSize = tilemap.cellSize.x;

        tileDictionary = new Dictionary<TileBase, TileData>();

        // Create a reference for each tileData and tiles
        foreach (TileData td in tileDatas){
            foreach (TileBase tb in td.tiles)
            {
                tileDictionary.Add(tb, td);
            }
        }

        // Compress the bounds of the tilemap to fit only the drawn area
        tilemap.CompressBounds();

        // Get grid stats
        BoundsInt bounds = tilemap.cellBounds;

        gridSize = new Vector2Int(bounds.xMax - bounds.xMin, bounds.yMax - bounds.yMin);
        gridWorldSize = new Vector2(gridSize.x * tilemap.cellSize.x, gridSize.y * tilemap.cellSize.y);
        gridStartPosition = new Vector2Int(bounds.xMin, bounds.yMin);

        // Set the grid size in the room
        belongingRoom.setGridData(gridWorldSize);

        // Init the node grid
        // Note: If we are drawing the tilemap behind (0,0) this will crash.
        nodeGrid = new Node[gridSize.x, gridSize.y];

        // Parse all the tiles
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                // Position of the tile in the grid
                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                // Get the tile
                TileBase tileBase = tilemap.GetTile(tilePosition);

                if (tileBase != null)
                {
                    // If tile exists check if it is in dictionary
                    if (tileDictionary.ContainsKey(tileBase))
                    {
                        // Get the data associated with that tile
                        TileData td = tileDictionary[tileBase];
                        
                        // Parse the data
                        GameObject go = td.renderObject;
                        bool walkable = td.isWalkable;
                        int walkCost = td.fCost;

                        // Get the tile center position
                        Vector3 renderPosition = transform.position + tilePosition + (tilemap.cellSize / 2);

                        // Render the gameobject
                        GameObject generated = Instantiate(go, renderPosition, go.transform.rotation);

                        if ((GameManager.instance != null) &&((GameManager.instance.whatIsDoor & 1 << generated.layer) == 1 << generated.layer))
                        {
                            belongingRoom.addDoor(generated);
                        }

                        // Set the correct parent
                        generated.transform.SetParent(transform);

                        // Create a node in the grid
                        nodeGrid[x, y] = new Node(walkable, renderPosition, x, y, walkCost);
                    }
                }
            }
        }
    }
    public int getNumberOfTiles()
    {
        return Mathf.CeilToInt(gridSize.x * gridSize.y);
    }
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                //Consideramos que 0,0 es el nodo en al que le estamos calculando los vecinos, así que saltamos.
                if (x == 0 && y == 0) continue;

                //Obtenemos el nodo
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                //Comprobamos que los nodos se encuentran en la cuadricula
                if (checkX >= 0 && checkX < gridSize.x && checkY >= 0 && checkY < gridSize.y)
                {
                    neighbours.Add(nodeGrid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        while (worldPosition.x > gridWorldSize.x)
        {
            worldPosition.x -= gridWorldSize.x;
        }
        while (worldPosition.x > gridWorldSize.y)
        {
            worldPosition.x -= gridWorldSize.y;
        }
        int x = Mathf.FloorToInt(worldPosition.x);
        int y = Mathf.FloorToInt(worldPosition.y);

        return nodeGrid[x, y];
    }
}
