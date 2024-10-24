using UnityEngine;
using UnityEngine.Events;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _factor = 2f;
    private Transform _target = null;

    public event UnityAction Finished;

    private void Update()
    {
        Move();
    }

    public void SetTackTransform(Transform target)
    {
        if(target == null)
            return;

        _target = target;
    }

    private void Move()
    {
        if (_target)
        {
            Vector3 offset = new Vector3(_target.position.x, transform.position.y, _target.position.z);
            transform.position = Vector3.MoveTowards(transform.position, offset, _speed * Time.deltaTime);
            transform.LookAt(_factor * transform.position - offset);

            Vector3 targetPosition = new Vector3(_target.position.x, 0, _target.position.z);
            Vector3 unitPosition = new Vector3(transform.position.x, 0, transform.position.z);

            if (targetPosition == unitPosition)
                Finished?.Invoke();
        }
    }
}
