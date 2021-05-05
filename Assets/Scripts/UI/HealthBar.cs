using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image filler;

    private float maxHP;
    private float maxWidth;
    
    private void Start()
    {
        maxHP = GameManager.instance.getPlayer().GetComponent<PlayerController>().maxHP;
        maxWidth = filler.rectTransform.localScale.x;
    }

    public void setValue(float newVal)
    {
        float newWidth = (newVal/maxHP)*maxWidth;
        Vector3 oldScale = filler.rectTransform.localScale;
        filler.rectTransform.localScale = new Vector3(newWidth, oldScale.y, oldScale.z);
    }
}
