using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    // Clase que permite realizar pathfinding usando el algoritmo A*
    private PathRequestManager requestManager;                 
    private TilemapManager grid;

    Node startNode = null;
    Node endNode = null;

    private void OnDrawGizmos()
    {
        if (startNode != null)
        {
            Gizmos.color = Color.green;
            //Gizmos.DrawCube(startNode.worldPosition, Vector3.one * (grid.nodeSize));
        }
        if (endNode != null)
        {
            Gizmos.color = Color.blue;
            //Gizmos.DrawCube(endNode.worldPosition, Vector3.one * (grid.nodeSize));
        }
    }

    private void Awake()
    {
        grid = GetComponent<TilemapManager>();
        requestManager = GetComponent<PathRequestManager>();
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        // Ignore z value
        startPos.z = 0;
        targetPos.z = 0;

        //Función que nos permite ejecutar una busqueda de caminos en paralelo mediante corrutinas.
        StartCoroutine(FindPath(startPos, targetPos));
    }
    private IEnumerator FindPath(Vector3 startPos, Vector3 endPos)
    {
        startNode = grid.NodeFromWorldPoint(startPos);
        endNode = grid.NodeFromWorldPoint(endPos);

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        //Implementación del algoritmo A*. 
        Heap<Node> openSet = new Heap<Node>(grid.getNumberOfTiles());
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            //Si el nodo en el que estamos es la meta, terminamos la exploración
            if (currentNode == endNode)
            {
                RetracePath(startNode, endNode);
                pathSuccess = true;

                break;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                //Si el nodo no es atravesable o ya ha sido explorado, nos lo saltamos
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                //Calculamos el coste del camino hasta ese nodo (añadiendo la penalización del movimiento por terreno de dicho nodo)
                int newPathToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour) + neighbour.movementPenalty;

                //Si el nodo no ha sido inicializado o se ha encontrado un camino más corto hasta ese nodo
                //actualizamos/inicializamos los valores de g cost y h cost en el nodo así como le asignamos un nodo padre
                if (newPathToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newPathToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, endNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                    else openSet.UpdateItem(neighbour);
                }
            }
        }

        yield return null;

        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, endNode);
            requestManager.FinishedProcessingPath(waypoints, pathSuccess);
        }
        else
        {
            requestManager.FinishedProcessingPath(null, pathSuccess);
        }
    }
    private int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        //A la hora de calcular las distancias consideramos 10 para un desplazamiento recto y 14 en diagonal
        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
    private Vector3[] RetracePath(Node startNode, Node targetNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;

        //Retrocedemos utilizando los padres para obtener la lista de nodos a visitar para obtener el camino a la meta
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        

        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }
    private Vector3[] SimplifyPath(List<Node> path)
    {
        //Para mejorar la eficiencia de un path lo simplificamos para que solo se marquen los puntos importantes
        //Consideramos los puntos relevantes aquellos en los que el camino cambia de direcci´n
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 dirOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            //La nueva dirección se calcula como el vector que une los puntos de dos nodos consecutivos en el camino
            Vector2 dirNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (dirNew != dirOld)
            {
                waypoints.Add(path[i].worldPosition);
            }
            dirOld = dirNew;
        }

        return waypoints.ToArray();
    }
}
