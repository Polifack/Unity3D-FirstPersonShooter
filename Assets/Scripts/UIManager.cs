using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI timeText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        if (timeText==null)
        {
            Debug.LogError("Error > No text component");
        }
    }

    public void setTime(float ammount)
    {
        string textContent = "-- " + Mathf.Floor(ammount) + " --";
        timeText.text = textContent;
    }
}
