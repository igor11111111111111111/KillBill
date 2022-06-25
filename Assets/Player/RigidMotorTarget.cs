using UnityEngine;

namespace MeshSplitting.Demo
{
    public class RigidMotorTarget : MonoBehaviour
    {
        private Transform _parent;

        private void Awake()
        {
            _parent = transform.parent;
        }

        private void Update()
        {
            transform.localRotation = Quaternion.Euler(-_parent.localEulerAngles);
        }
    }
}
