using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase de prueba del Pathfinding. Su objetivo es seguir a un elemento "target"

public class Unit : MonoBehaviour
{
    public Transform target;
    private float speed = 10f;
    private Vector3[] path;
    private int targetIndex;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Requesting new path");
            PathRequestManager.instance.RequestPath(transform.position, target.position, OnPathFound);
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccess)
    {
        Debug.Log("Following path");
        if (pathSuccess)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        else
        {
            Debug.Log("Error: path not found");
        }
    }
    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }
}
