using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : IMoveBehavior
{
    public void MoveOnLine(Product product)
    {
        var productTrans = product.productGO.transform;
        productTrans.position = new Vector3(productTrans.position.x + 10 * Time.deltaTime, productTrans.position.y, productTrans.position.z);
        Debug.Log("hejooo");
    }
}
