using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calm : IState
{
    public Enemy Enemy { get; set; }

    public void HandleState(Enemy enemy)
    {
        Enemy = enemy;

        Debug.Log($"{enemy.name} :: Нахожусь в спокойствии");
        if (enemy.IsLookingOnPlayer())
        {
            enemy.State = new Detect();
        }
    }


    
}
