using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image filler;
    public TextMeshProUGUI hpText;

    private float maxHP;
    private float maxWidth;
    
    private void Start()
    {
        maxHP = GameManager.instance.staticGameData.maxHP;
        maxWidth = filler.rectTransform.localScale.x;

        hpText.text = maxHP + "/" + maxHP;
    }

    public void setValue(float newVal)
    {
        float newWidth = (newVal/maxHP)*maxWidth;
        Vector3 oldScale = filler.rectTransform.localScale;
        filler.rectTransform.localScale = new Vector3(newWidth, oldScale.y, oldScale.z);

        hpText.text = newVal + "/" + maxHP;
    }
}
