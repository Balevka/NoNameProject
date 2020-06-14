﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Generation : MonoBehaviour
{
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private Tile groundTile;
    [SerializeField]
    private Tile pitTile;
    [SerializeField]
    private Tile[] topWallTile;
    [SerializeField]
    private Tile bottomWallTile;
    [SerializeField]
    private Tile leftWallTile;
    [SerializeField]
    private Tile rightWallTile;
    [SerializeField]
    private Tile WallTile;
    [SerializeField]
    private Tile rightBtTile;
    [SerializeField]
    private Tile rightTpTile;
    [SerializeField]
    private Tile leftBtTile;
    [SerializeField]
    private Tile leftTpTile;
    [SerializeField]
    private Tile dotTRTile;
    [SerializeField]
    private Tile dotTLTile;
    [SerializeField]
    private Tile dotBRTile;
    [SerializeField]
    private Tile dotBLTile;
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
    private int obstacleRate = 45;
    [SerializeField]
    private int enemyRate = 30;
    [SerializeField]
    private int maxRouteLength;
    [SerializeField]
    private int maxRoutes = 20;
    [SerializeField]
    private Text text;
    private int seed;
    private int lastX;
    private int lastY;
    private int lastRoomObstacles = 0;
    private int lastRoomSize = 0;
    private List<Vector2> gridPositions = new List<Vector2>();
    private Vector2 exitPos;
    public List<Vector2> enemyList = new List<Vector2>();
    private int level = 0;


    // PathFinding
    [SerializeField]
    private GameObject notWalk;
    private int countTiles = 0;
    private Vector3 startPos = Vector3.zero;
    private PathfindingSystem pathfinding;
    private List<Vector3Int> propPositionsList = new List<Vector3Int>();
    public List<Vector2> obstacleList = new List<Vector2>();
    // PathFindig
    public int routeCount = 0;

    public int Seed { get => seed;}


    private void Start()
    {
        CreateSeed();
        if(PlayerPrefs.HasKey("seed"))
        {
            if (PlayerPrefs.GetInt("seed") > 0)
                seed = PlayerPrefs.GetInt("seed");
        }
        Random.InitState(seed);
        maxRoutes = Random.Range(30, 50);
        int x = 0;
        int y = 0;
        int routeLength = 0;
        GenerateSquare(x, y, 1);
        
        Vector2Int previousPos = new Vector2Int(x, y);
        y += 3;
        GenerateSquare(x, y, 1);
        NewRoute(x, y, routeLength, previousPos);
        FillWalls();
        PathFinding();

        exitPos = new Vector2(lastX + 0.5f, lastY + 0.5f);
        while (obstacleList.Contains(exitPos))
        {
            exitPos = new Vector2(Random.Range(lastX - lastRoomSize, lastX + lastRoomSize + 1) + 0.5f, Random.Range(lastY - lastRoomSize, lastY + lastRoomSize + 1) + 0.5f);
        }
        exit.transform.position = exitPos;
        player.transform.position = new Vector2(0.5f, 1f);
        text.text = "seed: " + seed + " maxRoutes = " + maxRoutes + " " + level;
        PlayerPrefs.SetInt("seed", 0);
        PlayerPrefs.SetInt("loadedSeed", 0);
    }
  
    private void CreateSeed()
    {
        string datetime = System.DateTime.Now.ToString("MM/dd") + System.DateTime.Now.ToString("hh:mm:ss");
        string resultString = "";
        for (int i = 0; i < datetime.Length; i++)
        {
            if (datetime[i] >= '0' && datetime[i] <= '9')
                resultString += datetime[i];
        }
        Random.InitState(int.Parse(resultString));
        seed = Random.Range(10000, 999999999);
    }

    private void PathFinding()
    {
        pathfinding = new PathfindingSystem(pitMap.size.x, pitMap.size.y, 1f, startPos, false);

        foreach (Vector3Int pos in propPositionsList)
        {
            pathfinding.Grid.GetCellIndex(pos, out int xp, out int yp);
            pathfinding.GetNode(xp, yp).IsWalkable = false;
        }

        foreach (Vector2 pos in obstacleList)
        {
            pathfinding.Grid.GetCellIndex(pos, out int xo, out int yo);
            pathfinding.GetNode(xo, yo).IsWalkable = false;
        }
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
                Vector3Int dotTL = new Vector3Int(xMap + 1, yMap - 1, 0);
                Vector3Int dotTR = new Vector3Int(xMap - 1, yMap - 1, 0);
                Vector3Int dotBL = new Vector3Int(xMap + 1, yMap + 1, 0);
                Vector3Int dotBR = new Vector3Int(xMap - 1, yMap + 1, 0);
                TileBase tile = groundMap.GetTile(pos);
                TileBase tileBelow = groundMap.GetTile(posBelow);
                TileBase tileAbove = groundMap.GetTile(posAbove);
                TileBase tileBefore = groundMap.GetTile(posBefore);
                TileBase tileAfter = groundMap.GetTile(posAfter);
                TileBase tileTL = groundMap.GetTile(dotTL);
                TileBase tileTR = groundMap.GetTile(dotTR);
                TileBase tileBR = groundMap.GetTile(dotBR);
                TileBase tileBL = groundMap.GetTile(dotBL);
                if (tile == null)
                {
                    pitMap.SetTile(pos, pitTile);
                    propPositionsList.Add(pos);


                    if (tileTL != null)
                    {
                        wallMap.SetTile(pos, dotTLTile);
                    }
                    if (tileTR != null)
                    {
                        wallMap.SetTile(pos, dotTRTile);
                    }
                    if (tileBR != null)
                    {
                        wallMap.SetTile(pos, dotBRTile);
                    }
                    if (tileBL != null)
                    {
                        wallMap.SetTile(pos, dotBLTile);
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
                    if (tileAbove != null && tileBefore != null)
                    {
                        wallMap.SetTile(pos, rightBtTile);
                    }
                    if (tileAbove != null && tileAfter != null)
                    {
                        wallMap.SetTile(pos, leftBtTile);
                    }
                    if (tileBelow != null && tileBefore != null)
                    {
                        wallMap.SetTile(pos, leftTpTile);
                    }
                    if (tileBelow != null && tileAfter != null)
                    {
                        wallMap.SetTile(pos, rightTpTile);
                    }
                    if ((tileBefore != null && tileAfter != null) || (tileAbove != null && tileBelow != null))
                    {
                        wallMap.SetTile(pos, WallTile);
                    }
                    if (tileBelow != null)
                    {
                        wallMap.SetTile(pos, topWallTile[Random.Range(0, topWallTile.Length)]);
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
                }

                if (!routeUsed)
                {
                    x = previousPos.x + xOffset;
                    y = previousPos.y + yOffset;
                    GenerateSquare(x, y, roomSize);
                }
            }
        }
    }
    public void Save()
    {
        PlayerPrefs.SetInt("savedSeed", seed);
        Debug.Log("Saved, seed: " + PlayerPrefs.GetInt("savedSeed"));
    }

    private void GenerateSquare(int x, int y, int radius)
    {
        lastRoomSize = radius;
        Vector3 obstaclePos;
        Vector3 enemyPos;
        lastX = x;
        lastY = y;

        for (int tileX = x - radius; tileX <= x + radius; tileX++)
        {
            for (int tileY = y - radius; tileY <= y + radius; tileY++)
            {
                Vector3Int tilePos = new Vector3Int(tileX, tileY, 0);
                groundMap.SetTile(tilePos, groundTile);
            }
        }

        switch(radius)
        {
            case 1:
                if (Random.Range(0, 100) <= obstacleRate)
                {
                    obstaclePos = new Vector2(Random.Range(x - radius, x + radius + 1) + 0.5f, Random.Range(y - radius, y + radius + 1) + 0.5f);
                    Instantiate(obstacleTiles[Random.Range(0, obstacleTiles.Length)], obstaclePos, Quaternion.identity);

                    obstacleList.Add(obstaclePos);
                }

                if (Random.Range(0, 100) <= enemyRate)
                {
                    enemyPos = new Vector2(Random.Range(x - radius, x + radius + 1) + 0.5f, Random.Range(y - radius, y + radius + 1) + 0.5f);
                    while (obstacleList.Contains(enemyPos) || enemyList.Contains(enemyPos))
                    {
                        enemyPos = new Vector2(Random.Range(x - radius, x + radius + 1) + 0.5f, Random.Range(y - radius, y + radius + 1) + 0.5f);
                    }
                    enemyList.Add(enemyPos);
                    var Enemy = Instantiate(enemy, enemyPos, Quaternion.identity);
                    Enemy.GetComponent<Enemy>().target = player.transform;
                }
                break;

             default:
                for (int i = 0; i < Random.Range(3, radius); i++)
                {
                    obstaclePos = new Vector2(Random.Range(x - radius, x + radius + 1) + 0.5f, Random.Range(y - radius, y + radius + 1) + 0.5f);
                    while (obstacleList.Contains(obstaclePos) || enemyList.Contains(obstaclePos))
                    {
                        obstaclePos = new Vector2(Random.Range(x - radius, x + radius + 1) + 0.5f, Random.Range(y - radius, y + radius + 1) + 0.5f);
                    }
                    var obstacle = Instantiate(obstacleTiles[Random.Range(0, obstacleTiles.Length)], obstaclePos, Quaternion.identity);
                    obstacle.GetComponent<ObstacleScript>().grid = grid;
                    obstacleList.Add(obstaclePos);
                }

                for (int i = 0; i < Random.Range(1, radius - 1); i++)
                {
                    enemyPos = new Vector2(Random.Range(x - radius, x + radius + 1) + 0.5f, Random.Range(y - radius, y + radius + 1) + 0.5f);
                    while (obstacleList.Contains(enemyPos) || enemyList.Contains(enemyPos))
                    {
                        enemyPos = new Vector2(Random.Range(x - radius, x + radius + 1) + 0.5f, Random.Range(y - radius, y + radius + 1) + 0.5f);
                    }
                    enemyList.Add(enemyPos);
                    var Enemy = Instantiate(enemy, enemyPos, Quaternion.identity);
                    Enemy.GetComponent<Enemy>().target = player.transform;
                }
                break;

        } 
    }
}   