using System;
using Script.EnemySystem;
using UnityEditor.Animations;
using UnityEngine;

namespace Script
{
    public class EnemyCharacter : Enemy
    {
        [SerializeField] private Animator controller;
        private static readonly int Walking = Animator.StringToHash("walking");

        private void Start()
        {
            onSeeTarget.AddListener(enemy => { controller.SetBool(Walking, true); });
            onPickUp.AddListener(enemy => { controller.SetBool(Walking, false); });
        }
    }
}