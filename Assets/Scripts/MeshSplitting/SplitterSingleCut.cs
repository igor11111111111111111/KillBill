using MeshSplitting.Splitables;
using UnityEngine;

namespace MeshSplitting
{
    public class SplitterSingleCut : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Splitable splitable))
            {
                Debug.Log("da");
                splitable.Split(transform);
            }
        }
    }
}