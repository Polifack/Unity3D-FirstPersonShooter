using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public void Open()
    {
        // This could be done like a corroutine
        // where this rotates 
        Destroy(this);
    }
}
