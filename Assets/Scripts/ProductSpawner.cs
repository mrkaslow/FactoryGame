using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProductSpawner : IInitializable {

    readonly Product.Factory _productFactory;
    readonly BoardLayout _board;

    public ProductSpawner(Product.Factory productFactory, BoardLayout board)
    {
        this._productFactory = productFactory;
        this._board = board;
    }

    public void Initialize()
    {
        SpawnProduct(_board.floor.transform.position);
    }

    public Product SpawnProduct(Vector3 startPosition)
    {
        Product product = _productFactory.Create();
        product.productGO.transform.position = startPosition;
        return product;
    }
}
