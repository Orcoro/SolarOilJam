using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "SolarOil/New Weapon", order = 0)]
public class SOWeapon : ScriptableObject
{
    [SerializeField] private string _weaponName;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField, Range(5f, 25f)] private float _bulletSpeed;
    [SerializeField, Range(0.1f, 10f)] private float _shootDelay;
    [SerializeField, Range(1, 9)] private int _projectileCount;
    [SerializeField, Range(1, 9)] private int _rafaleCount;
    [SerializeField, Range(0f, 90f)] private float _angle;
    [SerializeField] private float _bulletLifeTime;
    [SerializeField] private WeaponCadence _cadence;

    public string WeaponName {
        get { return _weaponName; }
    }

    public GameObject BulletPrefab {
        get { return _bulletPrefab; }
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

    public int RafaleCount {
        get { return _rafaleCount; }
    }

    public float Angle {
        get { return _angle; }
    }

    public float BulletLifeTime {
        get { return _bulletLifeTime; }
    }

    public WeaponCadence Cadence {
        get { return _cadence; }
    }
}

public enum WeaponCadence
{
    Single = 0,
    Rafale = 1,
    Auto = 2
}