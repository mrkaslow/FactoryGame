using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpCommand : ICommand
{
    private Platform _platform;

    public MoveUpCommand(Platform platform)
    {
        this._platform = platform;
    }

    public void Execute()
    {
        this._platform.MovePlatform(1,0);
    }
}
