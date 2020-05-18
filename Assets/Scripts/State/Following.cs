using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : IState
{
    private Enemy Enemy { get; set; }
    

    public void HandleState(Enemy enemy)
    {
        Enemy = enemy;

        Debug.Log($"{enemy.name} :: Преследование!");

        if (enemy.IsLookingOnPlayer())
        {
            
            SetTargetPosition(Enemy.target.position);
            
        }

        enemy.RotationForTarget();

    }

    private void SetTargetPosition(Vector3 targetPos)
    {
        Enemy.currentIndex = 0;

        Enemy.path = PathfindingSystem.InstancePath.FindPath(Enemy.startPosition, targetPos);

        for (int i = 0; i < Enemy.path.Count - 1; i++)
        {
            Debug.DrawLine(Enemy.path[i], Enemy.path[i + 1], Color.green, 10f);
        }

        if (Enemy.path != null && Enemy.path.Count > 1)
        {
            Enemy.path.RemoveAt(0);

        }
    }

    

    
}
