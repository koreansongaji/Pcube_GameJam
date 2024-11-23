using System.Collections.Generic;
using UnityEngine;

public static class NearestMonsterFinder
{
    public static MonsterBehavior FindNearestMonster(Vector3 position, float range)
    {
        MonsterBehavior nearestMonster = null;
        float minDist = float.MaxValue;
        
        // todo : FindObjectsOfType 대신 몬스터 리스트를 가지고 있는 매니저를 만들어서 그 매니저에서 몬스터 리스트를 받아오는 것이 좋다.
        foreach (MonsterBehavior monster in (new []{ Object.FindObjectOfType<MonsterBehavior>() }))
        {
            float dist = Vector3.Distance(position, monster.transform.position);
            if (dist < range && dist < minDist)
            {
                nearestMonster = monster;
                minDist = dist;
            }
        }
        
        return nearestMonster;
    }
}