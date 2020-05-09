﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Generation : MonoBehaviour
{

    [SerializeField]
    private Tile groundTile;
    [SerializeField]
    private Tile pitTile;
    [SerializeField]
    private Tile topWallTile;
    [SerializeField]
    private Tile bottomWallTile;
    [SerializeField]
    private Tile leftWallTile;
    [SerializeField]
    private Tile rightWallTile;
    [SerializeField]
    private GameObject[] obstacleTiles;
    [SerializeField]
    private Tilemap groundMap;
    [SerializeField]
    private Tilemap pitMap;
    [SerializeField]
    private Tilemap wallMap;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject exit;
    [SerializeField]
    private int deviationRate = 10;
    [SerializeField]
    private int roomRate = 15;
    [SerializeField]
    private int obstacleRate =45;
    [SerializeField]
    private int enemyRate = 30;
    [SerializeField]
    private int maxRouteLength;
    [SerializeField]
    private int maxRoutes = 20;
    [SerializeField]
    private Text text;
    private int seed = 0;
    private int lastX;
    private int lastY;
    private List<Vector2> gridPositions = new List<Vector2>();


    // PathFinding
    [SerializeField]
    private Tile notWalk;
    private int countTiles = 0;
    private Vector3 startPos = Vector3.zero;
    private PathfindingSystem pathfinding;
    private List<Vector3Int> propPositionsList = new List<Vector3Int>();
    // PathFindig

    private int routeCount = 0;

    private void Start()
    {
        string datetime = System.DateTime.Now.ToString("MM/dd") + System.DateTime.Now.ToString("hh:mm:ss");
        string resultString = "";
        for (int i = 0; i < datetime.Length; i++)
        {
            if (datetime[i] >= '0' && datetime[i] <= '9')
                resultString += datetime[i];
        }
        Random.InitState(int.Parse(resultString));
        seed = Random.Range(0, 1000000000);
        Random.InitState(seed);
        int x = 0;
        int y = 0;
        int routeLength = 0;
        GenerateSquare(x, y, 1);
        Vector2Int previousPos = new Vector2Int(x, y);
        y += 3;
        GenerateSquare(x, y, 1);
        NewRoute(x, y, routeLength, previousPos);

        FillWalls();

        // Pathfinding
        pathfinding = new PathfindingSystem(pitMap.size.x, pitMap.size.y, 1f, startPos, false);

        foreach (Vector3Int pos in propPositionsList)
        {
            pathfinding.Grid.GetCellIndex(pos, out int xp, out int yp);
            pathfinding.GetNode(xp, yp).IsWalkable = false;
            //pitMap.SetTile(pos, notWalk);
        }
        // PathFinding

        Instantiate(exit, new Vector2(lastX + 0.5f, lastY + 0.5f), Quaternion.identity);
        player.transform.position = new Vector2(0.5f, 1f);
        text.text = "seed: " + seed;
    }

    private void FillWalls()
    {
        BoundsInt bounds = groundMap.cellBounds;
        for (int xMap = bounds.xMin - 11; xMap <= bounds.xMax + 10; xMap++)
        {
            for (int yMap = bounds.yMin - 11; yMap <= bounds.yMax + 10; yMap++)
            {
                Vector3Int pos = new Vector3Int(xMap, yMap, 0);

                if (startPos == Vector3.zero)
                {
                    startPos = pos;
                }

                Vector3Int posAbove = new Vector3Int(xMap, yMap + 1, 0);
                Vector3Int posBelow = new Vector3Int(xMap, yMap - 1, 0);
                Vector3Int posBefore = new Vector3Int(xMap - 1, yMap, 0);
                Vector3Int posAfter = new Vector3Int(xMap + 1, yMap, 0);
                TileBase tile = groundMap.GetTile(pos);
                TileBase tileBelow = groundMap.GetTile(posBelow);
                TileBase tileAbove = groundMap.GetTile(posAbove);
                TileBase tileBefore = groundMap.GetTile(posBefore);
                TileBase tileAfter = groundMap.GetTile(posAfter);
                if (tile == null)
                {
                    pitMap.SetTile(pos, pitTile);
                    propPositionsList.Add(pos);

                    if (tileBelow != null)
                    {
                        wallMap.SetTile(pos, topWallTile);
                    }
                    if (tileBefore != null)
                    {
                        wallMap.SetTile(pos, leftWallTile);
                    }
                    if (tileAfter != null)
                    {
                        wallMap.SetTile(pos, rightWallTile);
                    }
                    if (tileAbove != null)
                    {
                        wallMap.SetTile(pos, bottomWallTile);
                    }

                    

                }
            }
        }
    }

    private void NewRoute(int x, int y, int routeLength, Vector2Int previousPos)
    {
        if (routeCount < maxRoutes)
        {
            routeCount++;
            while (++routeLength < maxRouteLength)
            {
                //Initialize
                bool routeUsed = false;
                int xOffset = x - previousPos.x;
                int yOffset = y - previousPos.y;
                int roomSize = 1;
                if (Random.Range(1, 100) <= roomRate)
                    roomSize = Random.Range(3, 6);
                previousPos = new Vector2Int(x, y);

                //Go Straight
                if (Random.Range(1, 100) <= deviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x + xOffset, previousPos.y + yOffset, roomSize);
                        NewRoute(previousPos.x + xOffset, previousPos.y + yOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        x = previousPos.x + xOffset;
                        y = previousPos.y + yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                    lastX = previousPos.x + xOffset;
                    lastY = previousPos.y + yOffset;
                }

                //Go left
                if (Random.Range(1, 100) <= deviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x - yOffset, previousPos.y + xOffset, roomSize);
                        NewRoute(previousPos.x - yOffset, previousPos.y + xOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        y = previousPos.y + xOffset;
                        x = previousPos.x - yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }

                    lastY = previousPos.y + xOffset;
                    lastY = previousPos.x - yOffset;
                }
                //Go right
                if (Random.Range(1, 100) <= deviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x + yOffset, previousPos.y - xOffset, roomSize);
                        NewRoute(previousPos.x + yOffset, previousPos.y - xOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        y = previousPos.y - xOffset;
                        x = previousPos.x + yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                    lastY = previousPos.y - xOffset;
                    lastX = previousPos.x + yOffset;
                }

                if (!routeUsed)
                {
                    x = previousPos.x + xOffset;
                    y = previousPos.y + yOffset;
                    GenerateSquare(x, y, roomSize);
                    lastX = previousPos.x + xOffset;
                    lastY = previousPos.y + yOffset;
                }
            }
        }
    }

    private void GenerateSquare(int x, int y, int radius)
    {
        for (int tileX = x - radius; tileX <= x + radius; tileX++)
        {
            for (int tileY = y - radius; tileY <= y + radius; tileY++)
            {
                Vector3Int tilePos = new Vector3Int(tileX, tileY, 0);
                groundMap.SetTile(tilePos, groundTile);
            }
        }
        if (radius == 1)
        {
            if (Random.Range(0, 100) <= obstacleRate)
            {
                Instantiate(obstacleTiles[Random.Range(0, obstacleTiles.Length)],
                    new Vector2(Random.Range(x - radius, x + radius + 1) + 0.5f, Random.Range(y - radius, y + radius + 1) + 0.5f),
                    Quaternion.identity);
            }
            if (Random.Range(0, 100) <= enemyRate)
            {
                Instantiate(obstacleTiles[Random.Range(0, obstacleTiles.Length)],
                    new Vector2(Random.Range(x - radius, x + radius + 1) + 0.5f, Random.Range(y - radius, y + radius + 1) + 0.5f),
                    Quaternion.identity);
            }
        }
        else if (radius > 2)
        {
            for (int i = 0; i < Random.Range(1, radius + 3); i++)
            {
                Instantiate(obstacleTiles[Random.Range(0, obstacleTiles.Length)],
                    new Vector2(Random.Range(x - radius, x + radius + 1) + 0.5f, Random.Range(y - radius, y + radius + 1) + 0.5f),
                    Quaternion.identity);
            }

            for (int i = 0; i < Random.Range(3, radius); i++)
            {
                Instantiate(enemy,
                    new Vector2(Random.Range(x - radius, x + radius + 1) + 0.5f, Random.Range(y - radius, y + radius + 1) + 0.5f),
                    Quaternion.identity);
            }
        }
    }
}
