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

    [SerializeField] private List<Unit> _freeUnits;
    private List<Transform> _newBasesTransforms;

    private void Start()
    {
        _freeUnits = new List<Unit>();
        _newBasesTransforms = new List<Transform>();
        SearchCrystal();
    }

    private void OnEnable()
    {
        _unitSpawner.OnSpawn += SubscribeToSpawn;
        _flagSetter.IsSetFlag += AddNewBaseFlag;
    }

    private void OnDisable()
    {
        _unitSpawner.OnSpawn -= SubscribeToSpawn;
        _flagSetter.IsSetFlag -= AddNewBaseFlag;
    }

    private void Update()
    {
        if (_freeUnits.Count > 0)
        {
            if (_newBasesTransforms.Count > 0)
                GiveNewBaseFlag();
            else if (_crystals.Count >= 0)
                GiveTask(GetTaskTransform());
        }
    }

    public void GiveNewBaseFlag()
    {
        for (int i = 0; i < _freeUnits.Count; i++)
        {
            for (int j = _newBasesTransforms.Count - 1; j >= 0; j--)
            {
                _freeUnits[i].SetTaskTransform(_newBasesTransforms[j]);
                _freeUnits[i].ChangeFlagMoveToFlag(true);
                _freeUnits[i].GetComponent<PickerObject>().PickObject -= GiveBaseTransform;
                _freeUnits[i].IsFree -= AddFreeUnit;
                _newBasesTransforms.RemoveAt(j);
                _freeUnits.RemoveAt(i);
            }
        }
    }

    public void GiveTask(Transform transform)
    {
        for (int i = 0; i < _freeUnits.Count; i++)
        {
            _freeUnits[i].SetTaskTransform(transform);
            _freeUnits.RemoveAt(i);
        }
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

    public void AddFreeUnit(Unit unit)
    {
        _freeUnits.Add(unit);
        unit.GetComponent<PickerObject>().PickObject += GiveBaseTransform;
    }

    private void SearchCrystal()
    {
        _crystals = _crystalSearcher.Search();
    }

    private void SubscribeToSpawn(Unit unit)
    {
        unit.IsFree += AddFreeUnit;
    }
}
