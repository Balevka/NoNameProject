using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    
    private int columns;
    private int rows;

    private int obstacleCount;
    [SerializeField] private GameObject exit;
    [SerializeField] private GameObject[] floorTiles;
    [SerializeField] private GameObject[] obstacleTiles;
    [SerializeField] private GameObject[] wallTiles;
    [SerializeField] private GameObject[] enemyTiles;

    private Transform boardHolder;
    private List<Vector2> gridPositions = new List<Vector2>();
   

    private void InitialList()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector2(x, y));
            }
        }
    }

    private void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        rows = Random.Range(8, 15);
        columns = Random.Range(8, 15);
        obstacleCount = rows * columns / 9;
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];

                GameObject instance = Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }
    }
    private Vector2 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);

        Vector2 randomPosition = gridPositions[randomIndex];

        gridPositions.RemoveAt(randomIndex);

        return randomPosition;
    }

    private void LayoutObjectAtRandom(GameObject[] tiles, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector2 randomPosition = RandomPosition();
            GameObject tileChoice = tiles[Random.Range(0, tiles.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene(int level)
    {
        int enemyCount;
        enemyCount = (rows *columns) / 9;
        Debug.Log("enemies = " + enemyCount);

        BoardSetup();

        InitialList();

        LayoutObjectAtRandom(obstacleTiles, obstacleCount, obstacleCount);
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        Instantiate(exit, new Vector2(columns - 1, rows - 1), Quaternion.identity);

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
