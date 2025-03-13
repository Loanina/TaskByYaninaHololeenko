using UnityEngine;
using Zenject;

namespace Input
{
    public class MultiTouchInput : MonoBehaviour
    {
        [SerializeField] private RectTransform joystickRectTransform;
        [SerializeField] private RectTransform joystickHandle;

        private Vector2 _joystickInput;
        private int _joystickTouchId = -1;
        private int _rotationTouchId = -1;

        private InputConfig _inputConfig;

        [Inject]
        public void Construct(InputConfig inputConfig)
        {
            _inputConfig = inputConfig;
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

            OnJoystickUpdate?.Invoke(_joystickInput);
        }

        private void HandleTouchBegan(Touch touch)
        {
            bool isJoystickArea = RectTransformUtility.RectangleContainsScreenPoint(
                joystickRectTransform,
                touch.position
            );

            if (isJoystickArea && _joystickTouchId == -1)
            {
                _joystickTouchId = touch.fingerId;
            }
            else if (!isJoystickArea && _rotationTouchId == -1)
            {
                _rotationTouchId = touch.fingerId;
            }
        }

        private void HandleTouchMoved(Touch touch)
        {
            if (touch.fingerId == _joystickTouchId)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    joystickRectTransform,
                    touch.position,
                    null,
                    out Vector2 localPoint
                );

                Vector2 direction = localPoint - joystickRectTransform.rect.center;
                direction = Vector2.ClampMagnitude(direction, _inputConfig.JoystickRadius);
        
                _joystickInput = direction / _inputConfig.JoystickRadius;
                joystickHandle.anchoredPosition = direction;
            }
            else if (touch.fingerId == _rotationTouchId)
            {
                Vector2 delta = touch.deltaPosition * _inputConfig.RotationSensitivity;
                OnRotationUpdate?.Invoke(delta);
            }
        }

        private void HandleTouchEnded(Touch touch)
        {
            if (touch.fingerId == _joystickTouchId)
            {
                _joystickTouchId = -1;
                _joystickInput = Vector2.zero;
                joystickHandle.anchoredPosition = Vector2.zero;
            }
            else if (touch.fingerId == _rotationTouchId)
            {
                _rotationTouchId = -1;
            }
        }

        public Vector2 GetJoystickInput() => _joystickInput;
    }
}