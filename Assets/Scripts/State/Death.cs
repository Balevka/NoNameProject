using UnityEngine;

public class Death : IState
{
    public Enemy Enemy { get; set; }

    

    public void HandleState(Enemy enemy)
    {
        Enemy = enemy;
        Debug.Log($"{Enemy.name} :: Умер");
        Object.Destroy(Enemy.gameObject);
    }

    

    
}
