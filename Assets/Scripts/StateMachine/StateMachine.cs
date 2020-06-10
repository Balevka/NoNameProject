using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State State { get; set; }

    public void SetState(State state)
    {
        State = state;
        
        StartCoroutine(State.HandleState());
    }
}
