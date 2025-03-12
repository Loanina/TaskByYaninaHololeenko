using UnityEngine;
using UnityEngine.EventSystems;

namespace Input
{
    public class JoystickInput : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform joystickBackground;
        [SerializeField] private RectTransform joystickHandle;
        [SerializeField] private float joystickRadius = 100f;

        private Vector2 inputVector = Vector2.zero;

        public Vector2 InputVector => inputVector;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 direction = eventData.position - (Vector2)joystickBackground.position;
            inputVector = Vector2.ClampMagnitude(direction / joystickRadius, 1f);
            joystickHandle.anchoredPosition = inputVector * joystickRadius;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            inputVector = Vector2.zero;
            joystickHandle.anchoredPosition = Vector2.zero;
        }
    }
}