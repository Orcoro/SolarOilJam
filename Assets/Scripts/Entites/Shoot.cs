using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Shoot : MonoBehaviour
{
    [SerializeField] private SOWeapon _weapon;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private float _bulletLifeTime = 5f;
    [SerializeField] private float _shootDelay = 0.5f;
    [SerializeField] private int _projectileCount = 1;
    [SerializeField] private int _rafaleCount = 3;
    [SerializeField] private float _angle = 20f;
    [SerializeField] private WeaponCadence _cadence = WeaponCadence.Single;
    private float _lastShootTime = 0f;
    private FlagCoroutine _shootCoroutine;

    [Button ("Init Weapon")]
    public void InitWeapon()
    {
        Init(_shootPoint, _weapon);
    }

    public void Init(Transform shootPoint)
    {
        _shootPoint = shootPoint;
        _shootCoroutine = new FlagCoroutine();
    }

    public void Init(SOWeapon weapon)
    {
        _bulletPrefab = weapon.BulletPrefab;
        _bulletSpeed = weapon.BulletSpeed;
        _bulletLifeTime = weapon.BulletLifeTime;
        _shootDelay = weapon.ShootDelay;
        _projectileCount = weapon.ProjectileCount;
        _rafaleCount = weapon.RafaleCount;
        _angle = weapon.Angle;
        _cadence = weapon.Cadence;
        if (_shootCoroutine != null)
            StartCoroutine(StopShootCoroutine());
        else
            _shootCoroutine = new FlagCoroutine();
    }

    public void Init(Transform shootPoint, SOWeapon weapon)
    {
        Init(shootPoint);
        Init(weapon);
    }

    private IEnumerator StopShootCoroutine()
    {
        yield return new WaitUntil(() => _shootCoroutine.Flag == true);
        _shootCoroutine.Flag = false;
    }

    public void Shot(Vector3 direction)
    {
        Debug.Log("Shot");
        direction.z = 0f;
        direction = direction - transform.position;
        direction.Normalize();
        switch (_cadence)
        {
            case WeaponCadence.Single:
                if (Time.time > _lastShootTime + _shootDelay) {
                    _shootCoroutine.Coroutine = StartCoroutine(SingleShot(direction, 0f));
                    _lastShootTime = Time.time;
                }
                break;
            case WeaponCadence.Rafale:
                if (Time.time > _lastShootTime + _shootDelay) {
                    _shootCoroutine.Coroutine = StartCoroutine(RafaleShot(direction));
                    _lastShootTime = Time.time;
                }
                break;
            case WeaponCadence.Auto:
                _shootCoroutine.Coroutine = StartCoroutine(SingleShot(direction, _shootDelay));
                _lastShootTime = Time.time;
                break;
            default:
                break;
        }
    }

    private IEnumerator SingleShot(Vector3 direction, float delay)
    {
        _shootCoroutine.Flag = false;
        yield return MultiShot(direction);
        if (delay > 0f)
            yield return new WaitForSeconds(delay);
        _shootCoroutine.Flag = true;
    }

    private IEnumerator RafaleShot(Vector3 direction)
    {
        float rafaleStep = 0.05f;

        _shootCoroutine.Flag = false;
        for (int i = 0; i < _rafaleCount; i++) {
            yield return MultiShot(direction);
            yield return new WaitForSeconds(rafaleStep);
        }
        _shootCoroutine.Flag = true;
    }

    private IEnumerator MultiShot(Vector3 direction)
    {
        float angleStep = _angle / (_projectileCount - 1);
        Quaternion rotationOffset = Quaternion.Euler(0f, 0f, -_angle / 2f);

        for (int i = 0; i < _projectileCount; i++) {
            float currentAngle = angleStep < 360f ? i * angleStep : 0f;
            Quaternion bulletRotation = Quaternion.Euler(0f, 0f, currentAngle);
            Vector3 bulletDirection = rotationOffset * bulletRotation * direction;
            GameObject bullet = Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * _bulletSpeed;
            Destroy(bullet, _bulletLifeTime);
        }
        yield return null;
    }
}

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