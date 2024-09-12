using UnityEngine;
using UnityEngine.Events;

public class PickingObject : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    [field: SerializeField] public int Denomination { get; private set; }

    [field: SerializeField] public bool IsFree { get; private set; }

    public event UnityAction<GameObject> TransferredToBase;

    public void PickUp(Transform parent, float distance)
    {
        transform.SetParent(parent);
        transform.localPosition = new Vector3(0f, 0f, distance);

        _rigidbody.isKinematic = true;
        IsFree = false;
    }

    public void GiveToBase()
    {
        TransferredToBase?.Invoke(gameObject);
    }
}
