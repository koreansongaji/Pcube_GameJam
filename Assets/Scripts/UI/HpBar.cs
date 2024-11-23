using UnityEngine;

namespace UI
{
    public class HpBar : MonoBehaviour
    {
        private Player _player;
        private PlayerData _playerData;
        private void Awake()
        {
            _player = FindObjectOfType<Player>();
            _playerData = _player.GetStat();
        }

        private void Update()
        {
            transform.localScale = new Vector3(_playerData.currentHp.baseValue / _playerData.maxHp.baseValue, 1, 1);
        }
    }
}