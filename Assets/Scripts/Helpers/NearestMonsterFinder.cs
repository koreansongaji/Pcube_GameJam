using System.Collections.Generic;
using UnityEngine;

public static class NearestMonsterFinder
{
    public static MonsterBehavior FindNearestMonster(Vector3 position, float range)
    {
        MonsterBehavior nearestMonster = null;
        float minDist = float.MaxValue;

       var monsters = GameObject.FindGameObjectsWithTag("Monster");
       foreach (GameObject obj in monsters)
        {
            MonsterBehavior monster = obj.GetComponent<MonsterBehavior>();
            
            float dist = Vector3.Distance(position, obj.transform.position);
            if (dist < range && dist < minDist)
            {
                nearestMonster = monster;
                minDist = dist;
            }
        }
        
        return nearestMonster;
    }
}