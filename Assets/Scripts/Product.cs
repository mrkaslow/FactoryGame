using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Product{
    public GameObject productGO;
    private IMoveBehavior moveBehavior;

    public Product(GameObject ProductGO, IMoveBehavior MoveBehavior)
    {
        this.productGO = ProductGO;
        this.moveBehavior = MoveBehavior;
    }

    public void Move()
    {
        this.moveBehavior.MoveOnLine(this);
    }

    public class Factory: Factory<Product>
    {

    }
}
