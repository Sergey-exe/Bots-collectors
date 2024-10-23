using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseCreator : MonoBehaviour
{
    [SerializeField] private BaseSpawner _spawner;

    private Unit _startUnit;

    public event UnityAction<BaseCreator> Created;

    private void OnEnable()
    {
        if(_spawner)
            _spawner.Spawned += TransferUnitToNewBase;
    }

    private void OnDisable()
    {
        _spawner.Spawned -= TransferUnitToNewBase;
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
        _spawner.Spawned += TransferUnitToNewBase;
    }

    private void TransferUnitToNewBase(Base newBase)
    {

        if(newBase.TryGetComponent(out TaskDistributor newBaseTaskDistributor) 
            & newBase.TryGetComponent(out BaseCreator newBaseBaseCreator) 
            & newBase.TryGetComponent(out FlagSetter newBaseFlagSetter))
        {
            FlagSetter flagSetter = GetComponent<FlagSetter>();
            newBaseBaseCreator.InitSpawner(_spawner);
            newBaseFlagSetter.Init(flagSetter.GetRaycaster(), flagSetter.GetInputReader());
            _startUnit.SetTaskTransform(newBase.transform);
            _startUnit.InvokeFree();
            newBaseTaskDistributor.AddUnit(_startUnit);
            Created?.Invoke(this);
        }
    }
}