using MeshSplitting.Splitables;
using System.Collections.Generic;
using UnityEngine;

namespace MeshSplitting
{
    public class SplitterSingleCut : MonoBehaviour
    {
        private List<Splitable> _splitables = new List<Splitable>();

        private void OnEnable()
        {
            _splitables.Clear();
        }

        private void OnDisable()
        {
            foreach (var splitable in _splitables)
            {
                if(splitable)
                    splitable.Split(transform);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent(out Splitable splitable);
            if (splitable && !_splitables.Contains(splitable))
                _splitables.Add(splitable);
        }
    }
}