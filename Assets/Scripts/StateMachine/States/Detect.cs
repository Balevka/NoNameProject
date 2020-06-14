using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : State
{
    public Detect(Enemy currentEnemy) : base(currentEnemy)
    {
    }

    public override IEnumerator HandleState()
    {
        //Debug.Log("Обнаружен!");

        if (Enemy.DistanceToTarget().magnitude <= Enemy.attackDistantion)
        {
            Enemy.SetState(new Attack(Enemy));
            yield break;
        }
        
        
        Enemy.SetState(new Following(Enemy));
        yield break;
 
    }
}
