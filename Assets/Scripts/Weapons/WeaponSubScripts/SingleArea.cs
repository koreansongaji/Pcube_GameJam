using System.Collections.Generic;
using System.Linq;
using Data;
using Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace Weapons.WeaponSubScripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class SingleArea : MonoBehaviour
    {
        public UnityAction<MonsterBehavior> OnHit { get; set; }
        
        private GameObjectPool _pool;
        private Rigidbody _rigidBody;
        private const float ATK_RATE = 1;
        private readonly List<MonsterBehavior> _monsters = new List<MonsterBehavior>();
        
        //=================== args ===================
        private float _damage;
        private float _range;
        private float _duration;
        
        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _rigidBody.isKinematic = true;
        }

        private float _time = 0;
        private float _nextAtk = 0;
        private void Update()
        {
            _time += Time.deltaTime;
            if (_time >= _duration)
            {
                CheckRelease();
            }
            
            if (_time >= _nextAtk)
            {
                _nextAtk = _time + ATK_RATE;
                DealMonsters();
            }
        }

        private void DealMonsters()
        {
            foreach (MonsterBehavior monster in _monsters.Where(monster => monster != null))
            {
                monster.TakeDamage(_damage);
                OnHit?.Invoke(monster);
                _pool.Release(gameObject);
            }
        }

        private void CheckRelease()
        {
            _pool.Release(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag($"Monster"))
            {
                other.TryGetComponent(out MonsterBehavior monster);
                _monsters.Add(monster);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag($"Monster"))
            {
                other.TryGetComponent(out MonsterBehavior monster);
                _monsters.Remove(monster);
            }
        }

        public void SetArgs(float damage, float range, float duration, GameObjectPool pool)
        {
            _damage = damage;
            _range = range;
            _duration = duration;
            _pool = pool;
            
            transform.localScale = new Vector3(_range, transform.localScale.y, _range);
        }
        
        private void OnEnable()
        {
            _time = 0;
            _nextAtk = 0;
            _monsters.Clear();
        }
    }
}