using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    StartRoom,
    MiddleRoom,
    BossRoom,
}

[CreateAssetMenu]
public class RoomData : ScriptableObject
{
    public GameObject roomObject;
    public RoomType type;
    public bool hasLeft;
    public bool hasRight;
    public bool hasUp;
    public bool hasDown;
}
