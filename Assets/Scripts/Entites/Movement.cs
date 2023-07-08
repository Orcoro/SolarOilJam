using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IMoveable
{
    [SerializeField] Rigidbody2D _rigidbody2D;
    private float pushForce = 2.0f;
    private IKillable _owner;
    private float _initialSpeed = 100.0f;
    private Vector3 _direction;

    public float Speed {
        get { return _initialSpeed * (1 + _owner.Statistic.EntitiesStatistic.Speed); }
    }

    public Vector3 Direction {
        get { return _direction; }
    }

    public void Awake()
    {
        _initialSpeed = 25.0f;
        if (_rigidbody2D == null)
            _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public Movement(float speed)
    {
        _initialSpeed = speed;
    }

    public void Init()
    {
        _owner = GetComponent<IKillable>();
        if (_owner == null) {
            _owner = gameObject.tag == "Player" ? gameObject.AddComponent<Player>() : gameObject.AddComponent<Entities>();
            throw new System.Exception("Owner is NULL");
        }
        Debug.Log($"init movement with speed {_initialSpeed} speed {Speed}");
    }

    public void Move(Vector3 direction)
    {
        _direction = direction;
        _rigidbody2D.MovePosition(transform.position + (_direction * Speed * Time.deltaTime));
        //transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRb = collision.collider.attachedRigidbody;

        Debug.Log($"{gameObject.name} have Collision with {collision.gameObject.name}");
        if (otherRb != null && !otherRb.isKinematic) {
            Vector3 forceDirection = collision.GetContact(0).normal;
            otherRb.AddForce(forceDirection * pushForce, ForceMode.Impulse);
        }
    }
}
