using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calm : IState
{
    public Enemy Enemy { get; set; }

    public void HandleState(Enemy enemy)
    {
        Enemy = enemy;

        Debug.Log($"{Enemy.name} :: Нахожусь в спокойствии");
        if(Enemy.IsLookingOnPlayer())
        {
            Enemy.State = new Detect();
        }
    }


    
}
