using MeshSplitting.Splitables;
using UnityEngine;

namespace MeshSplitting
{
    public class SplitterSingleCut : MonoBehaviour
    {
        private bool _canSlice = false;

        private void OnEnable()
        {
            _canSlice = true;
        }

        private void OnDisable()
        {
            _canSlice = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_canSlice && other.TryGetComponent(out Splitable splitable))
            {
                _canSlice = false;
                splitable.Split(transform);
            }
        }
    }
}