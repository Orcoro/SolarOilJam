using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    private Movement _movement;
    private Shoot _shoot;
    private ItemSystem _itemSystem;

    private static Player _instance;

    public static Player Instance {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _shoot = GetComponent<Shoot>();
        _itemSystem = GetComponent<ItemSystem>();
        if (_movement == null)
            throw new System.Exception("Movement is NULL");
        if (_shoot == null)
            throw new System.Exception("Shoot is NULL");
        if (_itemSystem == null)
            throw new System.Exception("ItemSystem is NULL");
        _shoot.Init(_shootPoint, GameManager.Instance.DefaultWeapon, _itemSystem.Statistics);
    }

    private void Update()
    {
        Movement();
        Shoot();
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        _movement.Move(new Vector3(horizontalInput, verticalInput, 0));
    }

    private void Shoot()
    {
        if (Input.GetAxis("Reload") > 0)
            _shoot.Reload();
        if (Input.GetAxis("Fire") > 0) {
            _shoot.Shot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            _shoot.Hold = true;
        } else
            _shoot.Hold = false;
    }

    public void TakeDamage(int damage)
    {
        GameCanvas _gameCanvas = FindObjectOfType<GameCanvas>();
        _gameCanvas.UpdateHealth(damage);
        if (_gameCanvas.CurrentHealth() <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("Player is dead");
    }

    public void PickUpItem(SOItem item)
    {
        _itemSystem.AddItem(item);
    }
}
