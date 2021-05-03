using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        // load scene of the lobby
        // load data
        SceneManager.LoadScene(0);
    }

    public void onExit()
    {
        // save data 
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
    public GameStateName getName()
    {
        return GameStateName.dungeon;
    }

    public void onEnter()
    {
        SceneManager.LoadScene(1);
    }

    public void onExit()
    {
        // Save data
    }    

    public void Update()
    {
        if (UIManager.instance != null)
        {

            currentTime -= Time.deltaTime;
            UIManager.instance.setTime(currentTime);

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
