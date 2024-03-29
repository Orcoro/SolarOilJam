using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    private IKillable _owner;
    private int _maxHealth = 0;
    private int _currentHealth = 0;

    public int MaxHealth {
        get { return _maxHealth + _owner.Statistic.EntitiesStatistic.Health; }
    }

    public int CurrentHealth {
        get { return _currentHealth; }
    }

    public int Armor {
        get { return _owner.Statistic.EntitiesStatistic.Armor; }
    }

    private void Awake()
    {
        _maxHealth = 0;
        _currentHealth = 0;
    }

    public void Init(bool fullLife, int maxHealth)
    {
        _maxHealth = maxHealth;
        Init(fullLife);
    }

    public void Init(bool fullLife)
    {
        _owner = GetComponent<IKillable>();
        if (_owner == null) {
            _owner = gameObject.tag == "Player" ? gameObject.AddComponent<Player>() : gameObject.AddComponent<Entities>();
            throw new System.Exception("Owner is NULL");
        }
        _maxHealth = MaxHealth;
        _currentHealth = fullLife ? _maxHealth : _currentHealth;
    }

    public void UpdateHealth()
    {
        if (_currentHealth >= _maxHealth)
            _currentHealth = MaxHealth;
        _maxHealth = MaxHealth;
    }

    public bool TakeDamage(int damage, string attacker)
    {
        IKillable killable = gameObject.GetComponent<IKillable>();
        if (attacker == gameObject.tag)
            return false;
        if (Armor > 0)
            damage -= Armor;
            GameCanvas gameCanvas = FindObjectOfType<GameCanvas>();
            gameCanvas.UpdateScore(damage);
            if (gameObject.tag == "Player") {
                gameCanvas.UpdateHealth(damage * 10);
                gameCanvas.UpdateScore(-damage);
            }
        _currentHealth = _currentHealth - damage < 0 ? 0 : _currentHealth - damage;
        if (_currentHealth == 0 && killable != null)
            killable.Die();
        return true;
    }

    public void Heal(int heal)
    {
        _currentHealth += heal;
        if (_currentHealth > MaxHealth)
            _currentHealth = MaxHealth;
    }
}
