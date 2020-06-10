using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : State
{
    public Detect(Enemy currentEnemy) : base(currentEnemy)
    {
    }

    public override IEnumerator HandleState()
    {
        //Debug.Log("Обнаружен!");

        Enemy.SetState(new Following(Enemy));

        yield break;
    }
}
