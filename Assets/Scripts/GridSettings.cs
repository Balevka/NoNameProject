using UnityEngine;

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
    
    void Start()
    {
        PathfindingSystem system = new PathfindingSystem(width, height, cellSize, transform.position, debugMode);
    }


}
