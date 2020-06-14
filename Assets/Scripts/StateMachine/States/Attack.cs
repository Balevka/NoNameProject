using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    
    public Attack(Enemy currentEnemy) : base(currentEnemy)
    {
        
    }

    public override IEnumerator StartState()
    {
        
        yield break;
    }

    public override IEnumerator HandleState()
    {
        Enemy.StopMoving();

        while (Enemy.DistanceToTarget().magnitude <= Enemy.attackDistantion)
        {
            Enemy.Attack();
            yield return new WaitForSeconds(Enemy.attackDelay);
            Enemy.RotationForTarget();
        }

        Enemy.SetState(new Following(Enemy));
        yield break;
    }

}
