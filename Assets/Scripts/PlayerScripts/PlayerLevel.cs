using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerLevel : MonoBehaviour
{
    private Player _player;
    public UnityEvent<int> OnLevelUp { get; private set; }

    private int _curLevel;
    private float _curExp;
    
    private float _expToNextLevel;
    private float _expIncreaseFactor;

    private void Awake()
    {
        _player = GetComponent<Player>();
        OnLevelUp = new UnityEvent<int>();
    }
    
    private void EarnExp(int exp)
    {
        // todo : 성장이라는 강화요소를 사용한다면, 이곳에서 성장이 적용된 경험치를 계산한다.
        _curExp += exp;
        CheckLevelUp();
    }
    
    private void CheckLevelUp()
    {
        if (_curExp >= _expToNextLevel)
        {
            LevelUp();
            CheckLevelUp();
        }
    }

    private void LevelUp()
    {
        _curLevel++;
        _curExp -= _expToNextLevel;
        _expToNextLevel *= _expIncreaseFactor;
        OnLevelUp.Invoke(_curLevel);
    }
}