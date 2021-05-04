using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWarpLobby : ActionAbstract
{
    public override void execute()
    {
        GameStateManager.instance.toState(new LobbyState());
    }
}

