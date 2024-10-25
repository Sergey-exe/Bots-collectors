using UnityEngine;

public class Unit : MonoBehaviour 
{
    [SerializeField] private UnitMover _mover;

    public bool IsWork { get; private set; }

    public bool IsMoveToFlag { get; private set; }

    private void Start()
    {
        CancelTasks();
    }

    public void SetTaskTransform(Transform transform)
    {
        if (transform == null)
            return;

        _mover.SetTackTransform(transform);
        IsWork = true;
    }

    public void CancelTasks()
    {
        _mover.SetTackTransform(null);
        IsWork = false;
    }

    public void ChangeFlagMoveToFlag(bool moveToFlag)
    {
        IsMoveToFlag = moveToFlag;
    }
}