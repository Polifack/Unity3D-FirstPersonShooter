using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    public CurrentRunData currentGameData = new CurrentRunData();
    public GameData staticGameData = new GameData();

    public LayerMask whatIsPlayer;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsLadder;
    public LayerMask whatIsDoor;
    public LayerMask whatIsInteractable;

    public List<TileData> tileDatas;

    private GameObject player;

    private AsyncOperation asyncLoadLevel;

    private void Awake()
    {
        instance = this;

        // Sample values
        staticGameData.maxHP = 100;
        staticGameData.maxTime = 100;
        staticGameData.money = 0;
    }
    
    public void resetCurrentGameData()
    {
        currentGameData = new CurrentRunData();
    }

    public void saveCurrentGameData()
    {
        staticGameData.money += currentGameData.currentMoney;
    }

    public void loadScene(int number)
    {
        StartCoroutine("LoadLevel", number);
    }
    IEnumerator LoadLevel(int number)
    {
        asyncLoadLevel = SceneManager.LoadSceneAsync(number, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
    }

    public void setMouseLock(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
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
