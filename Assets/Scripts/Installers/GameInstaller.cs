using Input;
using Interaction;
using Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Rigidbody playerRb;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private InteractionConfig interactionConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(playerConfig).AsSingle();
            Container.BindInstance(interactionConfig).AsSingle();
            Container.BindInstance(playerRb).AsSingle();
            Container.BindInstance(playerTransform).AsSingle();
            Container.BindInstance(playerCamera).AsSingle();

            Container.Bind<JoystickInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle();
            Container.Bind<InteractionHandler>().AsSingle();
        }
    }
}