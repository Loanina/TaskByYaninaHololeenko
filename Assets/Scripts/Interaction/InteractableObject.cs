using UnityEngine;

namespace Interaction
{
    [RequireComponent(typeof(Rigidbody))]
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField, Range(-10f, 30f)] private float yOffsetFromCamera = 0;
        private void Awake()
        {
            if (rb == null) rb = GetComponent<Rigidbody>();
        }

        public void PickUp(Transform parent)
        {
            rb.isKinematic = true;
            transform.SetParent(parent);
            transform.localPosition = new Vector3(0, yOffsetFromCamera, 0);
        }

        public void Drop(Vector3 force)
        {
            transform.SetParent(null);
            rb.isKinematic = false;
            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}