using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGrid : MonoBehaviour
{
    public Camera camer;

    GridSystem<GridObject> grid;


    void Start()
    {
        // grid = new GridSystem<GridObject>(10, 10, 2f, transform.position, () => { return new GridObject(); }, true);
        


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.GetCellIndex(camer.ScreenToWorldPoint(Input.mousePosition), out int x, out int y);
            Debug.Log(x + ", " + y);

            grid.SetCellValue(x, y, new GridObject() { x = Random.Range(1, 10), y = Random.Range(1, 10) });
            
            
        }

        if (Input.GetMouseButtonDown(1))
        {
            grid.GetCellIndex(camer.ScreenToWorldPoint(Input.mousePosition), out int x, out int y);
            Debug.Log(x + ", " + y);
            Debug.Log(grid.GetCellValue(x, y).x + ", " + grid.GetCellValue(x, y).y);
        }
            
            
        
    }
}

public class GridObject
{
    public int x;
    public int y;
}
