using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathRequestManager : MonoBehaviour
{

    private Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    private PathRequest currentPathRequest;

    public Pathfinding pathfinding;

    bool isProcessingPath;
    public static PathRequestManager instance;

    void Awake()
    {
        instance = this;
    }

    public void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        Debug.Log("Requesting path from " + pathStart + " to " + pathEnd);
        Debug.Log("OnPahtFound: " + callback);
        
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        
        pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    void TryProcessNext()
    {
        if (!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;

            Debug.Log("Starting process of path from " + currentPathRequest.pathStart + " to " + currentPathRequest.pathEnd);
            Debug.Log("OnPathFound: " + currentPathRequest.callback);
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        Debug.Log("Finished process of path from " + currentPathRequest.pathStart + " to " + currentPathRequest.pathEnd);
        Debug.Log("Result: " + success);
        Debug.Log("OnPathFound: " + currentPathRequest.callback);

        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }

    }
}
