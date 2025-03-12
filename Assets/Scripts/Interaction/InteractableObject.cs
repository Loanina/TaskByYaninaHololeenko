using UnityEngine;

namespace Interaction
{
    [RequireComponent(typeof(Rigidbody))]
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private Rigidbody rb;

        private void Awake()
        {
            if (rb == null) rb = GetComponent<Rigidbody>();
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