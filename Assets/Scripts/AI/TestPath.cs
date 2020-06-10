using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPath : MonoBehaviour
{
    
    PathfindingSystem system;
    public GameObject enemy;
    Rigidbody2D enemyRb;
    
    int currentIndex = 0;
    
    Vector3 startPosition;
    Vector3 endPosition;
    List<Vector3> path;

    // Start is called before the first frame update

    void Start()
    {
        
        
        


        system = new PathfindingSystem(10, 10, 2f, transform.position, true);
        system.GetNode(4, 2).IsWalkable = false;
        system.GetNode(4, 3).IsWalkable = false;
        system.GetNode(4, 4).IsWalkable = false;

        

        enemyRb = enemy.GetComponent<Rigidbody2D>();


        startPosition = system.Grid.GetCellPosition(0, 0);
        
        











    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Movement();
        if (Input.GetMouseButtonDown(0))
        {
           
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            system.Grid.GetCellIndex(mouseWorldPosition, out int x, out int y);

            
            SetTargetPosition(system.Grid.GetCellPosition(x, y));
            

        }




        if (Input.GetMouseButton(1))
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            system.Grid.GetCellIndex(mouseWorldPosition, out int nX, out int nY);

            Node node = system.GetNode(nX, nY);
            Debug.Log($"{node.F}, {node.G}, {node.H}, {node.ParentNode}");
        }
    }

    private void Movement()
    {

        if (path != null)
        {
            

            Vector3 targetPosition = path[currentIndex];

            if (Vector3.Distance(enemyRb.position, targetPosition) > system.Grid.CellSize/2)
            {
                Vector3 moveDir = (targetPosition - (Vector3)enemyRb.position).normalized;
                
                float distanceBefore = Vector3.Distance(enemyRb.position, targetPosition);
                enemyRb.position = (Vector3)enemyRb.position + moveDir * 30 * Time.fixedDeltaTime;
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
        path = system.FindPath(startPosition, targetPosition);

        List<Node> searchN = CreateAreaSearch(2, PathfindingSystem.InstancePath.Grid.GetCellValue(targetPosition));
        Debug.Log(searchN.Count);


        for (int i = 0; i < path.Count - 1; i++)
        {
            Debug.DrawLine(path[i], path[i + 1], Color.green, 10f);
        }

        


        if (path != null && path.Count > 1)
        {
            path.RemoveAt(0);
            
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

            for(int i = 0; i<countNodes; i++)
            {
                List<Node> neighbourList = PathfindingSystem.InstancePath.GetNeighboursNodes(searchZone[i]);

                foreach(Node n in neighbourList)
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
}

