using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoardLayout : IInitializable
{
    private GameAssets _gameAssets;
    private DiContainer _container;
    internal GameObject floor;
    internal List<GameObject> factoryLines;
    internal GameObject playerPlatform;
    internal GameObject stockPlatform;

    public BoardLayout(GameAssets gameAssets, DiContainer container)
    {
        this._gameAssets = gameAssets;
        this._container = container;
    }

    public void Initialize()
    {
        factoryLines = new List<GameObject>();
        SetInitialLayout();
    }

    private void SetInitialLayout()
    {
        floor = _container.InstantiatePrefab(_gameAssets.floorPrefab) as GameObject;
        var floorBounds = floor.GetComponent<MeshRenderer>().bounds;
        var factoryLineBounds = _gameAssets.productionLinePrefab.GetComponent<MeshRenderer>().bounds;

        for (int i = 0; i < 3; i++)
        {
            factoryLines.Add(_container.InstantiatePrefab(_gameAssets.productionLinePrefab) as GameObject);
            factoryLines[i].transform.position = new Vector3(floor.transform.position.x, factoryLineBounds.size.y / 2, floorBounds.min.z + (floorBounds.size.z / 4 * (i + 1)));
        }
    }
}
