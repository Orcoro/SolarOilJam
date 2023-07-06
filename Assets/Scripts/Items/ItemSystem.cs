using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    private List<ItemElement> _items = new List<ItemElement>();
    private ItemStatistic _statistics;

    public ItemStatistic Statistics {
        get { return _statistics; }
    }

    public void AddItem(SOItem item)
    {
        ItemElement itemElement = _items.Find(x => x.Item == item);
        if (itemElement == null) {
            itemElement = new ItemElement();
            itemElement.Item = item;
            itemElement.Count++;
            _items.Add(itemElement);
        } else
            itemElement.Count++;
        _statistics.AddStatistic(item);
    }

    public void RemoveItem(SOItem item)
    {
        ItemElement itemElement = _items.Find(x => x.Item == item);
        if (itemElement != null) {
            itemElement.Count--;
            if (itemElement.Count <= 0)
                _items.Remove(itemElement);
            _statistics.RemoveStatistic(item);
        }
    }
}

[System.Serializable]
public class ItemElement
{
    private SOItem _item;
    private int _count;

    public SOItem Item {
        get { return _item; }
        set { _item = value; }
    }

    public int Count {
        get { return _count; }
        set { _count = value; }
    }
}
