using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI moneyText;
    public HealthBar hpBar;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        if (timeText==null || moneyText==null)
        {
            Debug.LogError("Error > No text component");
        }
    }

    public void setTime(float ammount)
    {
        string textContent = "-- " + Mathf.Floor(ammount) + " --";
        timeText.text = textContent;
    }

    public void setMoney(int ammount)
    {
        string moneyContent = ammount + " $";
        moneyText.text = moneyContent;
    }

    public void setHealth(float ammount)
    {
        hpBar.setValue(ammount);
    }
}
