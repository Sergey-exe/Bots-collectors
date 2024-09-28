using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseCreator : MonoBehaviour
{
    [SerializeField] private BaseSpawner _spawner;

    private Unit _startUnit;

    public event UnityAction<BaseCreator> IsCreate;

    private void OnEnable()
    {
        _spawner.OnSpawn += GetUnitToNewBase;
    }

    private void OnDisable()
    {
        _spawner.OnSpawn -= GetUnitToNewBase;
    }

    private void OnTriggerEnter(Collider other)
    {
        int countBases = 1;

        if(other.TryGetComponent(out Unit unit))
        {
            _startUnit = unit;
            _spawner.ArrangeSpawnObjects(countBases);
        }
    }

    private void GetUnitToNewBase(Base newBase)
    {
        TaskDistributor taskDistributor = newBase.GetComponent<TaskDistributor>();
        _startUnit.SetTaskTransform(newBase.transform);
        _startUnit.InvokeFree();
        taskDistributor.AddUnit(_startUnit);
        IsCreate?.Invoke(this);
    }
}