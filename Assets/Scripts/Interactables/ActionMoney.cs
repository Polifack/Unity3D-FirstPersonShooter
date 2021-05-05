using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMoney : ActionAbstract
{
    public int minAmmount;
    public int maxAmmount;

    private int ammount;


    private void Awake()
    {
        ammount = Random.Range(minAmmount, maxAmmount);
    }

    public override void execute()
    {
        GameState gs = GameStateManager.instance.getState();
        PlayState ps = (PlayState) gs;
        ps.increaseMoney(ammount);
        

    }
}
