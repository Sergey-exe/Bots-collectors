using UnityEngine;
using UnityEngine.Events;

public class PickingObject : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    public event UnityAction<GameObject> TransferredToBase;
    public event UnityAction<Crystal> Discovered;

    [field: SerializeField] public int Denomination { get; private set; }

    public void PickUp(Transform parent, float distance)
    {
        transform.SetParent(parent);
        transform.localPosition = new Vector3(0f, 0f, distance);

        _rigidbody.isKinematic = true;
    }

    public void GiveToBase()
    {
        TransferredToBase?.Invoke(gameObject);
    }

    public void DestroyCrystal()
    {
        Discovered?.Invoke(GetComponent<Crystal>());
    }
}
