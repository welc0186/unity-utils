using UnityEngine;
using Alf.Utils;

namespace Alf.GameGridSystem
{

public class GameGridHighlight : MonoBehaviour, IGridCellObject
{
    public GameObject GridCell { get; private set; }
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        GridCell = transform.parent.gameObject;
        GameGridEvents.onGameGridCellEvent.Subscribe(OnGridObjectEvent);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    void OnDestroy()
    {
        GameGridEvents.onGameGridCellEvent.Unsubscribe(OnGridObjectEvent);
    }

    private void OnGridObjectEvent(GameGridCellEvent @event)
    {
        var clickedGridObject = @event.GameGridCell;
        if(clickedGridObject == null || clickedGridObject != GridCell)
            return;
        
        var tileEvent = @event.EventType;

        switch (tileEvent)
        {
            case GameGridCellEventType.HOVER_ENTER:
                GameGridEvents.onGameGridHighlight.Invoke(gameObject);
                _spriteRenderer.enabled = true;
                break;
            case GameGridCellEventType.HOVER_EXIT:
                _spriteRenderer.enabled = false;
                break;
        }

    }
}
}
