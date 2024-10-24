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
        Transform newTaskTransform = _crystalSearcher.GetCrustal();

        return newTaskTransform;
    }

    public void AddUnit(Unit unit)
    {
        unit.GetComponent<PickerObject>().PickObject += GiveBaseTransform;
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

    private List<Transform> GetTasksTransforms(int countTransforms)
    {
        List<Transform> transforms = new List<Transform>();

        for (int i = 0; i < countTransforms; i++)
        {
            Transform transform1 = GetTaskTransform();

            transforms.Add(transform1);
        }

        return transforms;
    }

    public void GiveTasks()
    {
        List<Unit> freeUnits = SearchFreeUnits();
        List<Transform> transforms = GetTasksTransforms(freeUnits.Count);

        for (int i = 0; i < freeUnits.Count; i++)
            freeUnits[i].SetTaskTransform(transforms[i]);
        
    }
}
