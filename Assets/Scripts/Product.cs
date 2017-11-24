using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Product: IInitializable, ITickable{
    public GameObject productGO;
    internal int lineNumber;
    private IMoveBehavior moveBehavior;
    internal BoardLayout _board;
    private Platform _platform;
    private MeshRenderer lineRenderer;
    private int _productID;

    private ProductState currentState;

    private enum ProductState
    {
        moving,
        idle,
    }

    public Product(GameObject ProductGO, IMoveBehavior MoveBehavior, BoardLayout board, Platform platform, int productID)
    {
        this.productGO = ProductGO;
        this.moveBehavior = MoveBehavior;
        this._board = board;
        this._platform = platform;
        this._productID = productID;
    }

    public void Initialize()
    {
        currentState = ProductState.moving;
        lineRenderer = _board.factoryLines[0].GetComponent<MeshRenderer>();
    }

    public void Move()
    {
        this.moveBehavior.MoveOnLine(this, 2 + _platform._score.SpeedModificator());
    }

    public void Tick()
    {
        switch (currentState)
        {
            case ProductState.moving:
                if (productGO.transform.position.x > lineRenderer.bounds.max.x + 0.1f)
                {
                    if (_platform.GetCurrentY() == 0 && _platform.GetCurrentX() == lineNumber)
                    {
                        productGO.transform.SetParent(_platform.platformGO.transform);
                        productGO.transform.position = new Vector3(_platform.platformGO.transform.position.x,
                            productGO.GetComponent<MeshRenderer>().bounds.size.y * _platform.GetStack().Count+0.5f,
                            _platform.platformGO.transform.position.z);
                        _platform.AddToStack(this);
                    }
                    else
                    {
                        _platform._score.Failures++;
                        GameObject.FindObjectOfType<MonoBehaviour>().StartCoroutine(ExecuteAfterTime());
                    }
                    currentState = ProductState.idle;
                    return;
                }
                Move();
                break;
            case ProductState.idle:
                return;
            default:
                break;
        }
    }

    public int GetProductID()
    {
        return this._productID;
    }

    private IEnumerator ExecuteAfterTime()
    {
        productGO.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        productGO.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        Object.Destroy(productGO);
    }

    public class Factory: Factory<Product>
    {

    }
}
