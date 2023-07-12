using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Shoot : MonoBehaviour, IRange
{
    private SOWeapon _weapon;
    private Transform _shootPoint;
    private GameObject _bulletPrefab;
    private int _magazineSize;
    private float _bulletSpeed;
    private float _shootDelay;
    private int _projectileCount;
    private float _rafaleDelay;
    private int _rafaleCount;
    private float _angle;
    private WeaponCadence _cadence = WeaponCadence.Single;
    private float _bulletLifeTime;
    private float _lastShootTime;
    private bool _hold = false;
    private FlagCoroutine _shootCoroutine;
    private FlagCoroutine _reloadCoroutine;
    private IKillable _owner = null;

    public bool Hold {
        get { return _hold; }
        set { _hold = value; }
    }

    public int MaxMagazineSize {
        get { return (_weapon == null ? 10 : _weapon.MagazineSize) + (_owner == null ? 0 : _owner.Statistic.WeaponStatistic.AdditionnalMunition); }
    }

    public float BulletSpeed {
        get { return _weapon.BulletSpeed * (_owner == null ? 1f : (1f + _owner.Statistic.WeaponStatistic.BulletVelocity)); }
    }

    public float ShootDelay {
        get { return _shootDelay * (_owner == null ? 1f : (1f + _owner.Statistic.WeaponStatistic.ShootDelayMultiplier)); }
    }

    public int ProjectileCount {
        get { return _weapon.ProjectileCount + (_owner == null ? 0 : _owner.Statistic.WeaponStatistic.ProjectileBonus); }
    }

    public float RafaleDelay {
        get { return _rafaleDelay * (_owner == null ? 1f : (1f + _owner.Statistic.WeaponStatistic.RafaleDelay)); }
    }

    public int RafaleProjectile {
        get { return _weapon.RafaleCount + (_owner == null ? 0 : _owner.Statistic.WeaponStatistic.RafaleProjectileBonus); }
    }

    public float Angle {
        get { return _angle * (_owner == null ? 1f : (1f + _owner.Statistic.WeaponStatistic.SpreadAngle)); }
    }

    public float ReloadTime {
        get { return (_weapon == null ? 1f : _weapon.ReloadTime) * (_owner == null ? 1f : (1f + _owner.Statistic.WeaponStatistic.ReloadTime)); }
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
        _angle = 135f;
        _bulletLifeTime = 15f;
        _cadence = WeaponCadence.None;
        _shootCoroutine = new FlagCoroutine(true);
        _reloadCoroutine = new FlagCoroutine(true);
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

    public void Init()
    {
        _owner = GetComponent<IKillable>();
        if (_owner == null) {
            _owner = gameObject.tag == "Player" ? gameObject.AddComponent<Player>() : gameObject.AddComponent<Entities>();
            throw new System.Exception("Owner is NULL");
        }
    }

    public void Init(Transform shootPoint, SOWeapon weapon)
    {
        Init(shootPoint);
        Init(weapon);
        Init();
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

    public void Attack(bool fire, bool reload, Vector3 direction)
    {
        //Debug.Log($"Attack {fire} hold {_hold}");
        if (reload)
            Reload();
        if (fire) {
            Aim(direction);
            _hold = true;
        } else
            _hold = false;
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
        _reloadCoroutine.Flag = true;
    }

    public bool CanReload()
    {
        //Debug.Log($"CanReload {_shootCoroutine.Flag} {_reloadCoroutine.Flag} {_magazineSize < MaxMagazineSize}");
        return _shootCoroutine.Flag == true && _reloadCoroutine.Flag == true && _magazineSize < MaxMagazineSize;
    }

    public bool CanAttack()
    {
        //Debug.Log($"CanAttack {(_cadence == WeaponCadence.Single && _hold == true ? false : true)} {_shootCoroutine.Flag} {_reloadCoroutine.Flag} {_magazineSize > 0}");
        return (_cadence == WeaponCadence.Single && _hold == true ? false : true) && _shootCoroutine.Flag == true && _reloadCoroutine.Flag == true && _magazineSize > 0;
    }

    public void Aim(Vector3 direction)
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
                    //Debug.Log($"{Time.time} > {_lastShootTime} + {ShootDelay} = {Time.time > _lastShootTime + ShootDelay} && {_hold} == false");
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
        float angleStep = ProjectileCount > 1 ? Angle / (ProjectileCount - 1) : 0f;
        Quaternion rotationOffset = Quaternion.Euler(0f, 0f, ProjectileCount > 1 ? -Angle / 2f : 0f);

        for (int i = 0; i < ProjectileCount; i++) {
            float currentAngle = ProjectileCount > 1 ? i * angleStep : 0f;
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, currentAngle);
            Vector3 bulletDirection = rotationOffset * bulletRotation * direction;
            GameObject bullet = Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * BulletSpeed;
            bullet.GetComponent<Bullet>().Init(_owner, _weapon);
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