using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionExitGame : ActionAbstract
{
    public override void execute()
    {
        Application.Quit();
    }

}
