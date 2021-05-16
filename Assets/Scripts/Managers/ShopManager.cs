using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI currentText;
    

    public void shopButtonCallback()
    {
        if (GameManager.instance.staticGameData.money > 10)
        {
            GameManager.instance.staticGameData.maxTime += 10;
            GameManager.instance.staticGameData.money -= 10;
        }
    }

    public void exitButtonCallback() {
        GameManager.instance.setMouseLock(true);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        priceText.text = "10";
        currentText.text =GameManager.instance.staticGameData.maxTime+"s";
    }

}
