using Data;
using Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace Weapons.WeaponSubScripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class MeleeAttack : MonoBehaviour
    {
        public UnityAction<MonsterBehavior> OnHit { get; set; }

        public GameObject myVFX;
        private float _damage;
        private Rigidbody _rigidBody;
        
        private bool _collided = false;
        
        private void Awake()
        {
            //transform.GetChild(0).GetComponent<MeleeAttackSub>().meleeAttack = this;
            _rigidBody = GetComponent<Rigidbody>();
        }

        float a = 0;
        private float _time = 0;

        IEnumerator LiveCoroutine()
        {
            transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }

        public void Damage(MonsterBehavior monster)
        {
            monster.TakeDamage(_damage);
            _collided = true;
            OnHit?.Invoke(monster);
        }
        
        public void SetTarget(Vector3 target)
        {
            // target과 gameobjec의 y 동기화
            target.y = transform.position.y;
            
            // target 방향으로 회전
            transform.LookAt(target);
            GameObject spawnedVFX = Instantiate(myVFX, transform.position+new Vector3(0, 1, 0), transform.rotation*Quaternion.Euler(new Vector3(90, 0, -90))) as GameObject;
            spawnedVFX.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            Destroy(spawnedVFX, 1f);
            StartCoroutine(LiveCoroutine());
        }

        public void SetArgs(float damage)
        {
            _damage = damage;
        }
        
        private void OnEnable()
        {
            
            
            _time = 0;
            _collided = false;
        }
    }
}