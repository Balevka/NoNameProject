using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Здоровье
    [SerializeField] 
    private float health = 10;

    [SerializeField] 
    private GameObject healthBar;

    //Дальность зрения
    [SerializeField]
    internal float maxDistance = 5f;

    // Угол зрения
    [SerializeField]
    internal float lookingAngle = 45f;

    //Цель
    [SerializeField]
    public Transform target = null;


    [SerializeField]
    private float speed = 5f;

    //Список слоев которые не игнорируются
    [SerializeField]
    private LayerMask enemyNotIgnored;

    // Противник знает о присутствии игрока
    [SerializeField]
    private bool isPlayerHere = false;

    //Состояние противника
    internal IState State { get; set; }

    //Путь
    //-------------------------------
    internal List<Vector3> path ;
    internal int currentIndex;
    internal Vector3 startPosition;
    //-------------------------------


    internal Rigidbody2D enemyRb;
    private float healthPart;
    public bool isMoving;
    

    // Установка значений
    void Start()
    {
        
        State = new Calm();
        

        healthPart = 2 / health;
        enemyRb = GetComponent<Rigidbody2D>();
        startPosition = enemyRb.position; 
    }


    private void Update()
    {
        State.HandleState(this);

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
                    State = new Death();      
                }

                break;
        }
        


        
    }

    private void GetDamage(float damage)
    {
        health -= damage;
    }

    private bool IsDead()
    {
        return health <= 0 ? true : false;
    }

    private void Movement()
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

    public bool IsLookingOnPlayer()
    {
        Vector2 distance = target.position - transform.position;
        return lookingAngle > Vector2.Angle(transform.right, distance) && distance.magnitude <= maxDistance;
    }

    public void RotationForTarget()
    {
        Vector3 lookDir = target.position - transform.position;
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
}
