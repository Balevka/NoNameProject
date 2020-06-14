using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDemo : MonoBehaviour
{
	public Transform camera;
	private void FixedUpdate()
	{
		camera.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10);
	}
}
