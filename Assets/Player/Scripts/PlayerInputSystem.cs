using System;
using UnityEngine;

namespace KillBill
{
    public class PlayerInputSystem : MonoBehaviour
    {
        public Action OnAttack;
        public Action<Vector3> OnMove;
        private Vector3 _oldDirection;

        private void Update()
        {
            Move();
            Attack();
        }

        private void Move()
        {
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            //if (moveDirection != _oldDirection)
            //{
                OnMove?.Invoke(moveDirection);
            //}

            _oldDirection = moveDirection;
        }

        private void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                OnAttack?.Invoke();
            }
        }
    }
}