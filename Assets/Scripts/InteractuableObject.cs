using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuableObject : MonoBehaviour
{
    public float aimTime = 2; // Time before the "aim" dissapears    
    private GameObject interactionMark; // Gameobject of the interaction mark

    float aimCounter = 0;
    private void Start()
    {
        interactionMark = GetComponentInChildren<SpriteLookAtPlayer>().gameObject;
        interactionMark.SetActive(false);
                
    }
    public void onAim()
    {
        aimCounter = 2;
        interactionMark.SetActive(true);
    }

    public virtual void onInteract()
    {
        Debug.Log("Interacting");
    }

    private void Update()
    {
        if (aimCounter >= 0)
        {
            aimCounter -= Time.deltaTime;
        }
        else
        {
            interactionMark.SetActive(false);
        }
    }
}
