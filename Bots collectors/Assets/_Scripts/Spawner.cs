using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _spawnCount;
    [SerializeField] private float _minOffset;
    [SerializeField] private float _maxOffset;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private bool _spawnOnStart;
    [SerializeField] private bool _autoSpawn;
    [SerializeField] Transform _spawnPoint;

    private Vector3 _defaultPosition;
    private Coroutine _spawning;

    public event UnityAction<T> Spawned;

    private void Start()
    {
        if(_minOffset > _maxOffset)
            _minOffset = _maxOffset;

        _defaultPosition = _spawnPoint.position;

        if(_spawnOnStart)
            ArrangeSpawnObjects(_spawnCount);
    }

    private void Update()
    {
        if(_autoSpawn)
            if (_spawning == null)
                _spawning = StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        yield return new WaitForSeconds(_spawnDelay);

        ArrangeSpawnObjects(_spawnCount);
        _spawning = null;
    }

    public void InitSpawnPoint(Transform newSpawnPoint)
    {
        _spawnPoint = newSpawnPoint;
    }


    public virtual T SpawnObject()
    {
        T spawnObject = Instantiate(_prefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
        _spawnPoint.transform.position = new Vector3(_spawnPoint.position.x + Random.Range(_minOffset, _maxOffset),
            _spawnPoint.position.y, _spawnPoint.position.z + Random.Range(_minOffset, _maxOffset));
        Spawned?.Invoke(spawnObject);
        return spawnObject;
    }

    public void DestroyObject(T destroyedObject)
    {
        Destroy(destroyedObject.gameObject);
    }

    public void ArrangeSpawnObjects(int spawnCount)
    {
        for(int i = 0; i < spawnCount; i++)
        {
            SpawnObject();
        }

        _spawnPoint.position = _defaultPosition;
    }
}
