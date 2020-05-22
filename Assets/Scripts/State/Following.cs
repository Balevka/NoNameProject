using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Following : IState
{
    public Enemy Enemy { get; set; }
    
    





    public void HandleState(Enemy enemy)
    {
        Enemy = enemy;
        
        if (Enemy.IsLookingOnPlayer())
        {
            
            Debug.Log($"{Enemy.name} :: Преследование!");
            Enemy.SetTargetPosition(Enemy.target.position);

            
            
        }
        
        /*if(!Enemy.IsLookingOnPlayer() && !Enemy.isMoving)
        {
            Enemy.State = new FindTarget();
        }*/


        
            
        

        Enemy.RotationForTarget();

    }

   
}
