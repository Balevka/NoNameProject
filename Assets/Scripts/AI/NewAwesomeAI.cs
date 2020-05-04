using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class NewAwesomeAI : MonoBehaviour
{
    [SerializeField]
    private float maxDistance = 5f;

    [SerializeField]
    private float lookingAngle = 45f;

    [SerializeField] 
    private Transform target;

    [SerializeField]
    private LayerMask enemyNotIgnored;

    [SerializeField]
    private bool isPlayerHere = false;

    [SerializeField]
    private Transform gridGameObject;

 
    // Main

    private Rigidbody2D enemyRb;
    RaycastHit2D hit;
    Ray2D ray;
    int count = 0;

    private Pathfinding pathfinding;
    private Vector3 startPosition;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;

    // Debug
    private Vector2 distance;
    private BoxCollider2D col;
    private Vector3 startPosEnemy;
    private Vector3 lastPosPlayer;



    void Start()
    {
       
        enemyRb = GetComponent<Rigidbody2D>();
        pathfinding = new Pathfinding(36, 18, .5f, gridGameObject.transform.position, true);
        startPosEnemy = enemyRb.position;

    }


    private void Update()
    {
        


        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mousePosition, out int x, out int y);
            //pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWolkable);
            pathfinding.GetNode(x, y).isWolkable = false;
            Debug.Log("Node " + x + ", " + y + "is not walkable!");

        }

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        HandleMovement();





        Debug.DrawRay(transform.position, transform.right, Color.green);




        Debug.Log(isPlayerHere + " " + IsLookingOnPlayer());

        if (IsLookingOnPlayer(out Vector2 distance))
        {
            Debug.Log("LOOK!");
            if(hit = Physics2D.Raycast(transform.position, distance, distance.magnitude, enemyNotIgnored))
            {

                if (hit.collider.gameObject.layer == 11)
                {
                    isPlayerHere = true;
                    SetTargetPosition(target.position);
                    Debug.Log(hit.collider.name);
                    lastPosPlayer = hit.transform.position;
                    Debug.DrawRay(transform.position, distance, Color.red);
                }
            }
            
                
                
                
            

            


            


        }

        if(isPlayerHere && !IsLookingOnPlayer())
        {
            
           
            if(count >= 100)
            {
                isPlayerHere = false;
                count = 0;
                
            }

            count++;
            Debug.Log(count);
            
            SetTargetPosition(lastPosPlayer);
            Debug.DrawRay(transform.position, transform.right, Color.yellow);
            Debug.Log(lastPosPlayer);
        }


        if(!isPlayerHere && !IsLookingOnPlayer())
        {
            SetTargetPosition(startPosEnemy);
        }


        

    }


    private void HandleMovement()
    {
        if (pathVectorList != null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                enemyRb.position = (Vector3)enemyRb.position + moveDir * 10 * Time.fixedDeltaTime;
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                   
                }
            }
        }
        else
        {
            
        }
    }

    private void StopMoving()
    {
        pathVectorList = null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }



    private bool IsLookingOnPlayer()
    {

        Vector2 distance = target.position - transform.position;
        

        return lookingAngle > Vector2.Angle(transform.right, distance) && distance.magnitude <= maxDistance;
    }


    private bool IsLookingOnPlayer(out Vector2 distance)
    {

        

        distance = target.position - transform.position;
        

        return lookingAngle > Vector2.Angle(transform.right, distance) && distance.magnitude <= maxDistance;
    }




    

    private static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    private static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }



}
