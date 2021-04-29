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
    public LayerMask whatIsInteractable;

    private GameObject player;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        instance = this;
        // Initialize gamemanager data       
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
