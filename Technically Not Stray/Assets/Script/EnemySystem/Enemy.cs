using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Script.EnemySystem
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(SphereCollider))]
    public class Enemy : MonoBehaviour
    {
        public UnityEvent<Enemy> onSeeTarget;
        public UnityEvent<Enemy> onPickUp;
        public UnityEvent<Enemy> onDrop;

        private NavMeshAgent _agent;

        [SerializeField] private Target target;

        [SerializeField] private float visionRange = 10f;

        private Collider _trigger;

        private EnemyState _state = EnemyState.Idle;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _trigger = GetComponent<SphereCollider>();
            _trigger.isTrigger = true;
        }

        private void Update()
        {
            switch (_state)
            {
                case EnemyState.Idle:
                    if (target != null)
                        _state = EnemyState.Lost;
                    break;
                case EnemyState.Lost:
                    TargetInRange();
                    break;
                case EnemyState.Walking:
                    TargetInRange();
                    _agent.SetDestination(target.transform.position);
                    break;
                case EnemyState.Cuddling:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TargetInRange()
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= visionRange)
            {
                onSeeTarget?.Invoke(this);
                _state = EnemyState.Walking;
            }
            else
            {
                _state = EnemyState.Lost;
            }
        }

        public bool SetTarget(Target t)
        {
            if (t.Equals(this.target)) return false;
            if (this.target == null) return false;
            this.target = t;
            _state = EnemyState.Lost;
            return true;
        }

        public void Drop()
        {
            onDrop?.Invoke(this);
            target.OnDrop(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            _state = EnemyState.Cuddling;

            onPickUp?.Invoke(this);
            target.OnPickUp(this);
        }

        private void OnTriggerExit(Collider other)
        {
            _state = EnemyState.Walking;
        }

        private void OnDrawGizmos()
        {
            Handles.Label(transform.position, _state.ToString());
        }
    }

    internal enum EnemyState
    {
        Idle,
        Lost,
        Walking,
        Cuddling,
    }
}