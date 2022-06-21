using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SliceTester : MonoBehaviour
{
    [SerializeField] private Enemy _player;

    private void OnEnable()
    {
        TrySlice();
    }

    private void TrySlice()
    {
        _player.Skeleton.SliceByMeshPlane(transform.forward, transform.position, ActiveRagdollWhenNeeded);
        _player.Animator.SetTrigger("OnDeath");
        //_player.Animator.enabled = false;
    }

    private void ActiveRagdollWhenNeeded()
    {
        var brokenBones = new List<Transform>();
        var separatedBones = new List<Transform>();
        foreach (var sliceTarget in _player.Skeleton.sliceTargets)
        {
            brokenBones.AddRange(sliceTarget.sectionsBones);
            separatedBones.AddRange(sliceTarget.allRootBones);
        }
    }
}
