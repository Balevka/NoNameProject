using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : IState
{
    public Enemy Enemy { get; set; }

    public void HandleState(Enemy enemy)
    {
        Debug.Log($"{enemy.name} :: Обнаружен");


        enemy.State = new Following();
    }

    
    
    


}
