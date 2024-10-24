using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TaskDistributor : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private CrystalSearcher _crystalSearcher;
    [SerializeField] private Transform _baseTransform;
    [SerializeField] private FlagSetter _flagSetter;
    [SerializeField] private List<Unit> _units;
    [SerializeField] private List<Transform> _pickingObjects;
    [SerializeField] private List<Transform> _busyPickingObjects;

    private int _minCountUnits = 1;
    private List<Transform> _newBasesTransforms;
    private List<Unit> _freeUnits;

    private void Start()
    {
        _newBasesTransforms = new List<Transform>();
    }

    private void OnEnable()
    {
        _unitSpawner.Spawned += AddUnit;
        _flagSetter.IsSetFlag += AddNewBaseFlag;
    }

    private void OnDisable()
    {
        _unitSpawner.Spawned -= AddUnit;
        _flagSetter.IsSetFlag -= AddNewBaseFlag;
    }

    private void Update()
    {
        if(_pickingObjects.Count == 0)
            _pickingObjects.AddRange(_crystalSearcher.GetCrustals());
        

        if (_newBasesTransforms.Count > 0 & _units.Count > _minCountUnits)
            GiveNewBaseFlag();
        else
            GiveTasks();
    }

    public void GiveNewBaseFlag()
    {
        List<Unit> freeUnits = SearchFreeUnits();

        for (int i = _newBasesTransforms.Count - 1; i >= 0; i--)
        {
            for (int j = freeUnits.Count - 1; j >= 0; j--)
            {
                freeUnits[j].SetTaskTransform(_newBasesTransforms[i]);
                freeUnits[j].ChangeFlagMoveToFlag(true);
                PickerObject pickerObject = freeUnits[j].GetComponent<PickerObject>();
                pickerObject.PickObject -= GiveBaseTransform;
                pickerObject.GiveObject -= AcceptTransform;
                _newBasesTransforms.RemoveAt(i);
                _units.Remove(freeUnits[j]);
                freeUnits.Remove(freeUnits[j]);
            }
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

    public void AddUnit(Unit unit)
    {
        PickerObject pickerObject = unit.GetComponent<PickerObject>();
        pickerObject.PickObject += GiveBaseTransform;
        pickerObject.GiveObject += AcceptTransform;
        _units.Add(unit);
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

    private void GiveTasks()
    {
        List<Unit> freeUnits = SearchFreeUnits();

        for (int i = 0; i < freeUnits.Count; i++)
        {
            for (int j = _pickingObjects.Count - 1; j >= 0; j--)
            {
                if (IsRepeatTransform(_pickingObjects[j]) == false)
                {
                    freeUnits[i].SetTaskTransform(_pickingObjects[j]);
                    _busyPickingObjects.Add(_pickingObjects[j]);
                    _pickingObjects.Remove(_pickingObjects[j]);
                    break;
                }
            }
        }
    }

    private bool IsRepeatTransform(Transform transform)
    {
        foreach (Transform busyTransform in _busyPickingObjects)
        {
            if(busyTransform == transform)
            {
                _pickingObjects.Remove(transform);
                return true;
            }
        }

        return false;
    }

    private void AcceptTransform(Transform transform)
    {
        _busyPickingObjects.Remove(transform);
    }
}