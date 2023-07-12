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
    [Header ("Weapon Modifier")]
    [SerializeField] private WeaponStatistic _weaponStatistic;
    [Header ("Bullet Modifier")]
    [SerializeField] private BulletStatistic _bulletStatistic;
    [Header ("Entities Modifier")]
    [SerializeField] private EntitiesStatistic _entitiesStatistic;

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

    public Color ItemColor {
        get {
            switch (_itemQuality)
            {
                case(Quality.Common):
                    return Color.white;
                case(Quality.Uncommon):
                    return Color.green;
                case(Quality.Rare):
                    return Color.blue;
                case(Quality.Epic):
                    return new Color(0.6f, 0f, 0.9f);
                case(Quality.Legendary):
                    return new Color(1f, 0.58f, 0f);
                default:
                    return Color.white;
            }
        }
    }

    public WeaponStatistic WeaponStatistic {
        get { return _weaponStatistic; }
    }

    public BulletStatistic BulletStatistic {
        get { return _bulletStatistic; }
    }

    public EntitiesStatistic EntitiesStatistic {
        get { return _entitiesStatistic; }
    }
}

[System.Serializable]
public class WeaponStatistic
{
    [Tooltip ("This variable is a multiplier for the damage of the weapon. (must be write between 0 and 1)")]
    [SerializeField] private float _damageMultiplier;
    [Tooltip ("This variable increase the Magazine Size of the weapon.")]
    [SerializeField] private int _additionnalMunition;
    [Tooltip ("This variable increase the bullet speed of the weapon. (must be write between 0 and 1)")]
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

    public float DamageMultiplier {
        get { return _damageMultiplier; }
        set { _damageMultiplier = value; }
    }

    public float DamagePercent {
        get { return (1f + _damageMultiplier) * 100f;}
    }

    public int AdditionnalMunition {
        get { return _additionnalMunition; }
        set { _additionnalMunition = value; }
    }

    public float BulletVelocity {
        get { return _bulletVelocity; }
        set { _bulletVelocity = value; }
    }

    public float BulletVelocityPercent {
        get { return (1f + _bulletVelocity) * 100f;}
    }

    public float ShootDelayMultiplier {
        get { return _shootDelayMultiplier; }
        set { _shootDelayMultiplier = value; }
    }

    public float ShootDelayPercent {
        get { return (1f + _shootDelayMultiplier) * 100f;}
    }

    public int ProjectileBonus {
        get { return _projectileBonus; }
        set { _projectileBonus = value; }
    }

    public float RafaleDelay {
        get { return _rafaleDelay; }
        set { _rafaleDelay = value; }
    }

    public float RafaleDelayPercent {
        get { return (1f + _rafaleDelay) * 100f;}
    }

    public int RafaleProjectileBonus {
        get { return _rafaleProjectileBonus; }
        set { _rafaleProjectileBonus = value; }
    }

    public float SpreadAngle {
        get { return _spreadAngle; }
        set { _spreadAngle = value; }
    }

    public float SpreadAnglePercent {
        get { return (1f + _spreadAngle) * 100f;}
    }

    public float ReloadTime {
        get { return _reloadTime; }
        set { _reloadTime = value; }
    }

    public float ReloadTimePercent {
        get { return (1f + _reloadTime) * 100f;}
    }

    public WeaponStatistic()
    {
        _damageMultiplier = 0;
        _additionnalMunition = 0;
        _bulletVelocity = 0;
        _shootDelayMultiplier = 0;
        _projectileBonus = 0;
        _rafaleDelay = 0;
        _rafaleProjectileBonus = 0;
        _spreadAngle = 0;
        _reloadTime = 0;
    }

    public WeaponStatistic(WeaponStatistic a, WeaponStatistic b)
    {
        _damageMultiplier = a.DamageMultiplier + b.DamageMultiplier;
        _additionnalMunition = a.AdditionnalMunition + b.AdditionnalMunition;
        _bulletVelocity = a.BulletVelocity + b.BulletVelocity;
        _shootDelayMultiplier = a.ShootDelayMultiplier + b.ShootDelayMultiplier;
        _projectileBonus = a.ProjectileBonus + b.ProjectileBonus;
        _rafaleDelay = a.RafaleDelay + b.RafaleDelay;
        _rafaleProjectileBonus = a.RafaleProjectileBonus + b.RafaleProjectileBonus;
        _spreadAngle = a.SpreadAngle + b.SpreadAngle;
        _reloadTime = a.ReloadTime + b.ReloadTime;
    }

    public void AddStatistic(SOItem item)
    {
        _damageMultiplier += item.WeaponStatistic.DamageMultiplier;
        _additionnalMunition += item.WeaponStatistic.AdditionnalMunition;
        _bulletVelocity += item.WeaponStatistic.BulletVelocity;
        _shootDelayMultiplier += item.WeaponStatistic.ShootDelayMultiplier;
        _projectileBonus += item.WeaponStatistic.ProjectileBonus;
        _rafaleDelay += item.WeaponStatistic.RafaleDelay;
        _rafaleProjectileBonus += item.WeaponStatistic.RafaleProjectileBonus;
        _spreadAngle += item.WeaponStatistic.SpreadAngle;
        _reloadTime += item.WeaponStatistic.ReloadTime;
    }

