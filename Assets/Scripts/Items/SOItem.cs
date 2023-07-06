using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "NewItem", menuName = "SolarOil/New Item", order = 1)]
public class SOItem : ScriptableObject
{
    [SerializeField] private string _itemName;
    [SerializeField, TextArea] private string _itemDescription;
    [SerializeField, ShowAssetPreview] private Sprite _itemSprite;
    [SerializeField] private Quality _itemQuality;
    [SerializeField] private ItemStatistic _itemStatistic;

    public string ItemName {
        get { return _itemName; }
    }

    public string ItemDescription {
        get { return _itemDescription; }
    }

    public Sprite ItemSprite {
        get { return _itemSprite; }
    }

    public Quality ItemQuality {
        get { return _itemQuality; }
    }

    public ItemStatistic ItemStatistic {
        get { return _itemStatistic; }
    }
}

[System.Serializable]
public class ItemStatistic
{
    [Header ("Weapon Modifier")]
    [Tooltip ("This variable increase the base damage of the weapon.")]
    [SerializeField] private int _flatDamage;
    [Tooltip ("This variable is a multiplier for the damage of the weapon. (must be write between 0 and 1)")]
    [SerializeField] private float _damageMultiplier;
    [Tooltip ("This variable increase the Magazine Size of the weapon.")]
    [SerializeField] private int _additionnalMunition;
    [Tooltip ("This variable increase the bullet speed of the weapon.")]
    [SerializeField] private float _bulletVelocity;
    [Tooltip ("This variable increase the shoot delay of the weapon. (must be write between 0 and 1)")]
    [SerializeField] private float _shootDelayMultiplier;
    [Tooltip ("This variable increase the projectile of the weapon.")]
    [SerializeField] private int _projectileBonus;
    [Tooltip ("This variable increase the rafale delay of the weapon. (must be write between 0 and 1)")]
    [SerializeField] private float _rafaleDelay;
    [Tooltip ("This variable increase the rafale count of the weapon.")]
    [SerializeField] private int _rafaleProjectileBonus;
    [Tooltip ("This variable increase the spread angle of the weapon. (must be write between 0 and 1)")]
    [SerializeField] private float _spreadAngle;
    [Tooltip ("This variable increase the reload time of the weapon. (must be write between 0 and 1)")]
    [SerializeField] private float _reloadTime;

    //[Header ("Bullet Modifier")]
    //[Tooltip ("This variable increase the number of target hit by the bullet.")]
    //[SerializeField] private int _piercing;

    //[Header ("Player Modifier")]
    //[Tooltip ("This variable increase the player speed.")]
    //[SerializeField] private float _speed;
    //[Tooltip ("This variable increase the player health.")]
    //[SerializeField] private int _health;

    public int FlatDamage {
        get { return _flatDamage; }
        set { _flatDamage = value; }
    }

    public float DamageMultiplier {
        get { return _damageMultiplier; }
        set { _damageMultiplier = value; }
    }

    public int AdditionnalMunition {
        get { return _additionnalMunition; }
        set { _additionnalMunition = value; }
    }

    public float BulletVelocity {
        get { return _bulletVelocity; }
        set { _bulletVelocity = value; }
    }

    public float ShootDelayMultiplier {
        get { return _shootDelayMultiplier; }
        set { _shootDelayMultiplier = value; }
    }

    public int ProjectileBonus {
        get { return _projectileBonus; }
        set { _projectileBonus = value; }
    }

    public float RafaleDelay {
        get { return _rafaleDelay; }
        set { _rafaleDelay = value; }
    }

    public int RafaleProjectileBonus {
        get { return _rafaleProjectileBonus; }
        set { _rafaleProjectileBonus = value; }
    }

    public float SpreadAngle {
        get { return _spreadAngle; }
        set { _spreadAngle = value; }
    }

    public float ReloadTime {
        get { return _reloadTime; }
        set { _reloadTime = value; }
    }

    public void AddStatistic(SOItem item)
    {
        this._flatDamage += item.ItemStatistic.FlatDamage;
        this._damageMultiplier += item.ItemStatistic.DamageMultiplier;
        this._additionnalMunition += item.ItemStatistic.AdditionnalMunition;
        this._bulletVelocity += item.ItemStatistic.BulletVelocity;
        this._shootDelayMultiplier += item.ItemStatistic.ShootDelayMultiplier;
        this._projectileBonus += item.ItemStatistic.ProjectileBonus;
        this._rafaleDelay += item.ItemStatistic.RafaleDelay;
        this._rafaleProjectileBonus += item.ItemStatistic.RafaleProjectileBonus;
        this._spreadAngle += item.ItemStatistic.SpreadAngle;
        this._reloadTime += item.ItemStatistic.ReloadTime;
    }

    public void RemoveStatistic(SOItem item)
    {
        this._flatDamage -= item.ItemStatistic.FlatDamage;
        this._damageMultiplier -= item.ItemStatistic.DamageMultiplier;
        this._additionnalMunition -= item.ItemStatistic.AdditionnalMunition;
        this._bulletVelocity -= item.ItemStatistic.BulletVelocity;
        this._shootDelayMultiplier -= item.ItemStatistic.ShootDelayMultiplier;
        this._projectileBonus -= item.ItemStatistic.ProjectileBonus;
        this._rafaleDelay -= item.ItemStatistic.RafaleDelay;
        this._rafaleProjectileBonus -= item.ItemStatistic.RafaleProjectileBonus;
        this._spreadAngle -= item.ItemStatistic.SpreadAngle;
        this._reloadTime -= item.ItemStatistic.ReloadTime;
    }
}

public enum Quality
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
// 49, 30, 15, 5, 1