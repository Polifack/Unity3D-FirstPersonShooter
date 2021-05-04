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
        instance = this;
    }

    public GameObject getPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        return player;
    }
    public Vector3 getPlayerPosition()
    {
        return player.transform.position;
    }
}
