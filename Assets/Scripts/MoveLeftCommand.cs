using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftCommand : ICommand
{
    private Platform _platform;

    public MoveLeftCommand(Platform platform)
    {
        this._platform = platform;
    }

    public void Execute()
    {
        this._platform.MovePlatform(0,-1);
    }
}
