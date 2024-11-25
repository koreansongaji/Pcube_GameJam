using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(Player))]
public class PlayerLevel : MonoBehaviour
{
    private Player _player;
    public UnityEvent<int> OnLevelUp { get; private set; }

    [SerializeField] private int curLevel;
    [SerializeField] private float curExp;
    
    [SerializeField] private float expToNextLevel;
    [SerializeField] private float expIncreaseFactor;

    private void Awake()
    {
        _player = GetComponent<Player>();
        OnLevelUp = new UnityEvent<int>();
    }
    
    public void EarnExp(int exp)
    {
        curExp += exp * (100 + _player.GetStat().growth.Value) * 0.01f;
        CheckLevelUp();
    }
    
    private void CheckLevelUp()
    {
        if (curExp >= expToNextLevel)
        {
            LevelUp();
            CheckLevelUp();
        }
    }

    private void LevelUp()
    {
        int levelUpCnt = 0;
        while (curExp >= expToNextLevel)
        {
            curLevel++;
            curExp -= expToNextLevel;
            expToNextLevel *= expIncreaseFactor;
            levelUpCnt++;
        }
        
        OnLevelUp.Invoke(levelUpCnt);
    }
    
    public int GetLevel()
    {
        return curLevel;
    }
    
    public float GetExp()
    {
        return curExp;
    }

    public float GetExpToNextLevel()
    {
        return expToNextLevel;
    }
}