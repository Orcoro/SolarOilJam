using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private SOItem _item;
    private SpriteRenderer _background;
    private SpriteRenderer _icon;
    private float _defaultRadius = 1.5f;

    public float PickUpRange {
        get { return _defaultRadius * (1 + Player.Instance.Statistic.EntitiesStatistic.PickUpRange); }
    }

    private void Awake()
    {
        _icon = GetComponent<SpriteRenderer>();
        _background = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void Init(SOItem item)
    {
        Debug.Log($"Item: {item.name}");
        _item = item;
        _background.color = item.ItemColor;
        _icon.sprite = item.ItemSprite;
    }

    public bool IsOver(Vector3 position, float radius)
    {
        float itemRadius = _background.bounds.size.x / 2;
        float distance = Vector3.Distance(position, transform.position);

        return distance <= radius + itemRadius;
    }

    private void Update()
    {
        if (IsOver(Player.Instance.transform.position, PickUpRange)) {
            Player.Instance.PickUpItem(_item);
            GetAllstats();
            StatManager statManager = FindObjectOfType<StatManager>();
            statManager.UpdateStats(GetAllstats());
            Destroy(gameObject);
        }
    }

    private float[] GetAllstats()
    {
        float[] itemStats = new float[16];
        itemStats[0] += _item.WeaponStatistic.DamageMultiplier;
        //itemStats[1] += _item.WeaponStatistic.DamagePercent;
        itemStats[1] += _item.WeaponStatistic.AdditionnalMunition;
        itemStats[2] += _item.WeaponStatistic.BulletVelocity;
        //itemStats[4] += _item.WeaponStatistic.BulletVelocityPercent;
        itemStats[3] += _item.WeaponStatistic.ShootDelayMultiplier;
        //itemStats[6] += _item.WeaponStatistic.ShootDelayPercent;
        itemStats[4] += _item.WeaponStatistic.ProjectileBonus;
        itemStats[5] += _item.WeaponStatistic.RafaleDelay;
        //itemStats[9] += _item.WeaponStatistic.RafaleDelayPercent;
        itemStats[6] += _item.WeaponStatistic.RafaleProjectileBonus;
        itemStats[7] += _item.WeaponStatistic.SpreadAngle;
        //itemStats[12] += _item.WeaponStatistic.SpreadAnglePercent;
        itemStats[8] += _item.WeaponStatistic.ReloadTime;
        //itemStats[14] += _item.WeaponStatistic.ReloadTimePercent;
        itemStats[9] += _item.BulletStatistic.Damage;
        itemStats[10] += _item.BulletStatistic.Piercing;
        itemStats[11] += _item.EntitiesStatistic.Speed;
        itemStats[12] += _item.EntitiesStatistic.Health;
        itemStats[13] += _item.EntitiesStatistic.Armor;
        itemStats[14] += _item.EntitiesStatistic.Range;
        //itemStats[21] += _item.EntitiesStatistic.RangePercent;
        itemStats[15] += _item.EntitiesStatistic.PickUpRange;
        //itemStats[23] += _item.EntitiesStatistic.PickUpRangePercent;

        return itemStats;
    }
}