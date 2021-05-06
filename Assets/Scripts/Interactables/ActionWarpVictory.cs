using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWarpVictory : ActionAbstract
{
    public override void execute()
    {
        GameStateManager.instance.toState(new VictoryState());
    }
}
