using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "SolarOil/New Weapon", order = 0)]
public class SOWeapon : ScriptableObject
{
    [SerializeField] private string _weaponName;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _damage;
    [SerializeField] private int _magazineSize;
    [SerializeField, Range(5f, 25f)] private float _bulletSpeed;
    [SerializeField, Range(0.1f, 10f)] private float _shootDelay;
    [SerializeField, Range(1, 9)] private int _projectileCount;
    [SerializeField, Range(0.1f, 10f)] private float _rafaleDelay;
    [SerializeField, Range(1, 9)] private int _rafaleCount;
    [SerializeField, Range(0f, 360f)] private float _angle;
    [SerializeField, Range(1f, 10f)] private float _reloadTime;
    [SerializeField] private WeaponCadence _cadence;

    public string WeaponName {
        get { return _weaponName; }
    }

    public GameObject BulletPrefab {
        get { return _bulletPrefab; }
    }

    public int Damage {
        get { return _damage; }
    }

    public int MagazineSize {
        get { return _magazineSize; }
    }

    public float BulletSpeed {
        get { return _bulletSpeed; }
    }

    public float ShootDelay {
        get { return _shootDelay; }
    }

    public int ProjectileCount {
        get { return _projectileCount; }
    }

    public float RafaleDelay {
        get { return _rafaleDelay; }
    }

    public int RafaleCount {
        get { return _rafaleCount; }
    }

    public float Angle {
        get { return _angle; }
    }

    public float ReloadTime {
        get { return _reloadTime; }
    }

    public WeaponCadence Cadence {
        get { return _cadence; }
    }
}

public enum WeaponCadence
{
    None = -1,
    Single = 0,
    Rafale = 1,
    Auto = 2
}