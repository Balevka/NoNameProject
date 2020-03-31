using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : ScriptableObject
{
    private Camera cam;
    private GameObject obj;


    public Camera Camera
    {
        set
        {
            if (value != null)
            {
                cam = value;
            }
            else throw new System.Exception("Камера не найдена");
        }

        get
        {
            return cam;
        }
    }

    public GameObject Obj
    {
        set
        {
            if (value != null)
            {
                obj = value;
            }
            else throw new System.Exception("Объект не найдена");
        }


        get
        {
            return obj;
        }
    }


    public CameraFollow(Camera camera, GameObject gameObject)
    {
        Camera = camera;
        Obj = gameObject;
       
    }


    public void Follow()
    {
        Camera.transform.position = new Vector3(Obj.transform.position.x, Obj.transform.position.y, Camera.transform.position.z);
    }
}
