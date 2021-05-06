using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateName
{
    lobby,
    dungeon,
    mainMenu,
    victoryScreen,
    defeatScreen
}

public interface GameState
{
    GameStateName getName();

    void onEnter();
    void onExit();
    void Update();
}

public class LobbyState : GameState
{
    // State whenever the player is in the lobby
    public GameStateName getName()
    {
        return GameStateName.lobby;
    }

    public void onEnter()
    {
        // Scene 0 -> Lobby Scene
        GameManager.instance.loadScene(0);

        // Lock mouse
        GameManager.instance.setMouseLock(true);


    }

    public void onExit()
    {
        // Load partial data based on general data
    }

    public void Update()
    {
        // do nothing 
    }
}

public class VictoryState : GameState
{
    // State whenever the player is in the lobby
    public GameStateName getName()
    {
        return GameStateName.victoryScreen;
    }

    public void onEnter()
    {
        // Scene 2 -> RunawayScene
        GameManager.instance.loadScene(2);

        GameManager.instance.setMouseLock(false);
    }

    public void onExit()
    {
        // Save partial data to general data
        GameManager.instance.saveCurrentGameData();
    }

    public void Update()
    {
        // do nothing 
    }
}

public class PlayState : GameState
{
    // State whenever the player is in the dungeon
    public float currentTime = 100;
    public int currentMoney = 0;
    public int currentFloor = 1;
    public int enemiesKilled = 1;
    
    public GameStateName getName()
    {
        return GameStateName.dungeon;
    }

    public void increaseMoney(int ammount)
    {
        currentMoney += ammount;
    }

    public void onEnter()
    {
        // Load max values from the static data
        int maxHealth = GameManager.instance.staticGameData.maxHP;
        int maxTime = GameManager.instance.staticGameData.maxTime;

        // Set the max values in the current data
        GameManager.instance.currentGameData.setMaxData(maxHealth, maxTime);

        // Load the scene
        GameManager.instance.loadScene(1);
    }

    public void onExit()
    {
        // Set the current game data to the manager
        GameManager.instance.currentGameData.setData(currentMoney, currentFloor, enemiesKilled);
    }    

    public void Update()
    {
        if (UIManager.instance != null)
        {

            currentTime -= Time.deltaTime;
            UIManager.instance.setTime(currentTime);
            UIManager.instance.setMoney(currentMoney);

            if (currentTime <= 0)
            {
                // Timed out
                // Spawn strong enemies in start point
            }
        }
    }
}



public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    private GameState state = null;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (state == null)
        {
            state = new LobbyState();   
        }
        state.onEnter();
    }

    public GameState getState()
    {
        return state;
    }

    public void toState(GameState _state)
    {
        state.onExit();
        state = _state;
        state.onEnter();
    }

    void Update()
    {
        state.Update();       
    }
}
