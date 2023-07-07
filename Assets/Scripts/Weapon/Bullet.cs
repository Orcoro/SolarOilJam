using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Statistic _statistic;
    private int _damage = 1;
    private int _piercing = 1;

    public int Damage {
        get { return _damage + _statistic.BulletStatistic.Damage; }
    }

    public int Piercing {
        get { return _piercing + _statistic.BulletStatistic.Piercing; }
    }

    public void Init(Statistic statistic)
    {
        _statistic = statistic;
        _damage = Damage;
        _piercing = Piercing;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Bullet hit {other.gameObject.name}");
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null) {
            if (damageable.TakeDamage(_damage, gameObject.tag)) {
                _piercing--;
                if (_piercing == 0)
                    Destroy(this.gameObject);
            }
        }
    }
}