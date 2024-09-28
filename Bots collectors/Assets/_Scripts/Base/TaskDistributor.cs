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

    private List<Unit> _freeUnits;
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
        _flagSetter.IsSetFlag += GiveNewBaseFlag;
    }

    private void OnDisable()
    {
        _unitSpawner.OnSpawn -= SubscribeToSpawn;
        _flagSetter.IsSetFlag -= GiveNewBaseFlag;
    }

    private void Update()
    {
        if (_crystals.Count > 0)
            if (_freeUnits.Count > 0)
                GiveTask(GetTaskTransform());
    }

    public void GiveTask(Transform transform)
    {
        for(int i = 0; i < _freeUnits.Count; i++)
        {
            _freeUnits[i].SetTaskTransform(transform);
            _freeUnits.RemoveAt(i);
        }
    }

    public void GiveNewBaseFlag(Transform flagTransform)
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

        if(_newBasesTransforms.Count > 0)
        {
            Transform newTaskTransform = _newBasesTransforms[0];
            _newBasesTransforms.RemoveAt(0);

            return newTaskTransform;
        }
            

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

    public void RemoveUnit(Unit unit) 
    {
        unit.GetComponent<PickerObject>().PickObject -= GiveBaseTransform;
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
