using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SOWeapon _defaultWeapon;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private List<SOItem> _dropList;

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

    public void DropItem(Vector3 position)
    {
        SOItem item = GetItem();
        GameObject itemObject;

        Debug.Log(item);
        if (item == null)
            return;
        itemObject = Instantiate(_itemPrefab);
        itemObject.transform.position = position;
        itemObject.GetComponent<Item>().Init(item);
    }

    private SOItem GetItem()
    {
        float dropChance = (Random.value * 100f) % 100f ;

        if (dropChance <= 10f) {
            float qualityChance = (Random.value * 100f) % 100f;
            
            if (qualityChance <= 49f)
                return GetItemByQuality(Quality.Common);
            else if (qualityChance <= 79f)
                return GetItemByQuality(Quality.Uncommon);
            else if (qualityChance <= 94f)
                return GetItemByQuality(Quality.Rare);
            else if (qualityChance <= 99f)
                return GetItemByQuality(Quality.Epic);
            else
                return GetItemByQuality(Quality.Legendary);
        }
        return null;
    }

    private SOItem GetItemByQuality(Quality quality)
    {
        List<SOItem> filteredItems = _dropList.FindAll(item => item.ItemQuality == quality);

        if (filteredItems.Count > 0) {
            int randomIndex = Random.Range(0, filteredItems.Count);
            return filteredItems[randomIndex];
        }
        return null;
    }
}
