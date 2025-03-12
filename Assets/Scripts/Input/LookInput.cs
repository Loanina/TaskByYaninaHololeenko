using UnityEngine;
using UnityEngine.EventSystems;

namespace Input
{
    public class LookInput : MonoBehaviour, IDragHandler
    {
        [SerializeField] private Transform playerCamera;
        [SerializeField] private float sensitivity = 0.1f;
        [SerializeField] private float maxRotationX = 90f;
        [SerializeField] private float minRotationX = -90f;

        private float rotationX = 0f;

        public void OnDrag(PointerEventData eventData)
        {
            float deltaX = eventData.delta.x * sensitivity;
            float deltaY = eventData.delta.y * sensitivity;

            rotationX = Mathf.Clamp(rotationX - deltaY, minRotationX, maxRotationX);
            playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
            playerCamera.parent.Rotate(Vector3.up * deltaX);
        }
    }
}