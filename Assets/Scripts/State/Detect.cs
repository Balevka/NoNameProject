using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : IState
{
    public Enemy Enemy { get; set; }

    

    public void HandleState(Enemy enemy)
    {
        Enemy = enemy;

        Debug.Log($"{Enemy.name} :: Обнаружен");


        Enemy.State = new Following();
    }

    
    
    


}
