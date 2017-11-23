using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Platform: IInitializable, ITickable {

    private enum PlatformState
    {
        moving,
        idle,
    }

    internal GameObject platformGO;
    readonly DiContainer _container;
    private GameAssets _gameAssets;
    private float movementProgress = 0.0f;
    private Vector3 currentPosition;
    private Vector3 targetPosition;
    private PlatformState currentState;

    public Platform(DiContainer container, GameAssets gameAssets)
    {
        this._container = container;
        this._gameAssets = gameAssets;
    }

    public void Initialize()
    {
        platformGO = _container.InstantiatePrefab(_gameAssets.playerPrefab);
        platformGO.transform.position = CalculatePlatformPosition();
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
                return;
            default:
                break;
        }
    }

    public void MovePlatform(Vector3 targetPosition)
    {
        if (currentState == PlatformState.moving)
            return;
        currentState = PlatformState.moving;
        this.targetPosition = targetPosition;
    }

    public void ProcessMovement()
    {
        movementProgress += Time.deltaTime / 1.0f;
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

    private Vector3 CalculatePlatformPosition()
    {
        var factoryLine = _gameAssets.productionLinePrefab.GetComponent<MeshRenderer>().bounds.max.x;
        return new Vector3(factoryLine, platformGO.GetComponent<MeshRenderer>().bounds.size.y, 0);
    }
}
