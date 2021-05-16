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
    public LayerMask whatIsDoor;
    public LayerMask whatIsInteractable;
    public LayerMask whatIsWall;

    public List<TileData> tileDatas;

    private GameObject player;
    private AsyncOperation asyncLoadLevel;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);

        // Sample values
        staticGameData.maxHP = 100;
        staticGameData.maxTime = 100;
        staticGameData.money = 0;
    }

    public void saveStaticData()
    {
        // the only data that we will save is the current money
        Debug.Log("saving");
        staticGameData.updateMoney(currentGameData.currentMoney);
    }
    public void saveCurrentData(int money, int floor, int enemies, int time)
    {
        currentGameData.setData(money, floor, enemies, time);
    }
    public void loadScene(int number)
    {
        SceneManager.LoadScene(number);
        
    }
    public void loadBankScene(int floorNumber)
    {
        StartCoroutine("LoadBankScene",floorNumber);
    }
    IEnumerator LoadBankScene(int fn)
    {
        asyncLoadLevel = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
        if (FloorManager.instance != null)
        {
            Debug.Log("generating floor for real");
            FloorManager.instance.generate(fn);
        }
    }

    public void setMouseLock(bool lockCursor)
    {
        if (lockCursor)
        {
            if (CameraController.instance!=null)
                CameraController.instance.setActiveRotation(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            if (CameraController.instance != null)
            {
                CameraController.instance.setActiveRotation(false);
            }
                
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
