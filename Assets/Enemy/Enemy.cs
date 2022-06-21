using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SkeletonMeshSlicer Skeleton;
    [HideInInspector] public SkinnedMeshRenderer SkinnedMeshRenderer;
    [HideInInspector] public Animator Animator;

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        SkinnedMeshRenderer = Skeleton.GetComponent<SkinnedMeshRenderer>();
    }
}
