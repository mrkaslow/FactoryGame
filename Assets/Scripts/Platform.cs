using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class Platform: IInitializable, ITickable, IStackable {

    private Stack<Product> productStack;

    readonly DiContainer _container;
    private GameAssets _gameAssets;
    internal ScoreModel _score;
    internal GameObject platformGO;

    private Vector3 currentPosition;
    private Vector3 targetPosition;
    private Vector3[,] platformPositions;

    private float movementProgress = 0.0f;
    private int currentX = 1;
    private int currentY = 0;

    private PlatformState currentState;

    private enum PlatformState
    {
        moving,
        idle,
    }

    public Platform(DiContainer container, GameAssets gameAssets, ScoreModel score)
    {
        this._container = container;
        this._gameAssets = gameAssets;
        this._score = score;
    }

    public void Initialize()
    {
        platformPositions = new Vector3[3, 2];
        productStack = new Stack<Product>();
        platformGO = _container.InstantiatePrefab(_gameAssets.playerPrefab);
        CalculatePlatformPositions();
        platformGO.transform.position = platformPositions[currentX, currentY];
        currentPosition = platformGO.transform.position;
        currentState = PlatformState.idle;
    }

    public void Tick()
    {
        switch (currentState)
        {
            case PlatformState.moving:
                ProcessMovement();
                break;
            case PlatformState.idle:
                if (currentY == 0)
                    return;
                CheckStack();
                return;
            default:
                break;
        }
    }

    public void MovePlatform(int x, int y)
    {
        if (currentState == PlatformState.moving)
        {
            currentPosition = platformGO.transform.position;
            movementProgress = 0.0f;
        }
        currentState = PlatformState.moving;
        if (!Enumerable.Range(0, 3).Contains(currentX+x) || !Enumerable.Range(0, 2).Contains(currentY + y))
            return;
        currentX += x;
        currentY += y;
        this.targetPosition = platformPositions[currentX, currentY];
    }

    public void ProcessMovement()
    {
        movementProgress += Time.deltaTime / 0.5f;
        if (movementProgress < 1.0f)
        {
            platformGO.transform.position = Vector3.Lerp(currentPosition, targetPosition, movementProgress);
        }
        else
        {
            movementProgress = 0.0f;
            currentPosition = targetPosition;
            currentState = PlatformState.idle;
            return;
        }
    }

    private void CalculatePlatformPositions()
    {
        var factoryLineSize = _gameAssets.productionLinePrefab.GetComponent<MeshRenderer>();
        var platformSize = platformGO.GetComponent<MeshRenderer>();
        var floorSize = _gameAssets.floorPrefab.GetComponent<MeshRenderer>();
        for (int i = 0; i < platformPositions.GetLength(0); i++)
        {
            var position_0 = new Vector3(factoryLineSize.bounds.max.x + platformSize.bounds.size.x / 2,
                platformGO.GetComponent<MeshRenderer>().bounds.size.y/2,
                floorSize.bounds.min.z + (floorSize.bounds.size.z / 4 * (i + 1)));
            var position_1 = new Vector3(factoryLineSize.bounds.max.x + platformSize.bounds.size.x * 1.5f,
                platformGO.GetComponent<MeshRenderer>().bounds.size.y/2,
                floorSize.bounds.min.z + (floorSize.bounds.size.z / 4 * (i + 1)));
            platformPositions[i, 0] = position_0;
            platformPositions[i, 1] = position_1;
        }
    }

    private void CheckStack()
    {
        if (productStack.Count <= 0)
            return;
        var productOnTop = productStack.Peek();
        if(productOnTop.GetProductID() == productOnTop._board.stockPlatforms[currentX]._productID)
        {
            productOnTop._board.stockPlatforms[currentX].productsAmount++;

            productOnTop.productGO.transform.SetParent(productOnTop._board.stockPlatforms[currentX]._stockGO.transform);
            productOnTop.productGO.transform.position = productOnTop._board.stockPlatforms[currentX].SetProductsPosition();
            if (productOnTop._board.stockPlatforms[currentX].productsAmount >= 18)
            {
                _score.Score += productOnTop._board.stockPlatforms[currentX].productsAmount;
                productOnTop._board.stockPlatforms[currentX].ResetPlatform();
            }
            RemoveFromStack();
        }
    }

    internal int GetCurrentX()
    {
        return this.currentX;
    }

    internal int GetCurrentY()
    {
        return this.currentY;
    }

    public Stack<Product> GetStack()
    {
        return productStack;
    }

    public void AddToStack(Product product)
    {
        productStack.Push(product);
    }

    public void RemoveFromStack()
    {
        productStack.Pop();
    }
}
