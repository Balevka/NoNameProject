using System.Collections.Generic;
using UnityEngine;


public class PathfindingSystem
{
    

    // Константы передвижения
    public const int MOVE_STRAIGHT_COST = 10;
    public const int MOVE_DIAGONAL_COST = 14;

    //Система сеток
    private GridSystem<Node> grid;
    private bool DebugMode { get; set; }

    // Список не проверенных узлов
    private List<Node> openList;

    // Список проверенных узлов
    private List<Node> closeList;

    
    // Текст для отладки
    private List<TextMesh> debugTextArrayH;
    private List<TextMesh> debugTextArrayG;
    private List<TextMesh> debugTextArrayF;

    // Свойство для получения сетки
    public GridSystem<Node> Grid
    {
        get { return grid; }
    }

    public static PathfindingSystem InstancePath { get; private set; }


    // Конструктор системы поиска пути
    public PathfindingSystem(int width, int height, float cellSize, Vector3 position, bool debugMode)
    {
        InstancePath = this;
        DebugMode = debugMode;
        grid = new GridSystem<Node>(width, height, cellSize, position, (int x, int y) => new Node(x, y), DebugMode);
        /*
        debugTextArrayH = new List<TextMesh>();
        debugTextArrayG = new List<TextMesh>();
        debugTextArrayF = new List<TextMesh>();
        */
    }


    // Метод для получения узла по индексу
    public Node GetNode(int x, int y)
    {
        return grid.GetCellValue(x, y);
    }


    // Поиск пути по индексу узла
    public List<Node> FindPath(int xStart, int yStart, int xEnd, int yEnd)
    {


        Node startNode = GetNode(xStart, yStart);
        Node endNode = GetNode(xEnd, yEnd);

        if (!endNode.IsWalkable)
            return null;

        openList = new List<Node>() { startNode };
        closeList = new List<Node>();


        // Обнуление узлов
        for (int x = 0; x < grid.Width; x++)
            for (int y = 0; y < grid.Height; y++)
            {
                Node node = GetNode(x, y);
                node.G = 0;
                node.ParentNode = null;
            }


        // Установка значений стартовой точки
        startNode.G = 0;
        startNode.H = CalculateDistanceBetweenNodes(startNode, endNode);
        SetDebugTextForNode(startNode.GridIndexX, startNode.GridIndexY);


        while (openList.Count > 0)
        {
            Node currentNode = GetLowestNodeToF(openList);

            if (currentNode == endNode)
                return CalculatePath(endNode, startNode);

            closeList.Add(currentNode);
            openList.Remove(currentNode);


            foreach (Node neighbourNode in GetNeighboursNodes(currentNode))
            {
                if (closeList.Contains(neighbourNode))
                    continue;

                if (!neighbourNode.IsWalkable)
                {
                    closeList.Add(neighbourNode);
                    continue;
                }

                neighbourNode.G = currentNode.G + CalculateDistanceBetweenNodes(currentNode, neighbourNode);


                if ((openList.Contains(neighbourNode) && neighbourNode.G < currentNode.G) || !openList.Contains(neighbourNode))
                {
                    neighbourNode.ParentNode = currentNode;
                    neighbourNode.G = neighbourNode.ParentNode.G + CalculateDistanceBetweenNodes(currentNode, neighbourNode);
                    neighbourNode.H = CalculateDistanceBetweenNodes(neighbourNode, endNode);
                }

                if (!openList.Contains(neighbourNode))
                {
                    openList.Add(neighbourNode);
                    SetDebugTextForNode(neighbourNode.GridIndexX, neighbourNode.GridIndexY);
                }
            }
        }

        return null;

    }


    // Поиск пути по позиции. Вовращает список векторов пути
    public List<Vector3> FindPath(Vector3 startPos, Vector3 endPos)
    {
        grid.GetCellIndex(startPos, out int startX, out int startY);
        grid.GetCellIndex(endPos, out int endX, out int endY);

        List<Node> path = FindPath(startX, startY, endX, endY);

        if (path != null)
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (Node pathNode in path)
            {
                vectorPath.Add(grid.GetCellPosition(pathNode.GridIndexX, pathNode.GridIndexY) + Vector3.one * (grid.CellSize / 2));
            }

            return vectorPath;
        }

