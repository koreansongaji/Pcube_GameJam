using Data;
using Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace Weapons.WeaponSubScripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class NormalBullet : MonoBehaviour
    {
        public UnityAction<MonsterBehavior> OnHit { get; set; }

        public GameObject myVFX;
        private GameObjectPool _pool;
        private float _damage;
        private Rigidbody _rigidBody;
        
        private bool _collided = false;
        
        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }
        float a = 0;
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
                if(other.TryGetComponent(out MonsterBehavior monster) == false)
                    return;
                monster.TakeDamage(_damage);
                _collided = true;
                
                OnHit?.Invoke(monster);
                _pool.Release(gameObject);
            }
        }
        
        public void SetTarget(Vector3 target)
        {
            // target과 gameobjec의 y 동기화
            target.y = transform.position.y;
            
            // target 방향으로 회전
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