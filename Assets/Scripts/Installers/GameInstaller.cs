using Input;
using Interaction;
using Player;
using UnityEngine;
using Zenject;
using UI;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Rigidbody playerRb;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform heldObjectsParent;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private InputConfig inputConfig;
        [SerializeField] private InteractionConfig interactionConfig;
        [SerializeField] private MultiTouchInput multiTouchInput;
        [SerializeField] private UIController uiController;

        public override void InstallBindings()
        {
            Container.Bind<InputConfig>().FromInstance(inputConfig).AsSingle();
            Container.Bind<MultiTouchInput>().FromInstance(multiTouchInput).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle().WithArguments(playerRb, playerTransform, playerCamera.transform, playerConfig);
            Container.BindInterfacesAndSelfTo<InteractionHandler>().AsSingle().WithArguments(playerCamera, interactionConfig, heldObjectsParent);
            Container.Bind<UIController>().FromInstance(uiController).AsSingle();
        }
    }
}