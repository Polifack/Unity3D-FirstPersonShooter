using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float aimTime = 2;                   // Time before the "aim" dissapears    
    private float aimCounter = 0;
    
    private GameObject interactionMark;         // Gameobject of the interaction mark
    
    private ActionAbstract action;              // Action to be executed
    
    private void Start()
    {
        interactionMark = GetComponentInChildren<SpriteLookAtPlayer>().gameObject;
        interactionMark.SetActive(false);

        action = GetComponent<ActionAbstract>();
    }
    public void onAim()
    {
        aimCounter = 2;
        interactionMark.SetActive(true);
    }

    public virtual void onInteract()
    {
        action.execute();
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
