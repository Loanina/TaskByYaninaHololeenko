using UnityEngine;

namespace Interaction
{
    public class InteractionHandler
    {
        private readonly InteractionConfig config;
        private readonly Camera playerCamera;
        private IInteractable heldObject;

        public InteractionHandler(Camera camera, InteractionConfig config)
        {
            playerCamera = camera;
            this.config = config;
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
                    heldObject.PickUp(playerCamera.transform);
                }
        }

        public void DropItem()
        {
            if (heldObject != null)
            {
                heldObject.Drop(playerCamera.transform.forward * config.DropForce);
                heldObject = null;
            }
        }
    }
}