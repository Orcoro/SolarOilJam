using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "NewEntity", menuName = "SolarOil/New Entity", order = 0)]
public class SOEntities : ScriptableObject
{
    [SerializeField] private string _entityName;
    [SerializeField] private Sprite _entitySprite;
    [SerializeField] private SOWeapon _entityWeapon;
    [SerializeField] private Statistic _entityStatistic;
    [SerializeField] private AttackStyle _attackStyle;

    public string EntityName {
        get { return _entityName; }
    }

    public Sprite EntitySprite {
        get { return _entitySprite; }
    }

    public SOWeapon EntityWeapon {
        get { return _entityWeapon; }
    }

    public Statistic EntityStatistic {
        get { return _entityStatistic; }
    }

    public AttackStyle AttackStyle {
        get { return _attackStyle; }
    }
}