﻿using System.Collections;
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
        GameManager.instance.saveStaticData();
    }

    public void Update()
    {
        // do nothing 
    }
}

public class MainMenuState : GameState
{
    // State whenever the player is in the lobby
    public GameStateName getName()
    {
        return GameStateName.mainMenu;
    }

    public void onEnter()
    {
        GameManager.instance.setMouseLock(false);
    }

    public void onExit()
    {
    }

    public void Update()
    {
    }
}
public class DefeatState : GameState
{
    // State whenever the player is in the lobby
    public GameStateName getName()
    {
        return GameStateName.defeatScreen;
    }

    public void onEnter()
    {
        GameManager.instance.loadScene(4);
        GameManager.instance.setMouseLock(false);
    }

    public void onExit()
    {
    }

    public void Update()
    {
    }
}

public class PlayState : GameState
{
    // State whenever the player is in the dungeon
    public float currentTime;
    public int currentMoney;
    public int currentFloor;
    public int enemiesKilled;

    public void setInitialData()
    {
        // Data for when the player enters the dungeon

        currentTime = GameManager.instance.staticGameData.maxTime;
        currentMoney = 0;
        currentFloor = 1;
        enemiesKilled = 0;

        // load the scene and generate
        GameManager.instance.loadBankScene(currentFloor);
    }

    public void setNextFloorData()
    {
        // Data for when the player advances floor
        currentTime = GameManager.instance.currentGameData.currentTime;
        currentMoney = GameManager.instance.currentGameData.currentMoney;
        enemiesKilled = GameManager.instance.currentGameData.enemiesKilled;
        currentFloor = GameManager.instance.currentGameData.currentFloor+1;

        // load the scene and generate
        GameManager.instance.loadBankScene(currentFloor);
    }
    
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
    }

    public void onExit()
    {
        // Set the current game data to the manager
        GameManager.instance.saveCurrentData(currentMoney, currentFloor, enemiesKilled, (int)currentTime);
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
            state = new MainMenuState();
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

    // Auxiliar functions
    public void enterDungeonState()
    {
        toState(new PlayState());
        PlayState ps = (PlayState)state;
        ps.setInitialData();
    }
    public void goToNextFloor()
    {
        toState(new PlayState());
        PlayState ps = (PlayState)state;
        ps.setNextFloorData();
    }

    void Update()
    {
        state.Update();       
    }
}
