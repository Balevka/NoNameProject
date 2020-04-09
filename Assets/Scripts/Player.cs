using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 2f;
    
    


    private Rigidbody2D rb;
    private Vector2 movement;
    





    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Movement();

            
        
    }

    



    private void Movement()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
        
    }











}
