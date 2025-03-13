using System;
using UI;
using UnityEngine;
using Zenject;

namespace Interaction
{
    public class InteractionHandler : ITickable
    {
        private readonly InteractionConfig config;
        private readonly Camera playerCamera;
        private IInteractable heldObject;
        private readonly Transform parentTransform;
        private readonly UIController uiController;
        
        public event Action<bool> OnLookAtInteractable;
        public event Action OnObjectPickedUp;
        public event Action OnObjectDrop;

        public InteractionHandler(Camera camera, InteractionConfig config, Transform helpParentTransform)
        {
            playerCamera = camera;
            this.config = config;
            parentTransform = helpParentTransform;
        }

        public void Tick()
        {
            if (heldObject != null) return;

            RaycastHit hit;
            OnLookAtInteractable?.Invoke(Physics.Raycast(playerCamera.transform.position,
                playerCamera.transform.forward, out hit,
                config.PickupRange) && hit.transform.TryGetComponent(out IInteractable interactable));
        }

        public void TryPickup()
        {
            if (heldObject != null) return;

            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit,
                    config.PickupRange))
                if (hit.transform.TryGetComponent(out IInteractable interactable))
                {
                    heldObject = interactable;
                    heldObject.PickUp(parentTransform);
                    OnObjectPickedUp?.Invoke();
                }
        }

        public void DropItem()
        {
            if (heldObject == null) return;
            heldObject.Drop(playerCamera.transform.forward * config.DropForce);
            heldObject = null;
            OnObjectDrop?.Invoke();
        }
    }
}