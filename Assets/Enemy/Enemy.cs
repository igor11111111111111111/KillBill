using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    public SkinnedMeshRenderer SkinnedMeshRenderer;

    private void Awake()
    {
        var collider = GetComponent<MeshCollider>();
        var mesh = new Mesh();
        SkinnedMeshRenderer.BakeMesh(mesh, false);
        collider.sharedMesh = mesh;
    }
}
