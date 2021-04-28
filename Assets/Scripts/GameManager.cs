using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public LayerMask whatIsPlayer;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsLadder;
    public LayerMask whatIsDoor;

    private GameObject player;


    private void Awake()
    {
        instance = this;
        // Initialize gamemanager data
        
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public GameObject getPlayer()
    {
        return player;
    }
    public Vector3 getPlayerPosition()
    {
        return player.transform.position;
    }
}
