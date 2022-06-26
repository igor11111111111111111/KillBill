using MeshSplitting.Splitables;
using UnityEngine;

namespace KillBill
{
    public class EnemyInit : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private Splitable _splitable;

        private void Awake()
        {
            _splitable.Init(_skinnedMeshRenderer, _animator);
        }
    }
}
