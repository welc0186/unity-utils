using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Alf.GameGridSystem
{

public class GameGrid : MonoBehaviour
{
    private Dictionary<Vector2Int, GameObject> _cells;
    
    public Dictionary<Vector2Int, GameObject> Cells
        { 
            get
            { 
                if (_cells == null)
                    UpdateTiles();
                return _cells;
            } 
            set {}
        }

    public static GameObject Spawn(int width, int height)
    {
        var gameGrid = new GameObject("GameGrid", typeof(GameGrid));
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var coords = new Vector2Int(x, y);
                var gridCell = GameGridCell.Spawn(coords, gameGrid.transform);
                gridCell.transform.position = new Vector3(coords.x, coords.y, 0);
            }
        }
        return gameGrid;
    }

    void Start()
    {
        UpdateTiles();
    }

    void UpdateTiles()
    {
        _cells = new Dictionary<Vector2Int, GameObject>();
        foreach(Transform child in transform)
        {
            GameGridCell gridCell;
            child.gameObject.TryGetComponent<GameGridCell>(out gridCell);
            if(gridCell == null)
                continue;
            _cells.Add(gridCell.Coords, child.gameObject);
        }
    }

    public List<GameObject> FindAllCellObjects<T>()
    {
        var ret = new List<GameObject>();
        if(_cells == null)
            return ret;

        foreach(KeyValuePair<Vector2Int,GameObject> entry in _cells)
        {
            var gridCell = entry.Value.GetComponent<GameGridCell>();
            if (gridCell == null)
                continue;
            ret.AddRange(gridCell.FindCellObjects<T>());
        }

        return ret;
    }

    public GameObject AddCell(Vector2Int coords)
    {
        if (Cells.ContainsKey(coords))
            return null;
        var gridCell = GameGridCell.Spawn(coords, transform);
        gridCell.transform.localPosition = new Vector3(coords.x, coords.y, 0);
        _cells.Add(coords, gridCell);
        return gridCell;
    }

}
}
