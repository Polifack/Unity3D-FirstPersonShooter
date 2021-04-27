using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void Awake()
    {
        // Doors will always be opened at the start
        Open();
    }

    public void Open()
    {
        Vector3 p = transform.position;
        p.z -= 2;
        transform.position = p;
    }

    public void Close()
    {
        Vector3 p = transform.position;
        p.z += 2;
        transform.position = p;
    }
}
