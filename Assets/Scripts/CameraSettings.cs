using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
internal class CameraSettings : MonoBehaviour
{
    private new Camera camera;
    [SerializeField] private bool isOrthographic = true;
    [SerializeField] internal float cameraSpeed = 5f;
    

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();

        camera.orthographic = isOrthographic;

    }


    

    // Update is called once per frame
    void Update()
    {
        
    }
}
