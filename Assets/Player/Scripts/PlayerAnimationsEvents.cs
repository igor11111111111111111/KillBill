using MeshSplitting;
using System;
using UnityEngine;

namespace KillBill
{
    public class PlayerAnimationsEvents : MonoBehaviour
    {
        private SplitterSingleCut _splitterSingleCut;

        public void Init(SplitterSingleCut splitterSingleCut)
        {
            _splitterSingleCut = splitterSingleCut;
        }

        public void ActivateWeaponCuttingEdge(int active)
        {
            _splitterSingleCut.gameObject.SetActive(Convert.ToBoolean(active));
        }
    }
}


