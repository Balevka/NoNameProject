using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    
    private void Awake()
    {
        Health.OnStateChanged += OnHealthChanged;
    }

    private void Update()
    {
        
    }

    private void OnHealthChanged(int state)
    {
        Debug.Log(state);
    }

}
