using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Enemy Enemy { get; set; }

    public State(Enemy currentEnemy)
    {
        Enemy = currentEnemy;
    }

    public virtual IEnumerator StartState()
    {
        
        // Выполнение действий

        
        yield break;
    }


    public virtual IEnumerator HandleState()
    {
        yield break;
    }

    public virtual IEnumerator EndState()
    {
        yield break;
    }

    


}
