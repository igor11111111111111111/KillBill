using UnityEngine;

namespace KillBill
{
    public class PlayerAnimator
    {
        private Animator _animator;

        public PlayerAnimator(Animator animator, PlayerController playerController)
        {
            _animator = animator;

            playerController.OnAttack += Attack;
            playerController.OnMove += Move;
        }

        private void Move(Vector3 vector)
        {
            if(vector == Vector3.zero)
            {
                _animator.SetBool("IsMove", false);
            }
            else
            {
                _animator.SetBool("IsMove", true);
            }
        }

        private void Attack()
        {
            _animator.SetTrigger("OnAttack");
        }
    }
}


