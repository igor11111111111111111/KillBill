using UnityEngine;

namespace MeshSplitting.Demo
{
    [AddComponentMenu("Player/RigidMotor")]
    [RequireComponent(typeof(Rigidbody))]
    public class RigidMotor : MonoBehaviour
    {
        public float Speed = 10f;

        private Rigidbody _rigidbody;
        [SerializeField] private Transform _rigidMotorTarget;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.freezeRotation = true;
            _rigidbody.useGravity = false;
        }

        private void FixedUpdate()
        {
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

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
