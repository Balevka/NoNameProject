using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    

    
    [SerializeField] private Transform player;

    [SerializeField]internal bool isCameraMove = true;
    internal CameraSettings settings;

    private Vector3 toPosition;
    private new Transform camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = gameObject.transform;
        camera.position = new Vector3(player.position.x, player.position.y, camera.position.z);
        settings = this.GetComponent<CameraSettings>();
        Cursor.SetCursor(settings.cursorImage,Vector2.zero, CursorMode.Auto);
        
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (isCameraMove) 
        {
            toPosition = player.position;
            toPosition.z = camera.position.z;
            camera.position = Vector3.Lerp(camera.position, toPosition, settings.cameraSpeed * Time.deltaTime);
        }
        
        
        
    }


    
}
