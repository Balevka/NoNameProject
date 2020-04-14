using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public delegate void MethodsContainer();

    public static event MethodsContainer OnTeleportEvent;

    public Circle Circle1;
    public Circle Circle2;

    private void OnEnable()
    {
        OnTeleportEvent += Circle1.TeleportUp;
        OnTeleportEvent += Circle2.TeleportUp;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) OnTeleportEvent.Invoke();
    }

}
