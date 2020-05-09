using System.Collections.Generic;
using System;
using UnityEngine;

public class GridSystem<TGridObject>
{
    // Список полей
    private int width;
    private int height;
    private float cellSize;
    private Vector3 gridPosition;

    private TGridObject[,] gridArray;
    private TextMesh [,] debugTextArray;

    // Список свойств
    public int Width
    {
        get
        {
            return width;
        }

        set
        {
            width = value;
        }
    }
    public int Height
    {
        get
        {
            return height;
        }

        set
        {
            height = value;
        }
    }
    public float CellSize
    {
        get
        {
            return cellSize;
        }

        set
        {
            cellSize = value;
        }

    }
    public Vector3 GridPosition
    {
        get
        {
            return gridPosition;
        }

        set
        {
            gridPosition = value;
        }
    }
    public bool DebugMode { get; set; }

    
    // Конструктор работоющий со стандартными типами
    public GridSystem(int width, int height, float cellSize, Vector3 gridPosition, bool debugMode = false)
    {
        Width = width;
        Height = height;
        CellSize = cellSize;
        DebugMode = debugMode;
        GridPosition = gridPosition;

        gridArray = new TGridObject[Width, Height];

        if (DebugMode == true)
            DebugDrawGrid();

        for (int x = 0; x<Width; x++)
        {
            for(int y = 0; y<Height; y++)
            {
                gridArray[x, y] = default;
            }  
        }    
    }


    // Конструктор работающий с пользовательскими типами
    public GridSystem(int width, int height, float cellSize, Vector3 gridPosition, Func<int, int, TGridObject> createGridObject,  bool debugMode = false)
    {
        Width = width;
        Height = height;
        CellSize = cellSize;
        DebugMode = debugMode;
        GridPosition = gridPosition;

        gridArray = new TGridObject[Width, Height];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                gridArray[x, y] = createGridObject(x, y);
            }
        }

        if (DebugMode == true)
            DebugDrawGrid();
    }


    // Возвращает позицию ячейки
    public Vector3 GetCellPosition(int x, int y)
    {
        return new Vector3(x, y) * CellSize + GridPosition;
    }

    // В выходных параметрах возвращает индексы ячейки (X, Y) исходя из позиции
    public void GetCellIndex(Vector3 position, out int x, out int y)
    {
        x = Mathf.FloorToInt((position - GridPosition).x / CellSize);
        y = Mathf.FloorToInt((position - GridPosition).y / CellSize);
    }

    // Устанавливает значение клетки по индексу
    public void SetCellValue(int x, int y, TGridObject value)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            gridArray[x, y] = value;

            if (DebugMode) debugTextArray[x, y].text = value.ToString();

        }
    }


    // Устанавливает значение клетки по позиции
    public void SetCellValue(Vector3 cellPosition, TGridObject value)
    {
        GetCellIndex(cellPosition, out int x, out int y);

        SetCellValue(x, y, value);
    }


    // Получить значение ячейки по индексу
    public TGridObject GetCellValue(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default;
        } 
    }
    

    // Получить значеиие ячейки по позиции
    public TGridObject GetCellValue(Vector3 cellPosition)
    {
        GetCellIndex(cellPosition, out int x, out int y);

        return GetCellValue(x, y);
    }



    // Отобразить сетку
    private void DebugDrawGrid()
    {
        debugTextArray = new TextMesh[Width, Height];

        void DrawCell(int xPos, int yPos)
        {
            Debug.DrawLine(GetCellPosition(xPos, yPos), GetCellPosition(xPos + 1, yPos), Color.white, 1000f);
            Debug.DrawLine(GetCellPosition(xPos, yPos), GetCellPosition(xPos, yPos + 1), Color.white, 1000f);
        }

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                DrawCell(x, y);
                debugTextArray[x, y] = DebugTools.CreateText($"{x}, {y}", 0.1f, GetCellPosition(x, y) + Vector3.one * (CellSize / 2), Color.red, TextAnchor.MiddleCenter);
                

            }



        }

        Debug.DrawLine(GetCellPosition(Width, 0), GetCellPosition(Width, Height), Color.white, 100f);
        Debug.DrawLine(GetCellPosition(0, Height), GetCellPosition(Width, Height), Color.white, 100f);
    }


    

}
