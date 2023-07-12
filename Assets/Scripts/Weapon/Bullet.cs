using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private SOWeapon _weapon;
    private IKillable _owner;
    private int _damage = 1;
    private int _piercing = 1;

    public int Damage {
        get { return _damage + _owner.Statistic.BulletStatistic.Damage + _weapon.Damage; }
    }

    public int Piercing {
        get { return _piercing + _owner.Statistic.BulletStatistic.Piercing; }
    }

    public void Init(IKillable owner, SOWeapon weapon)
    {
        _weapon = weapon;
        _owner = owner;
        _damage = Damage;
        _piercing = Piercing;
        if (_owner is MonoBehaviour isOwner)
            gameObject.tag = isOwner.gameObject.tag;
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