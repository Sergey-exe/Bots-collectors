using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskDistributor : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private CrystalSearcher _crystalSearcher;
    [SerializeField] private List<PickingObject> _crystals;
    [SerializeField] private Transform _baseTransform;

    private List<Unit> _freeUnits;

    private void Start()
    {
        _freeUnits = new List<Unit>();
        SearchCrystal();
    }

    private void Update()
    {
        if (_crystals.Count > 0)
            if (_freeUnits.Count > 0)
                GiveTask();
    }

    private void OnEnable()
    {
        _unitSpawner.OnSpawn += SubscribeToSpawn;
    }

    private void OnDisable()
    {
        _unitSpawner.OnSpawn -= SubscribeToSpawn;
    }

    public void GiveTask()
    {
        for(int i = 0; i < _freeUnits.Count; i++)
        {
            _freeUnits[i].SetTaskTransform(GetTaskTransform());
            _freeUnits.RemoveAt(i);
        }
    }

    public void GiveBaseTransform(Unit unit)
    {
        unit.SetTaskTransform(_baseTransform);
    }

    public Transform GetTaskTransform()
    {
        if (_crystals.Count == 0)
            SearchCrystal();

        for (int i = 0; i < _crystals.Count; i++) 
        {
            if (_crystals[i].IsFree == true)
            {
                Transform newTaskTransform = _crystals[i].transform;
                _crystals.RemoveAt(i);

                return newTaskTransform;
            }
        }

        return null;
    }

    private void SearchCrystal()
    {
        _crystals = _crystalSearcher.Search();
    }

    private void SubscribeToSpawn(Unit unit)
    {
        unit.IsFree += AddFreeUnit;
    }

    private void AddFreeUnit(Unit unit)
    {
        _freeUnits.Add(unit);
        unit.GetComponent<PickerObject>().PickObject += GiveBaseTransform;
    }
}
