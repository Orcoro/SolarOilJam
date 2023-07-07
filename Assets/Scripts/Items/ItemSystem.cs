using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    private List<ItemElement> _items = new List<ItemElement>();
    private Statistic _statistics = new Statistic();

    public Statistic Statistics {
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

[System.Serializable]
public class Statistic
{
    [SerializeField] private WeaponStatistic _weaponStatistic;
    [SerializeField] private BulletStatistic _bulletStatistic;
    [SerializeField] private EntitiesStatistic _entitiesStatistic;

    public WeaponStatistic WeaponStatistic {
        get { return _weaponStatistic; }
        set { _weaponStatistic = value; }
    }

    public BulletStatistic BulletStatistic {
        get { return _bulletStatistic; }
        set { _bulletStatistic = value; }
    }

    public EntitiesStatistic EntitiesStatistic {
        get { return _entitiesStatistic; }
        set { _entitiesStatistic = value; }
    }

    public Statistic()
    {
        _weaponStatistic = new WeaponStatistic();
        _bulletStatistic = new BulletStatistic();
        _entitiesStatistic = new EntitiesStatistic();
    }

    public void AddStatistic(SOItem item)
    {
        _weaponStatistic.AddStatistic(item);
        _bulletStatistic.AddStatistic(item);
        _entitiesStatistic.AddStatistic(item);
    }

    public void RemoveStatistic(SOItem item)
    {
        _weaponStatistic.RemoveStatistic(item);
        _bulletStatistic.RemoveStatistic(item);
        _entitiesStatistic.RemoveStatistic(item);
    }

    //overload operator +
    public static Statistic operator +(Statistic a, Statistic b)
    {
        Statistic result = new Statistic();
        result.WeaponStatistic = a.WeaponStatistic + b.WeaponStatistic;
        result.BulletStatistic = a.BulletStatistic + b.BulletStatistic;
        result.EntitiesStatistic = a.EntitiesStatistic + b.EntitiesStatistic;
        return result;
    }
}
