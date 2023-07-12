using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKillable
{
    [SerializeField] private Transform _shootPoint;
    private SOEntities _entities;
    private Statistic _statistic = new Statistic();
    private Statistic _currentStatistic = new Statistic();
    private IMoveable _movement;
    private Shoot _shoot;
    private ItemSystem _itemSystem;
    private Health _health;

    public Statistic Statistic {
        get { return _statistic; }
    }

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
        _movement = GetComponent<IMoveable>();
        _shoot = GetComponent<Shoot>();
        _itemSystem = GetComponent<ItemSystem>();
        _health = GetComponent<Health>();
        if (_itemSystem == null) {
            _statistic = _currentStatistic;
            throw new System.Exception("ItemSystem is NULL");
        } else
            _statistic = _currentStatistic + _itemSystem.Statistics;
        if (_movement == null)
            throw new System.Exception("Movement is NULL");
        else
            _movement.Init();
        if (_shoot == null)
            throw new System.Exception("Shoot is NULL");
        else
            _shoot.Init(_shootPoint, GameManager.Instance.DefaultWeapon);
        if (_health == null)
            throw new System.Exception("Health is NULL");
        else
            _health.Init(true);
    }

    private void LateUpdate()
    {
        if (_movement != null)
            Movement();
        if (_shoot != null)
            _shoot.Attack(Input.GetAxis("Fire") > 0, Input.GetAxis("Reload") > 0, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        _movement.Move(new Vector3(horizontalInput, verticalInput, 0));
    }

    public void PlayAudioClip(AudioType audioType)
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        GameCanvas _gameCanvas = FindObjectOfType<GameCanvas>();
        _gameCanvas.UpdateHealth(0);
        SceneManager _sceneManager = FindObjectOfType<SceneManager>();
        _sceneManager.GameOver();
    }

    public void PickUpItem(SOItem item)
    {
        if (_itemSystem != null) {
            _itemSystem.AddItem(item);
            _statistic = _currentStatistic + _itemSystem.Statistics;
        }
        if (_health != null)
            _health.UpdateHealth();
    }
}
