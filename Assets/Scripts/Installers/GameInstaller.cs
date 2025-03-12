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
            Container.Bind<JoystickInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle().WithArguments(playerRb, playerConfig, playerTransform);
            Container.Bind<InteractionHandler>().AsSingle().WithArguments(playerCamera, interactionConfig);
        }
    }
}