using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Shoot : MonoBehaviour
{
    [SerializeField] private SOWeapon _weapon;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _magazineSize = 10;
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private float _shootDelay = 0.5f;
    [SerializeField] private int _projectileCount = 1;
    [SerializeField] private float _rafaleDelay = 0.5f;
    [SerializeField] private int _rafaleCount = 3;
    [SerializeField] private float _angle = 20f;
    [SerializeField] private WeaponCadence _cadence = WeaponCadence.Single;
    private float _bulletLifeTime = 15f;
    private float _lastShootTime = 0f;
    private bool _hold = false;
    private FlagCoroutine _shootCoroutine;
    private FlagCoroutine _reloadCoroutine;
    private ItemStatistic _itemStatistics;

    public bool Hold {
        get { return _hold; }
        set { _hold = value; }
    }

    public int MaxMagazineSize {
        get { return _weapon.MagazineSize + _itemStatistics.AdditionnalMunition; }
    }

    public float BulletSpeed {
        get { return _weapon.BulletSpeed * (1 + _itemStatistics.BulletVelocity); }
    }

    public float ShootDelay {
        get { Debug.Log($"ShootDelay: {_shootDelay * (1 + _itemStatistics.ShootDelayMultiplier)} _shootdelay {_shootDelay} _itemStatistics.ShootDelayMultiplier {_itemStatistics.ShootDelayMultiplier}");
            return _shootDelay * (1 + _itemStatistics.ShootDelayMultiplier); }
    }

    public int ProjectileCount {
        get { return _weapon.ProjectileCount + _itemStatistics.ProjectileBonus; }
    }

    public float RafaleDelay {
        get { return _rafaleDelay * (1 + _itemStatistics.RafaleDelay); }
    }

    public int RafaleProjectile {
        get { return _weapon.RafaleCount + _itemStatistics.RafaleProjectileBonus; }
    }

    public float Angle {
        get { return _weapon.Angle * (1 + _itemStatistics.SpreadAngle); }
    }

    public float ReloadTime {
        get { return _weapon.ReloadTime * (1 + _itemStatistics.ReloadTime); }
    }

    private void Awake()
    {
        _weapon = null;
        _bulletPrefab = null;
        _magazineSize = 0;
        _bulletSpeed = 0f;
        _shootDelay = 0f;
        _projectileCount = 0;
        _rafaleDelay = 0f;
        _rafaleCount = 0;
        _angle = 0f;
        _cadence = WeaponCadence.None;
        _shootCoroutine = new FlagCoroutine(true);
        _reloadCoroutine = new FlagCoroutine(true);
        _itemStatistics = new ItemStatistic();
    }

    [Button ("Init Weapon")]
    public void InitWeapon()
    {
        Init(_shootPoint);
        Init(_weapon);
    }

    public void Init(Transform shootPoint)
    {
        _shootPoint = shootPoint;
    }

    public void Init(SOWeapon weapon)
    {
        _weapon = weapon;
        _bulletPrefab = weapon.BulletPrefab;
        _magazineSize = weapon.MagazineSize;
        _bulletSpeed = weapon.BulletSpeed;
        _shootDelay = weapon.ShootDelay;
        _projectileCount = weapon.ProjectileCount;
        _rafaleDelay = weapon.RafaleDelay;
        _rafaleCount = weapon.RafaleCount;
        _angle = weapon.Angle;
        _cadence = weapon.Cadence;
        if (_shootCoroutine != null)
            StartCoroutine(StopFlagCoroutine(_shootCoroutine));
        else
            _shootCoroutine = new FlagCoroutine(true);
        if (_reloadCoroutine != null)
            StartCoroutine(StopFlagCoroutine(_reloadCoroutine));
        else
            _reloadCoroutine = new FlagCoroutine(true);
    }

    public void Init(ItemStatistic itemStatistics)
    {
        _itemStatistics = itemStatistics;
    }

    public void Init(Transform shootPoint, SOWeapon weapon, ItemStatistic itemStatistics)
    {
        Init(shootPoint);
        Init(weapon);
        Init(itemStatistics);
    }

    public int UpdateMagazineSize()
    {
        return _magazineSize;
    }

    private IEnumerator StopFlagCoroutine(FlagCoroutine flagCoroutine)
    {
        yield return new WaitUntil(() => flagCoroutine.Flag == true);
        flagCoroutine.Flag = true;
    }

    public void Reload()
    {
        if (_reloadCoroutine.Flag == true && _magazineSize < MaxMagazineSize)
            _reloadCoroutine.Coroutine = StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        _reloadCoroutine.Flag = false;
        yield return new WaitForSeconds(ReloadTime);
        _magazineSize = MaxMagazineSize;
        Debug.Log("Reloaded");
        _reloadCoroutine.Flag = true;
    }

    public void Shot(Vector3 direction)
    {
        if (_magazineSize <= 0 || _reloadCoroutine.Flag == false)
            return;
        direction.z = 0f;
        direction = direction - transform.position;
        direction.Normalize();
        switch (_cadence)
        {
            case WeaponCadence.Single:
                if (Time.time > _lastShootTime + ShootDelay && _hold == false) {
                    _shootCoroutine.Coroutine = StartCoroutine(SingleShot(direction, 0f));
                    _lastShootTime = Time.time;
                }
                break;
            case WeaponCadence.Rafale:
                if (Time.time > _lastShootTime + ShootDelay) {
                    _shootCoroutine.Coroutine = StartCoroutine(RafaleShot(direction));
                    _lastShootTime = Time.time;
                }
                break;
            case WeaponCadence.Auto:
                if (_shootCoroutine.Flag == true)
                    _shootCoroutine.Coroutine = StartCoroutine(SingleShot(direction, ShootDelay));
                _lastShootTime = Time.time;
                break;
            default:
                break;
        }
    }

    private IEnumerator SingleShot(Vector3 direction, float delay)
    {
        _shootCoroutine.Flag = false;
        _magazineSize--;
        yield return MultiShot(direction);
        if (delay > 0f)
            yield return new WaitForSeconds(delay);
        _shootCoroutine.Flag = true;
    }

    private IEnumerator RafaleShot(Vector3 direction)
    {
        _shootCoroutine.Flag = false;
        for (int i = 0; i < RafaleProjectile && _magazineSize > 0; i++) {
            _magazineSize--;
            yield return MultiShot(direction);
            yield return new WaitForSeconds(RafaleDelay);
        }
        _shootCoroutine.Flag = true;
    }

    private IEnumerator MultiShot(Vector3 direction)
    {
        float angleStep = Angle / (ProjectileCount - 1);
        Quaternion rotationOffset = Quaternion.Euler(0f, 0f, -Angle / 2f);

        for (int i = 0; i < ProjectileCount; i++) {
            float currentAngle = angleStep < 360f ? i * angleStep : 0f;
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, currentAngle);
            Vector3 bulletDirection = rotationOffset * bulletRotation * direction;
            GameObject bullet = Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.identity);
            bullet.transform.parent = _shootPoint;
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * BulletSpeed;
            Destroy(bullet, _bulletLifeTime);
        }
        yield return null;
    }
}

[System.Serializable]
public class FlagCoroutine
{
    private bool _flag = false;
    private Coroutine _coroutine;


    public bool Flag {
        get { return _flag; }
        set { _flag = value; }
    }

    public Coroutine Coroutine {
        get { return _coroutine; }
        set { _coroutine = value; }
    }

    public FlagCoroutine()
    {
        _flag = false;
    }

    public FlagCoroutine(bool flag)
    {
        _flag = flag;
    }
}