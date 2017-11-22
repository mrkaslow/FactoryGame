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

    public RandomProductFactory(Assets Assets, DiContainer Container, TickableManager tickableManager, InitializableManager initializableManager, BoardLayout board)
    {
        this._assets = Assets;
        this._container = Container;
        this._tickableManager = tickableManager;
        this._initializableManager = initializableManager;
        this._board = board;
    }

    public Product Create()
    {
        Product product = new Product(GetPrefab(), new SimpleMovement(), _board);
        product.Initialize();
        _tickableManager.Add(product);
        return product;
    }

    GameObject GetPrefab()
    {
        return _container.InstantiatePrefab(_assets.prefabs[UnityEngine.Random.Range(0, _assets.prefabs.Count)]);
    }

    [Serializable]
    public class Assets
    {
        public List<GameObject> prefabs;
    }
}
