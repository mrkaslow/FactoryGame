using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RandomProductFactory : IFactory<Product>
{
    readonly DiContainer _container;
    private Assets _assets;
    private TickableManager _tickableManager;
    private InitializableManager _initializableManager;
    readonly BoardLayout _board;
    private Platform _platform;

    public RandomProductFactory(Assets Assets, DiContainer Container, TickableManager tickableManager, InitializableManager initializableManager, BoardLayout board, Platform platform)
    {
        this._assets = Assets;
        this._container = Container;
        this._tickableManager = tickableManager;
        this._initializableManager = initializableManager;
        this._board = board;
        this._platform = platform;
    }

    public Product Create()
    {
        var productID = UnityEngine.Random.Range(0, 3);
        Product product = new Product(GetPrefab(productID), new SimpleMovement(), _board, _platform, productID);
        product.Initialize();
        _tickableManager.Add(product);
        return product;
    }

    GameObject GetPrefab(int productID)
    {
        var prefab = _container.InstantiatePrefab(_assets.prefabs[UnityEngine.Random.Range(0, _assets.prefabs.Count)]);
        prefab.GetComponent<MeshRenderer>().material = GetMaterial(productID);
        return prefab;
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

    [Serializable]
    public class Assets
    {
        public List<GameObject> prefabs;
    }
}
