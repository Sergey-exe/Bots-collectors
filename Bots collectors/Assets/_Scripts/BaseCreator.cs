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
        if(_spawner)
            _spawner.OnSpawn += TransferUnitToNewBase;
    }

    private void OnDisable()
    {
        _spawner.OnSpawn -= TransferUnitToNewBase;
    }

    public void CreateNewBase(Unit unitInFlag, Transform point)
    {
        int countSpawnBases = 1;

        _startUnit = unitInFlag;
        _spawner.InitSpawnPoint(point);
        _spawner.ArrangeSpawnObjects(countSpawnBases);
    }

    public void InitSpawner(BaseSpawner spawner)
    {
        _spawner = spawner;
        _spawner.OnSpawn += TransferUnitToNewBase;
    }

    private void TransferUnitToNewBase(Base newBase)
    {
        TaskDistributor taskDistributor = newBase.GetComponent<TaskDistributor>();
        FlagSetter flagSetter = GetComponent<FlagSetter>();
        newBase.GetComponent<BaseCreator>().InitSpawner(_spawner);
        newBase.GetComponent<FlagSetter>().Init(flagSetter.GetRaycaster(), flagSetter.GetInputReader());
        _startUnit.SetTaskTransform(newBase.transform);
        _startUnit.InvokeFree();
        taskDistributor.AddUnit(_startUnit);
        IsCreate?.Invoke(this);
    }
}