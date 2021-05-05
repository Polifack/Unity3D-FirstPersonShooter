using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase de prueba del Pathfinding. Su objetivo es seguir a un elemento "target"

public class Unit : MonoBehaviour
{
    public Vector3 target;
    private float speed = 3;
    private Vector3[] path;
    private int pathIndex;
    private PathRequestManager prm;
    private bool doMove;
    
    
    public void setRequestManager(PathRequestManager _prm)
    {
        prm = _prm;
    }
    public void OnPathFound(Vector3[] newPath, bool pathSuccess)
    {
        if (pathSuccess)
        {
            path = newPath;
            pathIndex = 0;
        }
        doMove = true;
        prm.isProcessingPath = false;
    }
    public void startMoving(Vector3 newTarget)
    {
        // Set new target and start moving
        
        target = newTarget;
        if (target != null && prm != null)
            prm.RequestPath(transform.position, target, OnPathFound);
    }
    public bool isMoving()
    {
        return doMove;
    }
    public void stopMoving()
    {
        doMove = false;
    }


    private void Update()
    {
        if (doMove == false) return;
        Vector3 currentWaypoint = (path != null) ? path[pathIndex] : Vector3.zero;
        if (currentWaypoint == Vector3.zero) return;
        
        if (transform.position == currentWaypoint)
        {
            pathIndex++;
            if (pathIndex >= path.Length)
            {
                return;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
    }
}
