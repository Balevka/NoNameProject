using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTarget : IState
{
    public Enemy Enemy { get; set; }
    private PathfindingSystem system = PathfindingSystem.InstancePath;
    private int radiusOfSearch = 2;
    private Vector3 searchPositionNode;
    int i = 0;

    public void HandleState(Enemy enemy)
    {
        Enemy = enemy;
        Debug.Log($"{Enemy.name} :: Начинаю поиск цели");

        if (Enemy.IsLookingOnPlayer())
        {
            Enemy.State = new Following();
        }

        
        

        while(i != 2)
        {
            Enemy.SetTargetPosition(system.Grid.GetCellPosition(GetSearchZone()[0].GridIndexX, GetSearchZone()[0].GridIndexY));
        }
       

        
    }



    private List<Node> GetSearchZone()
    {
        system.Grid.GetCellIndex(Enemy.GetPosition(), out int x, out int y);
        List<Node> searchZone = new List<Node>();
        Node currentNode;


        for(int i = 1; i<=radiusOfSearch; i++)
        {
            if ((currentNode = system.GetNode(x, y+i)) != null)
            {
                searchZone.Add(currentNode);

                // Верхняя-правая диагональ
                if ((currentNode = system.GetNode(x + i, y + i)) != null)
                    searchZone.Add(currentNode);

                // Верхняя-левая диагональ
                if ((currentNode = system.GetNode(x - i, y + i)) != null)
                    searchZone.Add(currentNode);
            }

            if ((currentNode = system.GetNode(x, y - i)) != null)
            {
                searchZone.Add(currentNode);

                //Нижняя-правая диагональ
                if ((currentNode = system.GetNode(x + i, y - i)) != null)
                    searchZone.Add(currentNode);

                // Нижняя-левая диагональ
                if ((currentNode = system.GetNode(x - i, y - i)) != null)
                    searchZone.Add(currentNode);
            }

            if ((currentNode = system.GetNode(x + i, y)) != null)
                searchZone.Add(currentNode);

            // Левый узел
            if ((currentNode = system.GetNode(x - i, y)) != null)
                searchZone.Add(currentNode);
        }

        return searchZone;
    }


    
}
