using UnityEngine;
using UnityEngine.Events;

public class PickerObject : MonoBehaviour
{
    [SerializeField] private float _holdDistance;
    [SerializeField] private Transform _handTransform;
    [SerializeField] private Unit _unit;

    [SerializeField] private PickingObject _pickingObject;
    public event UnityAction<Unit> PickObject;

    private void OnTriggerEnter(Collider other)
    {
        PickingObject pickingObject;

        if(pickingObject = other.GetComponent<PickingObject>())
        {
            if(pickingObject.IsFree == true)
            {
                _pickingObject = pickingObject;
                _pickingObject.PickUp(_handTransform, _holdDistance);
                PickObject?.Invoke(GetComponent<Unit>());
            }
        }
    }

    public int GiveToBase()
    {
        if (_pickingObject == null)
            return 0;

        _pickingObject.GiveToBase();
        _unit.InvokeFree();

        return _pickingObject.Denomination;
    }
}
