using TMPro;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;

    public Room lobbyRoom;
    public HealthBar hpbar;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI timeText;


    void Start()
    {
        GameManager.instance.getPlayer().transform.position = lobbyRoom.getSpawnPoint().position;

        updateCurrentHP();
        updateMoney();
        updateTime();
    }

    public void updateMoney()
    {
        moneyText.text = "Money: " + GameManager.instance.staticGameData.money;
    }
    public void updateTime()
    {
        timeText.text = "Time: " + GameManager.instance.staticGameData.maxTime;
    }
    public void updateCurrentHP()
    {
        hpbar.setValue(GameManager.instance.staticGameData.maxHP);
    }
}
