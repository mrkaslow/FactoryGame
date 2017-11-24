using UnityEngine;
using Zenject;

public class RootInstaller : MonoInstaller<RootInstaller>
{
    [SerializeField]
    private UIHandler uiHandler;

    public override void InstallBindings()
    {
        uiHandler.playButton.onClick.AddListener(()=>
        {
            uiHandler.PlayGame();
            InstallGame();
        });
        uiHandler.restartButton.onClick.AddListener(() =>
        {
            uiHandler.RestartGame();
            InstallGame();
        });
        Container.BindInterfacesAndSelfTo<GameplayHandler>().FromSubContainerResolve().
            ByNewPrefabResource("GameplayContext").UnderTransform(this.transform).AsTransient();
    }

    private void InstallGame()
    {
        Container.Resolve<GameplayHandler>();
    } 
}