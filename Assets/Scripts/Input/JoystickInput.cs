using UnityEngine;

namespace Input
{
    public class JoystickInput
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        public void UpdateInput(Vector2 input)
        {
            Horizontal = input.x;
            Vertical = input.y;
        }
    }
}