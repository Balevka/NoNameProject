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
    public Transform target;

    [SerializeField]
    private LayerMask enemyNotIgnored;

    [SerializeField]
    private bool isPlayerHere = false;
   


    private Rigidbody2D enemyRb;
    private List<Vector3> path;
    private Vector2 startPosition;
    private int currentIndex;

    void Start()
    {        
        enemyRb = GetComponent<Rigidbody2D>();
        startPosition = enemyRb.position;
    }


    private void Update()
    {
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (IsLookingOnPlayer())
        {
            SetTargetPosition(target.position);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PathfindingSystem.InstancePath.Grid.GetCellIndex(mouseWorldPosition, out int nX, out int nY);
            
            Debug.Log(PathfindingSystem.InstancePath.Grid.GetCellPosition(nX, nY));
        }
        Movement();
    }

    private void Movement()
    {
        if (path != null)
        {


            Vector2 targetPosition = path[currentIndex];
            

            if (Vector3.Distance(enemyRb.position, targetPosition) > PathfindingSystem.InstancePath.Grid.CellSize / 2)
            {
                
                Vector2 moveDir = (targetPosition - enemyRb.position).normalized;
                // float distanceBefore = Vector3.Distance(enemyRb.position, targetPosition);
                enemyRb.position += moveDir * 2 * Time.fixedDeltaTime;
                
            }
            else
            {
               
                startPosition = path[currentIndex];
                currentIndex++;
                if (currentIndex >= path.Count)
                    path = null;
            }



        }

    }

    private void SetTargetPosition(Vector3 targetPosition)
    {



        currentIndex = 0;
        path = PathfindingSystem.InstancePath.FindPath(startPosition, targetPosition);


        for (int i = 0; i < path.Count - 1; i++)
        {
            Debug.DrawLine(path[i], path[i + 1], Color.green, 10f);
        }




        if (path != null && path.Count > 1)
        {
            path.RemoveAt(0);

        }

    }




    /*private void StopMoving()
    {
        path = null;
    }*/

    public Vector3 GetPosition()
    {
        return transform.position;
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



}
