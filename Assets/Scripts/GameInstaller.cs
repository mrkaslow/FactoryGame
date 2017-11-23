using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public RandomProductFactory.Assets RandomProductFactoryAssets;
    [Inject] GameAssets gameAssets;

    public override void InstallBindings()
    {
        var platform = new Platform(Container, gameAssets);
        Container.BindInterfacesAndSelfTo<Platform>().FromInstance(platform);
        Container.BindInterfacesAndSelfTo<InputHandler>().FromInstance(new InputHandler(new MoveUpCommand(platform), new MoveDownCommand(platform), new MoveRightCommand(platform), new MoveLeftCommand(platform)));
        Container.BindInstance(RandomProductFactoryAssets);
        Container.BindInterfacesAndSelfTo<BoardLayout>().AsSingle().NonLazy();
        Container.BindInterfacesTo<ProductSpawner>().AsSingle().NonLazy();
        Container.BindFactory<Product, Product.Factory>().FromFactory<RandomProductFactory>();
    }
}