using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownCommand : ICommand
{
    private Platform _platform;

    public MoveDownCommand(Platform platform)
    {
        this._platform = platform;
    }

    public void Execute()
    {
        var platformGO = _platform.platformGO;
        var platformSize = platformGO.GetComponent<MeshRenderer>().bounds.size.z;
        this._platform.MovePlatform(new Vector3(platformGO.transform.position.x, platformGO.transform.position.y, platformGO.transform.position.z - platformSize));
    }
}
