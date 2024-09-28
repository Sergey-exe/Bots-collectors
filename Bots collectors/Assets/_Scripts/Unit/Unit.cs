using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Events;
public class Unit : MonoBehaviour 
{
    [SerializeField] private bool _isWork;
    [SerializeField] private UnitMover _mover;

    public bool MoveToFlag { get; private set; }

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

    public void ChangeFlagMoveToFlag(bool moveToFlag)
    {
        MoveToFlag = moveToFlag;
    }
}