    public void RemoveStatistic(SOItem item)
    {
        _damageMultiplier -= item.WeaponStatistic.DamageMultiplier;
        _additionnalMunition -= item.WeaponStatistic.AdditionnalMunition;
        _bulletVelocity -= item.WeaponStatistic.BulletVelocity;
        _shootDelayMultiplier -= item.WeaponStatistic.ShootDelayMultiplier;
        _projectileBonus -= item.WeaponStatistic.ProjectileBonus;
        _rafaleDelay -= item.WeaponStatistic.RafaleDelay;
        _rafaleProjectileBonus -= item.WeaponStatistic.RafaleProjectileBonus;
        _spreadAngle -= item.WeaponStatistic.SpreadAngle;
        _reloadTime -= item.WeaponStatistic.ReloadTime;
    }

    //overload operator +
    public static WeaponStatistic operator +(WeaponStatistic a, WeaponStatistic b)
    {
        WeaponStatistic result = new WeaponStatistic(a, b);
        return result;
    }
}

[System.Serializable]
public class BulletStatistic
{
    [Tooltip ("This variable increase the damage of the bullet.")]
    [SerializeField] private int _damage;
    [Tooltip ("This variable increase the number of enemies the bullet can hit.")]
    [SerializeField] private int _piercing;

    public int Damage {
        get { return _damage; }
    }

    public int Piercing {
        get { return _piercing; }
    }

    public BulletStatistic()
    {
        _damage = 0;
        _piercing = 0;
    }

    public BulletStatistic(BulletStatistic a, BulletStatistic b)
    {
        _damage = a.Damage + b.Damage;
        _piercing = a.Piercing + b.Piercing;
    }

    public void AddStatistic(SOItem item)
    {
        _damage += item.BulletStatistic.Damage;
        _piercing += item.BulletStatistic.Piercing;
    }

    public void RemoveStatistic(SOItem item)
    {
        _damage -= item.BulletStatistic.Damage;
        _piercing -= item.BulletStatistic.Piercing;
    }

    //overload operator +
    public static BulletStatistic operator +(BulletStatistic a, BulletStatistic b)
    {
        BulletStatistic result = new BulletStatistic(a, b);
        return result;
    }
}

[System.Serializable]
public class EntitiesStatistic
{
    [Tooltip ("This variable increase the player speed. (must be write between 0 and 1)")]
    [SerializeField] private float _speed;
    [Tooltip ("This variable increase the player health.")]
    [SerializeField] private int _health;
    [Tooltip ("This variable increase the player Armor and reduce Damage.")]
    [SerializeField] private int _armor;
    [Tooltip ("This variable increase the Range of the player. (must be write between 0 and 1)")]
    [SerializeField] private float _range;
    [Tooltip ("This variable increase PickUp Range. (must be write between 0 and 1)")]
    [SerializeField] private float _pickUpRange;

    public float Speed {
        get { return _speed; }
    }

    public int Health {
        get { return _health; }
    }

    public int Armor {
        get { return _armor; }
    }

    public float Range {
        get { return _range; }
    }
    
    public float RangePercent {
        get { return (1f + _range) * 100f;}
    }

    public float PickUpRange {
        get { return _pickUpRange; }
    }

    public float PickUpRangePercent {
        get { return (1f + _pickUpRange) * 100f;}
    }

    public EntitiesStatistic()
    {
        _speed = 0;
        _health = 0;
        _armor = 0;
        _range = 0;
        _pickUpRange = 0;
    }

    public EntitiesStatistic(EntitiesStatistic a, EntitiesStatistic b)
    {
        _speed = a.Speed + b.Speed;
        _health = a.Health + b.Health;
        _armor = a.Armor + b.Armor;
        _range = a.Range + b.Range;
        _pickUpRange = a.PickUpRange + b.PickUpRange;
    }

    public void AddStatistic(SOItem item)
    {
        _speed += item.EntitiesStatistic.Speed;
        _health += item.EntitiesStatistic.Health;
        _armor += item.EntitiesStatistic.Armor;
        _range += item.EntitiesStatistic.Range;
        _pickUpRange += item.EntitiesStatistic.PickUpRange;
    }

    public void RemoveStatistic(SOItem item)
    {
        _speed -= item.EntitiesStatistic.Speed;
        _health -= item.EntitiesStatistic.Health;
        _armor -= item.EntitiesStatistic.Armor;
        _range -= item.EntitiesStatistic.Range;
        _pickUpRange -= item.EntitiesStatistic.PickUpRange;
    }

    //overload operator +
    public static EntitiesStatistic operator +(EntitiesStatistic a, EntitiesStatistic b)
    {
        EntitiesStatistic result = new EntitiesStatistic(a, b);
        return result;
    }
}


public enum Quality
{
    None,
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
// 49, 30, 15, 5, 1