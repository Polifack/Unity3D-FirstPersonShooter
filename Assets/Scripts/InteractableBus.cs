using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableBus : InteractuableObject
{
    public override void onInteract()
    {
        GameStateManager.instance.toState(new PlayState());
    }
}
