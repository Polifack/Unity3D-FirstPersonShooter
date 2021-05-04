using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject doorModel;

    private void Awake()
    {
        // Doors will always be opened at the start
        Open();
    }

    public void Open()
    {
        Vector3 p = doorModel.transform.position;
        p.z -= 2;
        doorModel.transform.position = p;
    }

    public void Close()
    {
        Vector3 p = doorModel.transform.position;
        p.z += 2;
        doorModel.transform.position = p;
    }

}
