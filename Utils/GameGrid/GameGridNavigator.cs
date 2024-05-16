using System.Collections.Generic;
using Alf.Utils;
using UnityEngine;

namespace Alf.GameGridSystem
{

public class GameGridNavigatorXY
{

    GameGrid _gameGrid;
    Vector2Int _currentCoords;

    public GameGridNavigatorXY(GameGridCell startCell)
    {
        _currentCoords = startCell.Coords;
        _gameGrid = startCell.transform.parent?.GetComponent<GameGrid>();
    }

    public GameObject Move(Vector2Int direction)
    {
        if(_gameGrid == null) return null;

        // var checkCoords = _currentCoords + Vector2Int.up;
        var checkCoords = _currentCoords + direction;
        
        // Check if coordinate exists
        if(_gameGrid.Cells.ContainsKey(checkCoords))
        {
            _currentCoords = checkCoords;
            return _gameGrid.Cells[checkCoords];
        }
        
        return _gameGrid.Cells[_currentCoords];

    }

}
}