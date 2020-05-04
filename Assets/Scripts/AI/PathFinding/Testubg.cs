using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using CodeMonkey.Utils;

public class Testubg : MonoBehaviour
{

    Pathfinding pathfinding;
    Vector3 startPosision;
    public GameObject walls;
    private void Start()
    {

        walls.transform.localScale = new Vector3(10, 10) + Vector3.one * 10f;
        pathfinding = new Pathfinding(10, 10, 10f, transform.position, true);
        startPosision = new Vector3(0, 0);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            

            List<PathNode> path = pathfinding.FindPath((int)startPosision.x, (int)startPosision.y, x, y);

            

            if(path != null)
            {
                for(int i = 0; i<path.Count - 1; i++)
                {

                    Debug.Log(path[i].x + " " + path[i].y);
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one * 5f, Color.green, 5); ;
                }

                startPosision.x = x;
                startPosision.y = y;
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mousePosition, out int x, out int y);
            pathfinding.GetNode(x, y).isWolkable = false;
            Instantiate(walls, new Vector3(x, y)*10f + Vector3.one * 5f, Quaternion.identity);
            Debug.Log("Node " + x + ", " + y + "is not walkable!");
            
        }
    }


}
