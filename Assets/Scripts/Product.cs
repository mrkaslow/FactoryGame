using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Product: IInitializable, ITickable{
    public GameObject productGO;
    private IMoveBehavior moveBehavior;
    readonly BoardLayout _board;
    private MeshRenderer lineRenderer;

    public Product(GameObject ProductGO, IMoveBehavior MoveBehavior, BoardLayout board)
    {
        this.productGO = ProductGO;
        this.moveBehavior = MoveBehavior;
        this._board = board;
    }

    public void Initialize()
    {
        lineRenderer = _board.factoryLines[0].GetComponent<MeshRenderer>();
    }

    public void Move()
    {
        this.moveBehavior.MoveOnLine(this, 2.0f);
    }

    public void Tick()
    {
        if (productGO.transform.position.x > lineRenderer.bounds.max.x)
        {
            return;
        }
        Move();
    }

    public class Factory: Factory<Product>
    {

    }
}
