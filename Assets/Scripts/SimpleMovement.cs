using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : IMoveBehavior
{
    public void MoveOnLine(Product product, float speed)
    {
        var productTrans = product.productGO.transform;
        productTrans.position = new Vector3(productTrans.position.x + speed * Time.deltaTime, productTrans.position.y, productTrans.position.z);
    }
}
