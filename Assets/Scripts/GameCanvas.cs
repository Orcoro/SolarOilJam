using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private Slider _healthText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int totalScore = 0;

    void Start()
    {
        SOWeapon _weapon = GameManager.Instance.DefaultWeapon;
        _ammoText.text = GameManager.Instance.DefaultWeapon.MagazineSize * 10 + "ml/" + GameManager.Instance.DefaultWeapon.MagazineSize * 10 + "ml";
    }

    public void Update()
    {
        Shoot shoot = FindObjectOfType<Shoot>();
        _ammoText.text = shoot.UpdateMagazineSize() * 10 + "ml/" + shoot.MaxMagazineSize() * 10 + "ml";
    }

    public void UpdateScore(int score)
    {
        totalScore += score;
        _scoreText.text = totalScore.ToString();
    }

    public void UpdateHealth(int damage)
    {
        _healthText.value -= damage;
    }

    public int CurrentHealth()
    {
        return (int)_healthText.value;
    }

}
