using UnityEngine;
using System.Collections.Generic;

public class GridSettings : MonoBehaviour
{
    [SerializeField]
    private int width = 1;

    [SerializeField]
    private int height = 1;

    [SerializeField]
    private float cellSize =  1f;

    [SerializeField]
    private bool debugMode = false;

    [SerializeField]
    private Node[] nodes;
    
    void Start()
    {
        PathfindingSystem system = new PathfindingSystem(width, height, cellSize, transform.position, debugMode);
    }


}
