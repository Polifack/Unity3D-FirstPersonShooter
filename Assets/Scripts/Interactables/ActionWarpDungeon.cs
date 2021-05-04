using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWarpDungeon : ActionAbstract
{
    public override void execute()
    {
        GameStateManager.instance.toState(new PlayState());
    }
}
