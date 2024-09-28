using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCreator : MonoBehaviour
{
    [SerializeField] private BaseSpawner _spawner;

    private Unit _startUnit;

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
            _spawner.ArrangeSpawnObjects(countBases);
            _startUnit = unit;
        }
    }

    private void GetUnitToNewBase(Base newBase)
    {
        _startUnit.SetTaskTransform(newBase.transform);
        newBase.GetComponent<TaskDistributor>().AddFreeUnit(_startUnit);
    }
}
