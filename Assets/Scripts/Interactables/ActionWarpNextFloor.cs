using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWarpNextFloor : ActionAbstract
{
    public override void execute()
    {
        GameStateManager.instance.goToNextFloor();
    }
}
