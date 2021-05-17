using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionHeal : ActionAbstract
{
    public int ammount;

    public override void execute()
    {
        AudioManager.instance.playHealthPickupSFX();
        PlayerController pc = GameManager.instance.getPlayer().GetComponent<PlayerController>();
        pc.doTakeHealing(ammount);
    }
}
