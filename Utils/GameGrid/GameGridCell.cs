using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Alf.Utils;

namespace Alf.GameGridSystem
{

public class GameGridCellEvent
{
    public GameObject GameGridCell;
    public GameGridCellEventType EventType;
}

public enum GameGridCellEventType
{
    HOVER_ENTER,
    HOVER_EXIT,
    MOUSE_DOWN
}

public class GameGridCell : MonoBehaviour
{
    public Vector2Int Coords;

    public static GameObject Spawn(Vector2Int coords, Transform parent)
    {
        var gridCell = new GameObject("GridCell" + coords.x + coords.y, typeof(GameGridCell), typeof(BoxCollider2D));
        gridCell.GetComponent<GameGridCell>().Coords = coords;
        gridCell.transform.SetParent(parent, false);
        return gridCell;
    }

    public List<GameObject> GetGridCellObjects()
    {
        var gridCellObjects = new List<GameObject>();
        foreach(Transform child in transform)
        {
            if (child.gameObject.GetComponent<IGridCellObject>() != null)
            {
                gridCellObjects.Add(child.gameObject);
            }
        }
        return gridCellObjects;
    }

    public GameObject FindGridCellObject<T>()
    {
        var cellObjects = GetGridCellObjects();
        foreach (GameObject cellObject in cellObjects)
        {
            if(cellObject.GetComponent<T>() != null)
                return cellObject;
        }
        return null;
    }
    
    public T FindGetCellObject<T>()
    {
        var cellObjects = GetGridCellObjects();
        foreach (GameObject cellObject in cellObjects)
        {
            var ret = cellObject.GetComponent<T>();
            if(ret != null)
                return ret;
        }
        return default;
    }

    public List<GameObject> FindCellObjects<T>()
    {
        var cellObjects = GetGridCellObjects();
        var ret = new List<GameObject>();
        foreach (GameObject cellObject in cellObjects)
        {
            if(cellObject.GetComponent<T>() != null)
                ret.Add(cellObject);
        }
        return ret;
    }
    
    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        GameGridEvents.onGameGridCellEvent.Invoke(
            new GameGridCellEvent{
                GameGridCell = gameObject,
                EventType = GameGridCellEventType.HOVER_ENTER
            }
        );
    }

    void OnMouseExit()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        GameGridEvents.onGameGridCellEvent.Invoke(
            new GameGridCellEvent{
                GameGridCell = gameObject,
                EventType = GameGridCellEventType.HOVER_EXIT
            }
        );
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        GameGridEvents.onGameGridCellEvent.Invoke(
            new GameGridCellEvent{
                GameGridCell = gameObject,
                EventType = GameGridCellEventType.MOUSE_DOWN
            }
        );
    }
}

}
