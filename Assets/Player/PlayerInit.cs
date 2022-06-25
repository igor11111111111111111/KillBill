using MeshSplitting;
using UnityEngine;

namespace KillBill
{
    public class PlayerInit : MonoBehaviour
    {
        [SerializeField] private PlayerAnimationsEvents _playerAnimationsEvents;
        [SerializeField] private SplitterSingleCut _splitterSingleCut;

        private void Awake()
        {
            _playerAnimationsEvents.Init(_splitterSingleCut);
        }
    }
}


