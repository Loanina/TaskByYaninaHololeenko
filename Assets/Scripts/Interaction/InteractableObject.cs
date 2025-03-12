using UnityEngine;

namespace Interaction
{
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void PickUp(Transform parent)
        {
            rb.isKinematic = true;
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
        }

        public void Drop(Vector3 force)
        {
            transform.SetParent(null);
            rb.isKinematic = false;
            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}