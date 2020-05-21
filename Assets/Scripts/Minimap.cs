using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private Vector3 toPosition;
    private new Transform camera;
    void LateUpdate()
    {
        toPosition = player.position;
        toPosition.z = camera.position.z;
        camera.position = Vector3.Lerp(camera.position, toPosition, Time.deltaTime);
    }
}
