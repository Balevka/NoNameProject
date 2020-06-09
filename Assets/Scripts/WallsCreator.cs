using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class WallsCreator : MonoBehaviour
{
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
    private List<Vector3Int> propPositionsList = new List<Vector3Int>();
    private Vector3 startPos = Vector3.zero;

    public List<Vector3Int> PropPositionsList { get => propPositionsList;}

    public void FillWalls()
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
}
