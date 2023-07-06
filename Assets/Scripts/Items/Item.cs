using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private SOItem _item;
    private SpriteRenderer _background;
    private SpriteRenderer _icon;

    private void Awake()
    {
        _icon = GetComponent<SpriteRenderer>();
        _background = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (_item != null)
            Init(_item);
    }

    public void Init(SOItem item)
    {
        _item = item;
        _background.color = item.ItemColor;
        _icon.sprite = item.ItemSprite;
    }

    public bool IsOver(Vector3 position, float radius)
    {
        float itemRadius = _background.bounds.size.x / 2;
        float distance = Vector3.Distance(position, transform.position);

        return distance <= radius + itemRadius;
    }

    private void Update()
    {
        if (IsOver(Player.Instance.transform.position, 1.5f)) {
            Player.Instance.PickUpItem(_item);
            Destroy(gameObject);
        }
    }
}