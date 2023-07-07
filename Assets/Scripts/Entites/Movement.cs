using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IMoveable
{
    private IKillable _owner;
    private float _speed = 5.0f;
    private Vector3 _direction;

    public float Speed {
        get { return _speed * (1 + _owner.Statistic.EntitiesStatistic.Speed); }
    }

    public Vector3 Direction {
        get { return _direction; }
    }

    public Movement()
    {
        _speed = 5.0f;
    }

    public Movement(float speed)
    {
        _speed = speed;
    }

    public void Init(Statistic statistic)
    {
        _owner = GetComponent<IKillable>();
        if (_owner == null) {
            _owner = gameObject.tag == "Player" ? gameObject.AddComponent<Player>() : gameObject.AddComponent<Entities>();
            throw new System.Exception("Owner is NULL");
        }
        _speed = Speed;
    }

    public void Move(Vector3 direction)
    {
        _direction = direction;
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}
