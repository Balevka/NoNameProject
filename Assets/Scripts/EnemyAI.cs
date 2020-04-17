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
    



    private void Start()
    {
        

        
        
    }



    private void FixedUpdate()
    {




        Debug.DrawRay(rbEnemy.position, transform.right,  Color.yellow);
        if (hit = Physics2D.Raycast(rbEnemy.position, transform.right, 4f, enemySee))
        {
            
            if ( hit.collider.gameObject.layer == 11)
            {
                
                playerPos = hit.collider.transform.position;
                Vector2 lookDir = playerPos - (Vector2)transform.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                this.transform.localRotation = Quaternion.Euler(0f, 0f, angle);

                rbEnemy.position = Vector3.MoveTowards(transform.position, hit.collider.transform.position, Time.fixedDeltaTime * 2);

            }

            



        }
        

        


        

    }


    


    




    
}
