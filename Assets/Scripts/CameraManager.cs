using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _offsetRadius = 1f;
    private Vector3 _offset;
    private Movement _targetMovement;
    private Vector3 _velocity = Vector3.zero;
    private CameraManager _instance;
    private Camera _camera;

    private void Awake()
    {
        _offset = transform.position - _target.position;
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
        _camera = GetComponent<Camera>();
    }

    private void Start()
    {
        _targetMovement = _target.GetComponent<Movement>();
        if (_targetMovement == null)
            Debug.LogWarning("Target Movement is NULL");
    }

    private void Update()
    {
        SetOffsetByMousePosition();
    }

    private void LateUpdate()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        transform.position += _offset;
        transform.position = Vector3.ClampMagnitude(transform.position - _target.position, _offsetRadius) + _target.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    public void SetOffsetByMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        mousePosition.z = -10;
        _offset = transform.position - mouseWorldPosition;
        _offset = -_offset;
    }
}
