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
    internal List<StockPlatform> stockPlatforms;
    internal GameObject playerPlatform;
    private List<int> usedValues;

    public BoardLayout(GameAssets gameAssets, DiContainer container)
    {
        this._gameAssets = gameAssets;
        this._container = container;
    }

    public void Initialize()
    {
        factoryLines = new List<GameObject>();
        stockPlatforms = new List<StockPlatform>();
        usedValues = new List<int>();
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
            var productID = UniqueRandomInt(0, 3);
            usedValues.Add(productID);
            stockPlatforms.Add(new StockPlatform(productID, _container.InstantiatePrefab(_gameAssets.stockPrefab) as GameObject));
            factoryLines[i].transform.position = new Vector3(floor.transform.position.x, factoryLineBounds.size.y / 2, floorBounds.min.z + (floorBounds.size.z / 4 * (i + 1)));
            stockPlatforms[i]._stockGO.transform.position = new Vector3(factoryLineBounds.max.x + _gameAssets.playerPrefab.GetComponent<MeshRenderer>().bounds.size.x*2.75f,
                0.1f, factoryLines[i].transform.position.z);
            stockPlatforms[i]._stockGO.GetComponent<MeshRenderer>().material = GetMaterial(productID);
        }
    }

    public int UniqueRandomInt(int min, int max)
    {
        int val = Random.Range(min, max);
        while (usedValues.Contains(val))
        {
            val = Random.Range(min, max);
        }
        return val;
    }

    private Material GetMaterial(int productID)
    {
        var productMaterial = new Material(Shader.Find("Standard"));
        Color newColor = Color.white;
        switch (productID)
        {
            case 0:
                newColor = Color.green;
                break;
            case 1:
                newColor = Color.red;
                break;
            case 2:
                newColor = Color.blue;
                break;
            default:
                break;
        }
        productMaterial.SetColor("_Color", newColor);
        return productMaterial;
    }
}
