using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHeal : ActionAbstract
{
    public int ammount;

    public override void execute()
    {
        PlayerController pc = GameManager.instance.getPlayer().GetComponent<PlayerController>();
        pc.heal(ammount);
    }
}
