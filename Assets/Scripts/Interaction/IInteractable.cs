using UnityEngine;

namespace Interaction
{
    public interface IInteractable
    {
        void PickUp(Transform parent);
        void Drop(Vector3 force);
    }
}