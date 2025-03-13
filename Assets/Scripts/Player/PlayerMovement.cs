using System;
using Input;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMovement : ITickable, IInitializable, IDisposable
    {
        private readonly MultiTouchInput touchInput;
        private readonly Rigidbody rb;
        private readonly Transform playerTransform;
        private readonly Transform cameraTransform;
        private readonly PlayerConfig config;

        private float xRotation;
        private Quaternion targetRotation;

        public PlayerMovement(MultiTouchInput touchInput, Rigidbody rb, Transform playerTransform, Transform cameraTransform, PlayerConfig config)
        {
            this.touchInput = touchInput;
            this.rb = rb;
            this.playerTransform = playerTransform;
            this.cameraTransform = cameraTransform;
            this.config = config;
            targetRotation = rb.rotation;
        }

        public void Initialize()
        {
            touchInput.OnRotationUpdate += HandleRotationInput;
        }

        private void HandleRotationInput(Vector2 delta)
        {
            xRotation -= delta.y;
            xRotation = Mathf.Clamp(xRotation, -config.RotationClampY, config.RotationClampY);
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            targetRotation *= Quaternion.Euler(0, delta.x, 0);
        }

        public void Dispose()
        {
            touchInput.OnRotationUpdate -= HandleRotationInput;
        }

        public void Tick()
        {
            Vector2 input = touchInput.GetJoystickInput();

            if (input.magnitude > 0)
            {
                Vector3 move = new Vector3(input.x, 0, input.y);
                Vector3 globalMove = playerTransform.TransformDirection(move);
                rb.velocity = new Vector3(
                    globalMove.x * config.MoveSpeed,
                    rb.velocity.y,
                    globalMove.z * config.MoveSpeed
                );
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
            
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * config.RotationSmoothing));
        }
    }
}