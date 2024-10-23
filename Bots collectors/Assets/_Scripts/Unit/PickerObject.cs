using UnityEngine;
using UnityEngine.Events;

public class PickerObject : MonoBehaviour
{
    [SerializeField] private float _holdDistance;
    [SerializeField] private Transform _handTransform;
    [SerializeField] private Unit _unit;
    [SerializeField] private PickingObject _pickingObject;
    [SerializeField] private UnitMover _mover;
    [SerializeField] private CrystalSearcher _crystalSearcher;

    private bool _isFinish;
    private bool _isWork;

    public event UnityAction<Unit> PickObject;

    private void OnEnable()
    {
        _mover.Finished += Finish;
    }

    private void OnDisable()
    {
        _mover.Finished -= Finish;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isWork == false)
            if(other.TryGetComponent(out PickingObject pickingObject))
                _pickingObject = pickingObject;
    }

    public void Finish()
    {
        if(_pickingObject)
        {
            _pickingObject.PickUp(_handTransform, _holdDistance);
            PickObject?.Invoke(GetComponent<Unit>());
            _pickingObject.DestroyCrystal();
            _isWork = true;
        }
    }

    public int GiveToBase()
    {
        if (_pickingObject == null)
            return 0;

        _pickingObject.GiveToBase();
        _unit.InvokeFree();
        _isWork = false;

        return _pickingObject.Denomination;
    }
}
