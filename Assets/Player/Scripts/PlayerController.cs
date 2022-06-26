using System;
using UnityEngine;

namespace KillBill
{
    public class PlayerController
    {
        public Action OnAttack;
        public Action<Vector3> OnMove;

        public PlayerController(PlayerInputSystem playerInputSystem)
        {
            playerInputSystem.OnAttack += () => OnAttack?.Invoke();
            playerInputSystem.OnMove += (v) => OnMove?.Invoke(v);
        }
    }
}


