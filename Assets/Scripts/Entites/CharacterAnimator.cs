using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private CharacterAnimation _animation;
    private float _timer;
    private int _currentFrame;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(CharacterAnimation animation)
    {
        _spriteRenderer.sprite = animation.DefaultSprite;
        _animation = animation;
    }

    private void Update()
    {
        if (_animation == null)
            return;
        //Debug.Log($"Current frame: {_currentFrame} Timer: {_timer} FrameRate: {_animation.FrameRate} Sprites count: {_animation.Sprites.Count} Sprite: {_spriteRenderer.sprite.name}");
        _timer += Time.deltaTime;
        if (_timer > _animation.FrameRate) {
            _currentFrame = (_currentFrame + 1) % _animation.Sprites.Count == 0 ? 1 : (_currentFrame + 1) % _animation.Sprites.Count;
            _spriteRenderer.sprite =  _animation.Sprites[_currentFrame];
            _timer -= _animation.FrameRate;
        }
    }
}

[System.Serializable]
public class CharacterAnimation
{
    [SerializeField] private string _name;
    [SerializeField] private float _frameRate;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private List<Sprite> _sprites;

    public string Name {
        get { return _name; }
    }

    public float FrameRate {
        get { return _frameRate; }
    }

    public Sprite DefaultSprite {
        get { return _defaultSprite; }
    }

    public List<Sprite> Sprites {
        get { return _sprites; }
    }
}
