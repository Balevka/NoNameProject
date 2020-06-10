using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : State
{
    public Following(Enemy currentEnemy) : base(currentEnemy)
    {
    }

    public override IEnumerator HandleState()
    {

        while (true)
        {
            yield return new WaitForFixedUpdate();

            //Debug.Log($"{Enemy.name} :: Преследование!");
            Enemy.SetTargetPosition(Enemy.target.position);
            Enemy.RotationForTarget();

            if (!Enemy.IsLookingOnPlayer())
            {
                Enemy.SetState(new FindTarget(Enemy));
                yield break;

            }
            
        }
        

       

        
    }
}
