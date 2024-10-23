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
    [SerializeField] private bool _spawnToStart;
    [SerializeField] private bool _autoSpawn;
    [SerializeField] Transform _spawnPoint;

    private Vector3 _defaultTransform;
    private Coroutine _spawnToTime;

    public event UnityAction<T> Spawned;

    private void Start()
    {
        if(_minOffset > _maxOffset)
            _minOffset = _maxOffset;

        _defaultTransform = _spawnPoint.position;

        if(_spawnToStart)
            ArrangeSpawnObjects(_spawnCount);
    }

    private void Update()
    {
        if(_autoSpawn)
            if (_spawnToTime == null)
                _spawnToTime = StartCoroutine(SpawnToTime());
    }

    private IEnumerator SpawnToTime()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_spawnDelay);

        yield return waitForSeconds;

        ArrangeSpawnObjects(_spawnCount);
        _spawnToTime = null;
    }

    public virtual void ArrangeSpawnObjects(int spawnCount)
    {
        for(int i = 0; i < spawnCount; i++)
        {
            T spawnObject = Instantiate(_prefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
            _spawnPoint.transform.position = new Vector3(_spawnPoint.position.x + Random.Range(_minOffset, _maxOffset), 
                _spawnPoint.position.y, _spawnPoint.position.z + Random.Range(_minOffset, _maxOffset));
            Spawned?.Invoke(spawnObject);
        }

        _spawnPoint.position = _defaultTransform;
    }

    public void InitSpawnPoint(Transform newSpawnPoint)
    {
        _spawnPoint = newSpawnPoint;
    }
}
