using UnityEngine;

namespace KillBill
{
    public class PlayerPhysics
    {
        public float Speed = 10f;
        private Rigidbody _rigidbody;
        private Transform _rigidMotorTarget;

        public PlayerPhysics(Rigidbody rigidbody, Transform target, PlayerController playerController)
        {
            _rigidbody = rigidbody;
            _rigidMotorTarget = target;

            _rigidbody.freezeRotation = true;
            _rigidbody.useGravity = false;

            playerController.OnMove += Move;
        }

        private void Move(Vector3 moveDirection)
        {
            Vector3 targetVelocity = _rigidMotorTarget.TransformDirection(moveDirection) * Speed;
            Vector3 changeVelocity = targetVelocity - _rigidbody.velocity;
            changeVelocity.y = 0f;

            float deltaVelocity = changeVelocity.magnitude;
            if (deltaVelocity > Speed)
            {
                changeVelocity = changeVelocity / deltaVelocity * Speed;
            }

            _rigidbody.AddForce(changeVelocity, ForceMode.VelocityChange);
            _rigidbody.AddForce(Physics.gravity * _rigidbody.mass, ForceMode.Force);
        }
    }
}
