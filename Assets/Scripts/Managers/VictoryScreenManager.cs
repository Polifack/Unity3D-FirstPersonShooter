using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryScreenManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI floorText;
    public TextMeshProUGUI enemiesText;

    private void Start()
    {
        moneyText.text = "Money: " + GameManager.instance.currentGameData.currentMoney;
        floorText.text = "Floor: " + GameManager.instance.currentGameData.currentFloor;
        enemiesText.text = "Enemies: " + GameManager.instance.currentGameData.enemiesKilled;
    }

    public void buttonCallback()
    {
        GameStateManager.instance.toState(new LobbyState());
    }
}
