using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockPlatform {
    internal int _productID;
    internal GameObject _stockGO;
    internal int productsAmount = 0;
    private int row = 0;
    private int column = 0;
    private int height = 1;

    public StockPlatform(int productID, GameObject stockGO)
    {
        _productID = productID;
        _stockGO = stockGO;
    }

    internal Vector3 SetProductsPosition()
    {
        Vector3 position = new Vector3();
        float productSize = 0.75f;
        var stockMesh = _stockGO.GetComponent<MeshRenderer>().bounds;
        position = new Vector3(stockMesh.min.x + 0.7f + productSize * row, 0.7f * height, stockMesh.min.z + 0.7f + productSize * column);
        row++;
        if (row == 3)
        {
            row = 0;
            column++;
            if(column == 3)
            {
                column = 0;
                height++;
            }
        }
        return position;
    }

    internal void ResetPlatform()
    {
        productsAmount = 0;
        row = 0;
        column = 0;
        height = 1;
        foreach (Transform product in _stockGO.transform)
        {
            Object.Destroy(product.gameObject);
        }
    }
}
