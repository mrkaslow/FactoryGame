using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProductSpawner: IInitializable { 

    readonly Product.Factory _productFactory;
    readonly BoardLayout _board;
    private int producingLine;

    public ProductSpawner(Product.Factory productFactory, BoardLayout board)
    {
        this._productFactory = productFactory;
        this._board = board;
    }

    public void Initialize()
    {
        producingLine = 4;
        GameObject.FindObjectOfType<MonoBehaviour>().StartCoroutine(ExecuteAfterTime());
    }

    public void SpawnProduct(int lineNumber)
    {
        Product product = _productFactory.Create();
        product.lineNumber = lineNumber;
        product.productGO.transform.position = GetStartPosition(product, lineNumber);
    }

    private Vector3 GetStartPosition(Product product, int lineNumber)
    {
        var productionLine = _board.factoryLines[lineNumber];
        var productionBox = productionLine.transform.GetChild(0);
        var startPosition = new Vector3(productionBox.transform.position.x,
            productionLine.GetComponent<MeshRenderer>().bounds.max.y + product.productGO.GetComponent<MeshRenderer>().bounds.size.y / 2,
            productionBox.transform.position.z);
        return startPosition;
    }

    private IEnumerator ExecuteAfterTime()
    {
        producingLine = UnityEngine.Random.Range(0,3);
        float delay = UnityEngine.Random.Range(0.5f, 5);
        yield return new WaitForSeconds(delay);
        SpawnProduct(producingLine);
        GameObject.FindObjectOfType<MonoBehaviour>().StartCoroutine(ExecuteAfterTime());
    }
}
