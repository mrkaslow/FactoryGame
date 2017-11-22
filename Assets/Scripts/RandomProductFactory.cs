using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RandomProductFactory : IFactory<Product>
{
    readonly DiContainer _container;
    private Assets _assets;

    public RandomProductFactory(Assets Assets, DiContainer Container)
    {
        this._assets = Assets;
        this._container = Container;
    }

    public Product Create()
    {
        return new Product(GetPrefab(), new SimpleMovement());
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
