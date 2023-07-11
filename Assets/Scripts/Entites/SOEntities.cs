using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "NewEntity", menuName = "SolarOil/New Entity", order = 0)]
public class SOEntities : ScriptableObject
{
    [SerializeField] private string _entityName;
    [SerializeField] private GameObject _entityPrefab;
    [SerializeField] private Sprite _entitySprite;
    [SerializeField] private CharacterAnimation _entityAnimation;
    [SerializeField] private Vector2 _hitBoxSize;
    [SerializeField] private Vector2 _hitBoxOffset;
    [SerializeField] private SOWeapon _entityWeapon;
    [SerializeField] private Statistic _entityStatistic;
    [SerializeField] private AttackStyle _attackStyle;

    public string EntityName {
        get { return _entityName; }
    }

    public GameObject EntityPrefab {
        get { return _entityPrefab; }
    }

    public Sprite EntitySprite {
        get { return _entitySprite; }
    }

    public CharacterAnimation EntityAnimation {
        get { return _entityAnimation; }
    }

    public Vector2 HitBoxSize {
        get { return _hitBoxSize; }
    }

    public Vector2 HitBoxOffset {
        get { return _hitBoxOffset; }
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