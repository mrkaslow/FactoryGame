using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameAssetsInstaller", menuName = "Installers/GameAssetsInstaller")]
public class GameAssetsInstaller : ScriptableObjectInstaller<GameAssetsInstaller>
{
    public GameAssets gameAssets;
    public override void InstallBindings()
    {
        Container.BindInstance(gameAssets);
    }
}