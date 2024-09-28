using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskDistributor : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private CrystalSearcher _crystalSearcher;
    [SerializeField] private List<PickingObject> _crystals;
    [SerializeField] private Transform _baseTransform;
    [SerializeField] private FlagSetter _flagSetter;

    [SerializeField] private List<Unit> _units;
    private List<Transform> _newBasesTransforms;

    private void Start()
    {
        _units = new List<Unit>();
        _newBasesTransforms = new List<Transform>();
    }

    private void OnEnable()
    {
        _unitSpawner.OnSpawn += AddUnit;
        _flagSetter.IsSetFlag += AddNewBaseFlag;
    }

    private void OnDisable()
    {
        _unitSpawner.OnSpawn -= AddUnit;
        _flagSetter.IsSetFlag -= AddNewBaseFlag;
    }

    private void Update()
    {
        if (_newBasesTransforms.Count > 0)
            GiveNewBaseFlag();
        else
            GiveTask();
    }

    public void GiveNewBaseFlag()
    {
        List<Unit> freeUnits = SearchFreeUnits();

        for (int i = _newBasesTransforms.Count - 1; i >= 0; i--)
        {
            for(int j = freeUnits.Count - 1; j >= 0; j--)
            {
                freeUnits[j].SetTaskTransform(_newBasesTransforms[i]);
                freeUnits[j].ChangeFlagMoveToFlag(true);
                freeUnits[j].GetComponent<PickerObject>().PickObject -= GiveBaseTransform;
                _newBasesTransforms.RemoveAt(i);
                _units.Remove(freeUnits[j]);
                freeUnits.Remove(freeUnits[j]);
            }
        }
    }

    public void GiveTask()
    {
        List<Unit> freeUnits = SearchFreeUnits();

        foreach (Unit freeUnit in freeUnits)
            freeUnit.SetTaskTransform(GetTaskTransform());
    }

    public void AddNewBaseFlag(Transform flagTransform)
    {
        _newBasesTransforms.Add(flagTransform);
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

    public void AddUnit(Unit unit)
    {
        unit.GetComponent<PickerObject>().PickObject += GiveBaseTransform;
        _units.Add(unit);
    }

    private void SearchCrystal()
    {
        _crystals.AddRange(_crystalSearcher.Search());
    }

    private List<Unit> SearchFreeUnits()
    {
        List<Unit> freeUnits = new List<Unit>();

        foreach (Unit unit in _units)
        {
            if (unit.IsWork == false)
            {
                freeUnits.Add(unit);
            }
        }

        return freeUnits;
    }
}
