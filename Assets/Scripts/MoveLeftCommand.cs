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
        var platformGO = _platform.platformGO;
        var platformSize = platformGO.GetComponent<MeshRenderer>().bounds.size.x;
        this._platform.MovePlatform(new Vector3(platformGO.transform.position.x - platformSize, platformGO.transform.position.y, platformGO.transform.position.z));
    }
}
