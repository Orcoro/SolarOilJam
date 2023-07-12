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
    [SerializeField] private List<AudioElement> _entitySounds;
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

    public List<AudioElement> EntitySounds {
        get { return _entitySounds; }
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

    public AudioClip GetAudioClip(AudioType audioType)
    {
        AudioElement audioElement = _entitySounds.Find(x => x.AudioType == audioType);

        if (audioElement != null)
            return audioElement.AudioClip;
        return null;
    }
}

[System.Serializable]
public class AudioElement
{
    [SerializeField] private AudioType _audioType;
    [SerializeField] private AudioClip _audioClip;

    public AudioType AudioType {
        get { return _audioType; }
    }

    public AudioClip AudioClip {
        get { return _audioClip; }
    }
}

public enum AudioType
{
    Attack,
    Death,
    Hurt,
    Walk
}