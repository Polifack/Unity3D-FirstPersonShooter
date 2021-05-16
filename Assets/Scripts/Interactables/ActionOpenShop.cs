using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOpenShop : ActionAbstract
{
    public override void execute()
    {
        LobbyManager.instance.enableShop();
    }    
}
