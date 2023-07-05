using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    private Vector3 _direction;

    public Vector3 Direction {
        get { return _direction; }
    }

    Movement()
    {
        _speed = 5.0f;
    }

    Movement(float speed)
    {
        _speed = speed;
    }

    public void Move(Vector3 direction)
    {
        _direction = direction;
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}
