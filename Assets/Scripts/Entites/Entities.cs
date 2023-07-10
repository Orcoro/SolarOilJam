using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entities : MonoBehaviour, IKillable 
{
    [SerializeField] private SOEntities _entity;
    private Statistic _statistic = new Statistic();
    private IMoveable _movement;
    private Health _health;
    private IAttackable _attackable;
    private float _range;

    public Statistic Statistic {
        get { return _statistic; }
    }

    public float Range {
        get { return _range * (1 + _statistic.EntitiesStatistic.Range); }
    }

    private void Awake()
    {
        _range = 5f;
    }

    private void Start()
    {
        
    }

    public void Init()
    {
        _movement = GetComponent<IMoveable>();
        _health = GetComponent<Health>();
        if (_movement == null)
            throw new System.Exception("Movement is NULL");
        else
            _movement.Init();
        if (_health == null)
            throw new System.Exception("Health is NULL");
        else
            _health.Init(true);
    }

    public void Init(SOEntities entity)
    {
        _entity = entity;
        _statistic = _entity.EntityStatistic;
        if (_entity.AttackStyle == AttackStyle.Range)
            _attackable = gameObject.AddComponent<Shoot>();
        _attackable.Init(transform, _entity.EntityWeapon);
        Init();
    }

    private void Update()
    {
        if (_movement != null && CanMove())
            _movement.Move(Player.Instance.transform.position - transform.position);
        // if (_attackable is IMelee attackable)
        //     attackable.Attack(attackable.CanAttack(), false, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (_attackable is IRange attackable)
            attackable.Attack(attackable.CanAttack() && PlayerIsInRange(), attackable.CanReload(), Player.Instance.transform.position);
    }

    private bool PlayerIsInRange()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position) < (_entity.AttackStyle == AttackStyle.Melee ? Range : Range * 2);
    }

    private bool CanMove()
    {
        if (_attackable is IRange && Vector3.Distance(transform.position, Player.Instance.transform.position) < Range)
            return false;
        if (_attackable is IMelee)
            Debug.Log("CanMove as melee");
        return true;
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