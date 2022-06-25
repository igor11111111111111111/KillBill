using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SliceTester : MonoBehaviour
{
    private Enemy _enemy;

    //private void Start()
    //{
    //    Test();
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Test();
        }
    }

    private /*async*/ void Test()
    {
        //await Task.Delay(1000);
        //_enemy = Enemy.Instance;
        TrySlice();
        gameObject.SetActive(false);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.layer == LayerMask.NameToLayer("CropsPart"))
    //    {
    //        _enemy = Enemy.Instance;
    //        TrySlice();
    //        gameObject.SetActive(false);
    //    }
    //}

    private void TrySlice()
    {
        //_enemy.Skeleton.SliceByMeshPlane(transform.forward, transform.position, ActiveRagdollWhenNeeded);
        ////_enemy.Animator.SetTrigger("OnDeath");
        //_enemy.Animator.enabled = false;
    }

    //private void ActiveRagdollWhenNeeded()
    //{
    //    var brokenBones = new List<Transform>();
    //    var separatedBones = new List<Transform>();
    //    foreach (var sliceTarget in _enemy.Skeleton.sliceTargets)
    //    {
    //        brokenBones.AddRange(sliceTarget.sectionsBones);
    //        separatedBones.AddRange(sliceTarget.allRootBones);
    //    }
    //}
}