        return null;
    }

    
    //---------------------------------------------------------------------------

    // Рассчитывает дистанцию между узлами отнносительно их стоимости перемещения и направления
    private int CalculateDistanceBetweenNodes(Node start, Node end)
    {
        int xDistance = Mathf.Abs(start.GridIndexX - end.GridIndexX);
        int yDistance = Mathf.Abs(start.GridIndexY - end.GridIndexY);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    // Возрвращает узел с наименьшей стоимостью F, выбранный из соседних узлов конкретного узла
    private Node GetLowestNodeToF(List<Node> openList)
    {
        Node lowestNode = openList[0];

        for (int i = 1; i < openList.Count; i++)
        {
            if (openList[i].F <= lowestNode.F)
                lowestNode = openList[i];
        }


        return lowestNode;
    }



    // Возвращает список соседних узлов конкретного узла
    private List<Node> GetNeighboursNodes(Node node)
    {
        List<Node> neighbourList = new List<Node>();
        Node currentNode;

        // Верхний узел

        if ((currentNode = GetNode(node.GridIndexX, node.GridIndexY + 1)) != null)
        {
            neighbourList.Add(currentNode);

            // Верхняя-правая диагональ
            if ((currentNode = GetNode(node.GridIndexX + 1, node.GridIndexY + 1)) != null)
                neighbourList.Add(currentNode);

            // Верхняя-левая диагональ
            if ((currentNode = GetNode(node.GridIndexX - 1, node.GridIndexY + 1)) != null)
                neighbourList.Add(currentNode);
        }


        // Нижний узел

        if ((currentNode = GetNode(node.GridIndexX, node.GridIndexY - 1)) != null)
        {
            neighbourList.Add(currentNode);

            //Нижняя-правая диагональ
            if ((currentNode = GetNode(node.GridIndexX + 1, node.GridIndexY - 1)) != null)
                neighbourList.Add(currentNode);

            // Нижняя-левая диагональ
            if ((currentNode = GetNode(node.GridIndexX - 1, node.GridIndexY - 1)) != null)
                neighbourList.Add(currentNode);
        }

        // Правый узел
        if ((currentNode = GetNode(node.GridIndexX + 1, node.GridIndexY)) != null)
            neighbourList.Add(currentNode);

        // Левый узел
        if ((currentNode = GetNode(node.GridIndexX - 1, node.GridIndexY)) != null)
            neighbourList.Add(currentNode);

        return neighbourList;

    }


    // Рассчитывает пути с конца
    private List<Node> CalculatePath(Node endNode, Node startNode)
    {
        
        List<Node> pathNodeList = new List<Node>() { endNode };
        Node currentNode = endNode;
        int i = 0;
        while(currentNode.ParentNode != null)
        {
            pathNodeList.Add(currentNode.ParentNode);
            currentNode = currentNode.ParentNode;

            
        }

        pathNodeList.Reverse();
        return pathNodeList;
    }


    // Устанавливает текст для дебага
    private  void SetDebugTextForNode(int x, int y)
    {
        if (DebugMode)
        {
            DebugTools.CreateText("F " + GetNode(x, y).F, 0.1f, grid.GetCellPosition(x, y) + Vector3.one * (grid.CellSize / 2), Color.white, TextAnchor.MiddleCenter);
            DebugTools.CreateText("H " + GetNode(x, y).H, 0.1f, grid.GetCellPosition(x, y) + Vector3.one * ((grid.CellSize / 2) + 0.2f), Color.white, TextAnchor.LowerRight);
            DebugTools.CreateText("G " + GetNode(x, y).G, 0.1f, grid.GetCellPosition(x, y) + Vector3.one * ((grid.CellSize / 2) + 0.2f), Color.white, TextAnchor.LowerLeft);

        }


    }



}


