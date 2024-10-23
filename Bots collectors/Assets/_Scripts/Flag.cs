using UnityEngine;
using UnityEngine.Events;

public class Flag : MonoBehaviour 
{
    public event UnityAction<Flag> Collision;
    public event UnityAction<Unit, Transform> HasUnit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Unit unit))
        {
            if (unit.IsMoveToFlag)
            {
                HasUnit?.Invoke(unit, transform);
                unit.ChangeFlagMoveToFlag(false);
                Collision?.Invoke(this);
            }
        }
    }
}
