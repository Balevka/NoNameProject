using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class EnemyAI : MonoBehaviour
{
    
    

    [SerializeField]private LayerMask enemySee;
    [SerializeField] private Rigidbody2D rbEnemy;


    private Vector2 playerPos;
    private RaycastHit2D hit;
    private bool isPlayereHere = false;
    private Vector2 startPos;
    



    private void Start()
    {
        startPos = rbEnemy.position;
        
        
        
    }


    private void FixedUpdate()
    {


        Debug.DrawRay(transform.position, transform.right, Color.yellow);
        hit = Physics2D.Raycast(transform.position, transform.right, 4f, enemySee);
        
        if (hit)
        {
            
            //OnPlayerDetected
            if ( hit.collider.gameObject.layer == 11)
            {
                
                playerPos = hit.collider.transform.position;
                Vector2 lookDir = playerPos - (Vector2)transform.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                this.transform.localRotation = Quaternion.Euler(0f, 0f, angle);

               
                isPlayereHere = true;

            }

        }

        if (isPlayereHere)
        {
            rbEnemy.position = Vector3.MoveTowards(rbEnemy.position, playerPos, Time.fixedDeltaTime);
            

        }

        if(rbEnemy.position == playerPos)
        {

            transform.localRotation *= Quaternion.Euler(0f, 0f, 1);
            

        }


    }

  
}
