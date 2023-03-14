using UnityEngine;

namespace Script.EnemySystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class Target : MonoBehaviour
    {
        public Enemy GetPicker()
        {
            return _picker;
        }

        private Enemy _picker;

        public void OnPickUp(Enemy enemy)
        {
            _picker = enemy;
        }

        public void OnDrop(Enemy enemy)
        {
            _picker = null;
        }
    }
}