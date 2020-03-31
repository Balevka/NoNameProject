using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2;
    public Camera cam;

    private Rigidbody2D rb;
    private CameraFollow Follow;


    
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Follow = new CameraFollow(cam, gameObject);
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
        Follow.Follow();
        
    }
}
