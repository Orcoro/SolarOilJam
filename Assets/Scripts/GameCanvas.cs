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
    private int currentMaxMagazineSize = 10;

    void Start()
    {
        SOWeapon _weapon = GameManager.Instance.DefaultWeapon;
        _ammoText.text = GameManager.Instance.DefaultWeapon.MagazineSize * 10 + "ml/" + GameManager.Instance.DefaultWeapon.MagazineSize * 10 + "ml";
    }

    public void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        int magazineSize = player.GetComponent<Shoot>().UpdateMagazineSize();
        int maxMagazineSize = player.GetComponent<Shoot>().MaxMagazineSize;
        if (maxMagazineSize > currentMaxMagazineSize)
            currentMaxMagazineSize = maxMagazineSize;
        Shoot shoot = FindObjectOfType<Shoot>();
        _ammoText.text = magazineSize * 10 + "ml/" + currentMaxMagazineSize * 10 + "ml";
    }

    public void UpdateScore(int score)
    {
        totalScore += score;
        if (totalScore < 0)
            totalScore = 0;
        _scoreText.text = totalScore.ToString();
    }

    public void UpdateHealth(int damage)
    {
        //_healthText.value -= damage;
    }
}
