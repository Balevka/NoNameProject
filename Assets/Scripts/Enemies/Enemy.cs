﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Enemy : StateMachine
{
    #region Сhangeable Fields
    //Здоровье
    [SerializeField] 
    private float health = 10;

    [SerializeField] 
    private GameObject healthBar;

    [SerializeField] 
    private GameObject heal;

    [SerializeField] 
    private GameObject rofl;

    //Дальность зрения
    [SerializeField]
    internal float maxDistance = 5f;

    // Угол зрения
    [SerializeField]
    internal float lookingAngle = 45f;

    [SerializeField]
    public float attackDelay = 1f;

    //Цель
    [SerializeField]
    public Transform target = null;

    [SerializeField]
    public GameObject player = null;


    [SerializeField]
    private float speed = 5f;

    //Список слоев которые не игнорируются
    [SerializeField]
    private LayerMask enemyNotIgnored;

    // Противник знает о присутствии игрока
    [SerializeField]
    private bool isPlayerHere = false;


    [SerializeField] public float attackDistantion = 2f;

    #endregion

    #region Pathfinding Fields
    //-------------------------------
    internal List<Vector3> path ;
    internal int currentIndex;
    internal Vector3 startPosition;
    //-------------------------------
    #endregion

    internal Rigidbody2D enemyRb;
    private float healthPart;
    private bool isMoving;
    public Vector3 enemyBeginPosition;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private GameObject enemyProjectTile;

    // Установка значений
    private void Start()
    {
        

        healthPart = 2 / health;
        enemyRb = GetComponent<Rigidbody2D>();
        startPosition = enemyRb.position;
        enemyBeginPosition = GetPosition();
        SetState(new Calm(this));
    }


    private void Update()
    {
        
            


        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PathfindingSystem.InstancePath.Grid.GetCellIndex(mouseWorldPosition, out int nX, out int nY);

            Debug.Log(PathfindingSystem.InstancePath.Grid.GetCellPosition(nX, nY));
        }

    }


    private void FixedUpdate()
    {
        Movement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        switch (collision.gameObject.layer)
        {
            case 14:
                GetDamage(1);

                healthBar.transform.localScale = 
                    new Vector3(healthBar.transform.localScale.x - healthPart, 0.2f, 1);

                if (IsDead())
                {
                    SetState(new Death(this));    
                }

                break;
        }
        


        
    }

    public void Attack()
    {
        GameObject enemyPt = Instantiate(enemyProjectTile, firePoint.position, firePoint.rotation);
        enemyPt.GetComponent<Rigidbody2D>().AddForce(firePoint.right * 20f, ForceMode2D.Impulse);
    }
    

    public void GetDamage(float damage)
    {
        health -= damage;
    }

    private bool IsDead()
    {
        return health <= 0 ? true : false;
    }

    public void Movement()
    {


        if (path != null)
        {
            isMoving = true;

            Vector2 targetPosition = path[currentIndex];


            if (Vector3.Distance(enemyRb.position, targetPosition) > PathfindingSystem.InstancePath.Grid.CellSize / 2)
            {

                Vector2 moveDir = (targetPosition - enemyRb.position).normalized;
                // float distanceBefore = Vector3.Distance(enemyRb.position, targetPosition);
                enemyRb.position += moveDir * 1 * Time.fixedDeltaTime;

            }
            else
            {

                startPosition = path[currentIndex];
                currentIndex++;
                if (currentIndex >= path.Count)
                    StopMoving();
            }



        }

    }

    public Vector3 DistanceToTarget()
    {
        return target.position - transform.position;
    }

    public bool IsLookingOnPlayer()
    {
        
        
        
        Vector2 distance = DistanceToTarget();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, distance, Mathf.Infinity, enemyNotIgnored);
        return lookingAngle > Vector2.Angle(transform.right, distance) && distance.magnitude <= maxDistance && hit.collider.gameObject.layer == 11;
    }

    public void RotationForTarget()
    {
        Vector3 lookDir = DistanceToTarget();
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        enemyRb.rotation = angle;
    }

    public void StopMoving()
    {
        path = null;
        isMoving = false;
        
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPos)
    {
        currentIndex = 0;
        

        path = PathfindingSystem.InstancePath.FindPath(startPosition, targetPos);

        for (int i = 0; i < path.Count - 1; i++)
        {
            Debug.DrawLine(path[i], path[i + 1], Color.green, 10f);
        }

        if (path != null && path.Count > 1)
        {
            path.RemoveAt(0);

        }
    }

    public void FindTarget()
    {
        if(!isMoving)
        {
            PathfindingSystem.InstancePath.Grid.GetCellIndex(GetPosition(), out int x, out int y);
            
            List<Node> searchArea = CreateAreaSearch(2, PathfindingSystem.InstancePath.GetNode(x, y));
            Debug.Log(searchArea.Count);
            Debug.Log(searchArea[Random.Range(0, searchArea.Count - 1)]);
            /*Node node;

            SetTargetPosition(PathfindingSystem.InstancePath.Grid.GetCellPosition(node.GridIndexX, node.GridIndexY));
            */
        }  
    }


    public List<Node> CreateAreaSearch(int radiusSearch, Node node)
    {

        if (path != null)
        {

            /* List<Vector3> pathVectors = path;
             pathVectors.Reverse();
             Vector3 lastPosition = pathVectors[0];
             pathVectors.Clear();*/

            /*Node endNode = PathfindingSystem.InstancePath.Grid.GetCellValue(lastPosition);*/

            List<Node> searchZone = PathfindingSystem.InstancePath.GetNeighboursNodes(node);
            Debug.Log(searchZone.Count);
            int countNodes = searchZone.Count;

            for (int i = 0; i < countNodes; i++)
            {
                List<Node> neighbourList = PathfindingSystem.InstancePath.GetNeighboursNodes(searchZone[i]);

                foreach (Node n in neighbourList)
                {
                    if (!searchZone.Contains(n))
                    {
                        searchZone.Add(n);
                    }
                }
            }

            return searchZone;

        }


        return null;

    }
    public void SpawnObjects()
    {
        if (Random.Range(0, 100) <= 10)
        {
           var Heal = Instantiate(heal, new Vector2(this.gameObject.transform.position.x + 0.5f, this.gameObject.transform.position.y), Quaternion.identity);
            Heal.GetComponent<Heal>().player = player;
        }
        if (Random.Range(0, 100) <= 30)
        {
            var Rofl = Instantiate(rofl, new Vector2(this.gameObject.transform.position.x - 0.5f, this.gameObject.transform.position.y), Quaternion.identity);
            Rofl.GetComponent<Roflanium>().player = player;
        }
    }
}