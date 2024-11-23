using Data;
using Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace Weapons.WeaponSubScripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class LeavesBullet : MonoBehaviour
    {
        public UnityAction<MonsterBehavior> OnHit { get; set; }
        
        private GameObjectPool _pool;
        private float _damage;
        private Rigidbody _rigidBody;
        
        private bool _collided = false;
        
        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        private float _time = 0;
        private void Update()
        {
            _time += Time.deltaTime;
            if (_time >= 1)
            {
                CheckRelease();
            }
        }

        private void CheckRelease()
        {
            _pool.Release(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag($"Monster") && !_collided)
            {
                other.TryGetComponent(out MonsterBehavior monster);
                monster.TakeDamage(_damage);
                _collided = true;
                
                OnHit?.Invoke(monster);
                _pool.Release(gameObject);
            }
        }
        
        public void SetTarget(Vector3 target)
        {
            transform.LookAt(target);
        }

        public void SetArgs(float damage, float speed, GameObjectPool pool)
        {
            _damage = damage;
            _rigidBody.velocity = transform.forward * speed;
            _pool = pool;
        }
        
        private void OnEnable()
        {
            _time = 0;
            _collided = false;
        }
    }
}