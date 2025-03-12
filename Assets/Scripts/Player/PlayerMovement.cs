using Input;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMovement : ITickable
    {
        private readonly PlayerConfig config;
        private readonly JoystickInput joystickInput;
        private readonly Rigidbody rb;
        private readonly Transform transform;

        public PlayerMovement(Rigidbody rb, JoystickInput joystickInput, PlayerConfig config, Transform transform)
        {
            this.rb = rb;
            this.joystickInput = joystickInput;
            this.config = config;
            this.transform = transform;
        }

        public void Tick()
        {
            var move = new Vector3(joystickInput.Horizontal, 0, joystickInput.Vertical);
            rb.velocity = transform.TransformDirection(move) * config.MoveSpeed;
        }
    }
}