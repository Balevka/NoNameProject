using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTarget : State
{
    private int time = 0;

    public FindTarget(Enemy currentEnemy) : base(currentEnemy)
    {
        
        
    }

    public override IEnumerator HandleState()
    {
        
        
        while (!Enemy.IsLookingOnPlayer())
        {
            yield return null;

            time++;
            Debug.Log(time);
            if(time > 200)
            {
                Enemy.SetTargetPosition(Enemy.enemyBeginPosition);
                Enemy.SetState(new Calm(Enemy));
                yield break;
            }
            
        }

        
        Enemy.SetState(new Following(Enemy));
        yield break;
    }



    
}
