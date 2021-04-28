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

    public bool[] getCompatibility(RoomData other)
    {
        bool[] result = new bool[4];

        //u r d l
        result[0] = hasUp && other.hasDown;
        result[1] = hasRight && other.hasLeft;
        result[2] = hasDown && other.hasUp;
        result[3] = hasLeft && other.hasRight;

        return result;
    }
}
