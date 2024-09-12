using UnityEngine;
using UnityEngine.Events;
public class Unit : MonoBehaviour 
{
    [SerializeField] private bool _isWork;
    [SerializeField] private UnitMover _mover;

    public event UnityAction<Unit> IsFree;

    [ContextMenu(nameof(Start))]
    private void Start()
    {
        InvokeFree();
    }

    public void SetTaskTransform(Transform transform)
    {
        if (transform == null)
            return;

        _mover.SetTackTransform(transform);
        _isWork = true;
    }

    public void InvokeFree()
    {
        _mover.SetTackTransform(null);
        _isWork = false;
        IsFree?.Invoke(this);
    }
}
