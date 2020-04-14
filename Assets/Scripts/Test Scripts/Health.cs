using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public delegate void StateChange(int state);
    public static event StateChange OnStateChanged;

    public int state = 100;

    private void Update()
    {
        OnStateChanged.Invoke(state--);
    }

}
