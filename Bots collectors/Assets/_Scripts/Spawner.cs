using UnityEngine;
using UnityEngine.Events;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _spawnCount;
    [SerializeField] private float _offset;
    [SerializeField] private bool _autoSpawn;
    [SerializeField] Transform _spawnPoint;

    private Vector3 _defaultTransform;

    public event UnityAction<T> OnSpawn;

    private void Start()
    {
        _defaultTransform = _spawnPoint.position;

        if(_autoSpawn)
            ArrangeSpawnObjects(_spawnCount);
    }

    public virtual void ArrangeSpawnObjects(int spawnCount)
    {
        for(int i = 0; i < spawnCount; i++)
        {
            T spawnObject = Instantiate(_prefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
            _spawnPoint.transform.position = new Vector3(_spawnPoint.position.x + _offset, _spawnPoint.position.y, _spawnPoint.position.z);
            OnSpawn?.Invoke(spawnObject);
        }

        _spawnPoint.position = _defaultTransform;
    }

    public void InitSpawnPoint(Transform newSpawnPoint)
    {
        _spawnPoint = newSpawnPoint;
    }
}
