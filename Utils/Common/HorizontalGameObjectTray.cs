using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alf.Utils
{
// Holds moveable UI game objects horizontally on a tray, e.g. a card hand or Scrabble tray
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(HorizontalLayoutGroup))]
public class HorizontalGameObjectTray : MonoBehaviour
{

    [SerializeField] private Rect slotSize;

    private GameObject _movingObject;
    private List<GameObject> _traySlots;
    private bool _isCrossing = false;

    void Awake()
    {
        _traySlots = new List<GameObject>();
    }

    public void AddObject(GameObject newObject, ref Action objectBeginMove, ref Action objectEndMove)
    {
        AddSlot(newObject);
        objectBeginMove += () => _movingObject = newObject;
        objectEndMove += () => _movingObject = null;
    }

    public void Clear()
    {
        foreach(var slot in _traySlots)
        {
            Destroy(slot.gameObject);
        }
        _traySlots = new List<GameObject>();
    }

    private void AddSlot(GameObject childObject)
    {
        var newSlot = new GameObject("TraySlot", typeof(RectTransform));
        newSlot.transform.SetParent(transform, false);
        newSlot.GetComponent<RectTransform>().sizeDelta = new Vector2(slotSize.width, slotSize.height);
        newSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(slotSize.x, slotSize.y);
        childObject.transform.SetParent(newSlot.transform);
        _traySlots.Add(newSlot);
    }

    void Update()
    {

        if (_movingObject == null)
            return;

        if (_isCrossing)
            return;

        for (int i = 0; i < _traySlots.Count; i++)
        {
            if (_movingObject.transform.position.x > _traySlots[i].transform.position.x
                && _traySlots.Contains(_movingObject.transform.parent.gameObject)
                && _traySlots.IndexOf(_movingObject.transform.parent.gameObject) < i)
            {
                Swap(i);
                break;
            }

            if (_movingObject.transform.position.x < _traySlots[i].transform.position.x
                && _traySlots.Contains(_movingObject.transform.parent.gameObject)
                && _traySlots.IndexOf(_movingObject.transform.parent.gameObject) > i)
            {
                Swap(i);
                break;
            }
        }
    }

    public void RemoveEmptySlots()
    {
        var i = 0;
        while (true)
        {
            if (i >= _traySlots.Count)
                break;
            if (_traySlots[i].transform.childCount < 1)
            {
                RemoveSlot(_traySlots[i]);
                continue;
            }
            i++;
        }
    }

    public void RemoveSlot(GameObject traySlot)
    {
        if(!_traySlots.Contains(traySlot))
            return;
        _traySlots.Remove(traySlot);
        Destroy(traySlot);
    }

    void Swap(int index)
    {
        if (_movingObject == null)
            return;
        
        _isCrossing = true;

        var focusedParent = _movingObject.transform.parent;
        var crossedParent = _traySlots[index].transform;

        if(crossedParent.childCount == 0)
            return;

        var crossedObject = crossedParent.GetChild(0);
        crossedObject.transform.SetParent(focusedParent);
        _movingObject.transform.SetParent(crossedParent);

        _isCrossing = false;
    }

}
}