using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calm : State
{
    public Calm(Enemy currentEnemy) : base(currentEnemy)
    {
    }

    public override IEnumerator HandleState()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            Debug.Log($"{Enemy.name} :: Нахожусь в спокойствии");
            if (Enemy.IsLookingOnPlayer())
            {
                Enemy.SetState(new Detect(Enemy));
                yield break;
            }
        }
    }
}
