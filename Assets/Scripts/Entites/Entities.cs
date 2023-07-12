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
    private BoxCollider2D _collider;
    private AudioSource _audioSource;
    private CharacterAnimator _animation;
    private Status _status = Status.Alive;
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
        _status = Status.Alive;
    }

    private void Start()
    {
        
    }

    public void Init()
    {
        _movement = GetComponent<IMoveable>();
        _health = GetComponent<Health>();
        _animation = GetComponent<CharacterAnimator>();
        _collider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        if (_movement == null)
            throw new System.Exception("Movement is NULL");
        else
            _movement.Init();
        if (_health == null)
            throw new System.Exception("Health is NULL");
        else
            _health.Init(true);
        _animation.Init(_entity.EntityAnimation);
        _collider.size = _entity.HitBoxSize;
        _collider.offset = _entity.HitBoxOffset;
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

    public void PlayAudioClip(AudioType audioType)
    {
        if (_audioSource == null)
            throw new System.Exception("AudioSource is NULL");
        else {
            _audioSource.clip = _entity.GetAudioClip(audioType);
            if (_audioSource.clip != null) {
                _audioSource.loop = false;
                _audioSource.volume = 0.5f;
                _audioSource.Play();
            }
        }

    }

    private void Update()
    {
        if (_status == Status.Dead)
            return;
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
        _status = Status.Dead;
        _collider.enabled = false;
        PlayAudioClip(AudioType.Death);
        Debug.Log("Entity Die");
        Destroy(this.gameObject, 1f);
    }

    private void OnDestroy()
    {
        GameManager.Instance.DropItem(new Vector3(transform.position.x, transform.position.y, -2f));
    }
}

[System.Serializable]
public enum Status {
    Alive = 0,
    Dead = 1
}

[System.Serializable]
public enum AttackStyle {
    Melee = 0,
    Range = 1,
    Healer = 2
}