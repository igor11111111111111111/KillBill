using MeshSplitting;
using UnityEngine;

namespace KillBill
{
    public class PlayerInit : MonoBehaviour
    {
        [SerializeField] private PlayerAnimationsEvents _playerAnimationsEvents;
        [SerializeField] private SplitterSingleCut _splitterSingleCut;
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerInputSystem _playerInputSystem;
        [SerializeField] private Transform _rigidMotorTarget;

        private void Awake()
        {
            _playerAnimationsEvents.Init(_splitterSingleCut);

            Rigidbody rigidbody = GetComponent<Rigidbody>();

            PlayerController playerController = new PlayerController(_playerInputSystem);
            PlayerAnimator playerAnimator = new PlayerAnimator(_animator, playerController);
            PlayerPhysics playerPhysics = new PlayerPhysics(rigidbody, _rigidMotorTarget, playerController);
        }
    }
}


