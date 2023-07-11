using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private List<SpawnWave> _objectsToSpawn;
    private List<GameObject> _spawnedObjects = new List<GameObject>();
    private float _spawnTime;
    private int _index = 0;

    private void Awake()
    {
        _spawnTime = 0f;
        _index = 0;
    }

    private void Update()
    {
        if (_index < _objectsToSpawn.Count && _spawnTime > _objectsToSpawn[_index].SpawnTime) {
            StartCoroutine(Spawn(_objectsToSpawn[_index].SpawnElements));
            _spawnTime = 0f;
            _index++;
        } else
            _spawnTime += Time.deltaTime;
    }

    private IEnumerator Spawn(List<SpawnElement> elementsToSpawn)
    {
        for (int entity = 0; entity < elementsToSpawn.Count; entity++) {
            for (int count = 0; count < elementsToSpawn[entity].Count; count++) {
                GameObject enemy = Instantiate(_prefab);
                enemy.transform.position = GetRandomPositionInCircle();
                enemy.GetComponent<Entities>()?.Init(elementsToSpawn[entity].Entity);
                _spawnedObjects.Add(enemy);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    public Vector3 GetRandomPositionInCircle()
    {
        float minRadius = CameraManager.Instance.Camera.orthographicSize * 3;
        float offset = 10f;
        float radius = Random.Range(minRadius, minRadius + offset);
        float angle = Random.Range(0f, 2f * Mathf.PI);

        float x = Player.Instance.transform.position.x + radius * Mathf.Cos(angle);
        float y = Player.Instance.transform.position.y + radius * Mathf.Sin(angle);

        return new Vector3(x, y, 0f);
    }
}

[System.Serializable]
public class SpawnWave
{
    [SerializeField] private List<SpawnElement> _spawnElements;
    [SerializeField] private float _spawnTime;

    public List<SpawnElement> SpawnElements {
        get { return _spawnElements; }
    }

    public float SpawnTime {
        get { return _spawnTime; }
    }
}

[System.Serializable]
public class SpawnElement
{
    [SerializeField] private int _count;
    [SerializeField] private SOEntities _entity;

    public int Count {
        get { return _count; }
    }

    public SOEntities Entity {
        get { return _entity; }
    }
}