using UnityEngine;
using Zenject;

public class RootInstaller : MonoInstaller<RootInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameplayHandler>().FromSubContainerResolve().
            ByNewPrefabResource("GameplayContext").UnderTransform(this.transform).AsTransient().NonLazy();
    }
}