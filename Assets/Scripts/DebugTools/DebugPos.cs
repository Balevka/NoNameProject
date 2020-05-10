using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPos : MonoBehaviour
{
    public enum positionType
    {
        standPos,
        worldPos,
        screenPos,
    }

    public Camera cam;
    void Start()
    {

        //Координаты видимой области
        Debug.Log(cam.ScreenToWorldPoint(transform.position));
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
            Debug.Log(cam.ScreenToWorldPoint(cam.ScreenToWorldPoint(Input.mousePosition)));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.ClearDeveloperConsole();
        }
    }


    
}
