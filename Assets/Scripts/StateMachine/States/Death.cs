using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : State
{
    public Death(Enemy currentEnemy) : base(currentEnemy)
    {
    }

    public override IEnumerator HandleState()
    {
        //Debug.Log($"{Enemy.name} :: Умер");
        Object.Destroy(Enemy.gameObject);
        yield break;
    }


}
