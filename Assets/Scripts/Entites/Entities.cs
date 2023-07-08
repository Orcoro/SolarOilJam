using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entities : MonoBehaviour, IKillable 
{
    private Statistic _statistic = new Statistic();
    private IMoveable _movement;
    private Health _health;
    private SOEntities _entities;
    private AttackStyle _tmpattackStyle = AttackStyle.Range;
    private IAttackable _attackable;

    public Statistic Statistic {
        get { return _statistic; }
    }

    private void Awake()
    {
        _movement = GetComponent<IMoveable>();
        _health = GetComponent<Health>();
        Init(_statistic);
    }

    private void Start()
    {
        
    }

    public void Init(Statistic statistic)
    {
        if (_movement == null)
            throw new System.Exception("Movement is NULL");
        else
            _movement.Init();
        if (_health == null)
            throw new System.Exception("Health is NULL");
        else
            _health.Init(true);
        if (_tmpattackStyle == AttackStyle.Range)
            _attackable = gameObject.AddComponent<Shoot>();
    }

    private void Update()
    {
        // if (_attackable is IMelee)
        //     ((IMelee)_attackable).Attack(((IMelee)_attackable).CanAttack(), false, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (_attackable is IRange)
            ((IRange)_attackable).Attack(((IRange)_attackable).CanAttack(), ((IRange)_attackable).CanReload(), Player.Instance.transform.position);
    }

    public void Die()
    {
        Debug.Log("Entity Die");
        Destroy(this.gameObject);
    }
}

[System.Serializable]
public enum AttackStyle {
    Melee = 0,
    Range = 1,
    Healer = 2
}