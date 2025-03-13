using UnityEngine;
using Zenject;

namespace Input
{
    public class MultiTouchInput : MonoBehaviour
    {
        [SerializeField] private RectTransform joystickRectTransform;
        [SerializeField] private RectTransform joystickHandle;

        private Vector2 joystickInput;
        private int joystickTouchId = -1;
        private int rotationTouchId = -1;

        private InputConfig inputConfig;

        [Inject]
        public void Construct(InputConfig inputConfig)
        {
            this.inputConfig = inputConfig;
        }

        public System.Action<Vector2> OnJoystickUpdate;
        public System.Action<Vector2> OnRotationUpdate;

        private void Update()
        {
            foreach (var touch in UnityEngine.Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        HandleTouchBegan(touch);
                        break;
                    case TouchPhase.Moved:
                        HandleTouchMoved(touch);
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        HandleTouchEnded(touch);
                        break;
                }
            }

            OnJoystickUpdate?.Invoke(joystickInput);
        }

        private void HandleTouchBegan(Touch touch)
        {
            var isJoystickArea = RectTransformUtility.RectangleContainsScreenPoint(
                joystickRectTransform,
                touch.position
            );

            if (isJoystickArea && joystickTouchId == -1)
            {
                joystickTouchId = touch.fingerId;
            }
            else if (!isJoystickArea && rotationTouchId == -1)
            {
                rotationTouchId = touch.fingerId;
            }
        }

        private void HandleTouchMoved(Touch touch)
        {
            if (touch.fingerId == joystickTouchId)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    joystickRectTransform,
                    touch.position,
                    null,
                    out Vector2 localPoint
                );

                var direction = localPoint - joystickRectTransform.rect.center;
                direction = Vector2.ClampMagnitude(direction, inputConfig.JoystickRadius);
        
                joystickInput = direction / inputConfig.JoystickRadius;
                joystickHandle.anchoredPosition = direction;
            }
            else if (touch.fingerId == rotationTouchId)
            {
                var delta = touch.deltaPosition * inputConfig.RotationSensitivity;
                OnRotationUpdate?.Invoke(delta);
            }
        }

        private void HandleTouchEnded(Touch touch)
        {
            if (touch.fingerId == joystickTouchId)
            {
                joystickTouchId = -1;
                joystickInput = Vector2.zero;
                joystickHandle.anchoredPosition = Vector2.zero;
            }
            else if (touch.fingerId == rotationTouchId)
            {
                rotationTouchId = -1;
            }
        }

        public Vector2 GetJoystickInput() => joystickInput;
    }
}