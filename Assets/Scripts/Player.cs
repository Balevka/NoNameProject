using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 2f;
    [SerializeField] internal Animator playerAnimator;
    
    


    private Rigidbody2D rb;
   
    private Vector2 movement;
    private Vector2 mousePos;
    [SerializeField] private Camera cam;
    





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


    private float Rotation()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        return angle;
    }

    private void AnimationSwitch()
    {
        Debug.Log(playerAnimator.GetCurrentAnimatorStateInfo(0));

    }











}
