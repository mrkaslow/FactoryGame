using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProductSpawner: IInitializable { 

    readonly Product.Factory _productFactory;
    readonly BoardLayout _board;

    public ProductSpawner(Product.Factory productFactory, BoardLayout board)
    {
        this._productFactory = productFactory;
        this._board = board;
    }

    public void Initialize()
    {
        for (int i = 0; i < _board.factoryLines.Count; i++)
        {
            GameObject.FindObjectOfType<MonoBehaviour>().StartCoroutine(ExecuteAfterTime(i));
        }
    }

    public void SpawnProduct(int lineNumber)
    {
        Product product = _productFactory.Create();
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

    private IEnumerator ExecuteAfterTime(int lineNumber)
    {
        float delay = UnityEngine.Random.Range(0, 5);
        yield return new WaitForSeconds(delay);
        SpawnProduct(lineNumber);
        GameObject.FindObjectOfType<MonoBehaviour>().StartCoroutine(ExecuteAfterTime(lineNumber));
    }
}
