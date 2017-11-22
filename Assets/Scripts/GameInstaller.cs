using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public RandomProductFactory.Assets RandomProductFactoryAssets;

    public override void InstallBindings()
    {
        Container.BindInstance(RandomProductFactoryAssets);
        Container.BindInterfacesAndSelfTo<BoardLayout>().AsSingle().NonLazy();
        Container.BindInterfacesTo<ProductSpawner>().AsSingle().NonLazy();
        Container.BindFactory<Product, Product.Factory>().FromFactory<RandomProductFactory>();
    }
}