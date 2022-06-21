using UnityEngine;

public class SeveredLimb : MonoBehaviour
{
    private Rigidbody _rigidBody;

    public void Init(GameObject go)
    {
        transform.SetParent(go.transform);

        _rigidBody = gameObject.TryGetComponent(out Rigidbody rigidbody)
            ? rigidbody
            : gameObject.AddComponent<Rigidbody>();
    }
} 
