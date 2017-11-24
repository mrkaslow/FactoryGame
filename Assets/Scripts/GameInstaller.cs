using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public RandomProductFactory.Assets RandomProductFactoryAssets;
    [Inject] GameAssets gameAssets;
    private UIHandler uiHandler;

    public override void InstallBindings()
    {
        uiHandler = FindObjectOfType<UIHandler>();
        var scoreModel = new ScoreModel(uiHandler, this);
        Container.BindInterfacesAndSelfTo<ScoreModel>().FromInstance(scoreModel);
        var platform = new Platform(Container, gameAssets, scoreModel);
        Container.BindInterfacesAndSelfTo<Platform>().FromInstance(platform);
        Container.BindInterfacesAndSelfTo<InputHandler>().FromInstance(new InputHandler(new MoveUpCommand(platform), new MoveDownCommand(platform), new MoveRightCommand(platform), new MoveLeftCommand(platform)));
        Container.BindInstance(RandomProductFactoryAssets);

        Container.BindInterfacesAndSelfTo<BoardLayout>().AsSingle().NonLazy();
        Container.BindInterfacesTo<ProductSpawner>().AsSingle().NonLazy();
        Container.BindFactory<Product, Product.Factory>().FromFactory<RandomProductFactory>();
    }

    internal void CloseGame()
    {
        Destroy(this.gameObject);
    }
}