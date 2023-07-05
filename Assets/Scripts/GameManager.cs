using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SOWeapon _defaultWeapon;

    private static GameManager _instance;

    public SOWeapon DefaultWeapon {
        get { return _defaultWeapon; }
    }

    public static GameManager Instance {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }
}
