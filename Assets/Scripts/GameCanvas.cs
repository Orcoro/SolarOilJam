using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    void Start()
    {
        SOWeapon _weapon = GameManager.Instance.DefaultWeapon;
        _ammoText.text = GameManager.Instance.DefaultWeapon.MagazineSize * 10 + "ml/" + GameManager.Instance.DefaultWeapon.MagazineSize * 10 + "ml";
    }

    public void Update()
    {
        Shoot shoot = FindObjectOfType<Shoot>();
        int MaxMag = shoot.MaxMagazineSize();
        _ammoText.text = shoot.UpdateMagazineSize() * 10 + "ml/" + MaxMag * 10 + "ml";
    }
}
