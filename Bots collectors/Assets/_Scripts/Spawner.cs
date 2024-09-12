using UnityEngine;
using UnityEngine.Events;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _spawnCount;
    [SerializeField] private float _offset;
    [SerializeField] Transform _spawnPoint;

    public event UnityAction<T> OnSpawn;

    private void Start()
    {
        Spawn();
    }

    public virtual void Spawn()
    {
        Vector3 _defaultTransform = _spawnPoint.position;

        for(int i = 0; i < _spawnCount; i++)
        {
            T spawnObject =  Instantiate(_prefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
            _spawnPoint.transform.position = new Vector3(_spawnPoint.position.x + _offset, _spawnPoint.position.y, _spawnPoint.position.z);
            OnSpawn?.Invoke(spawnObject);
        }

        _spawnPoint.position = _defaultTransform;
    }
}
