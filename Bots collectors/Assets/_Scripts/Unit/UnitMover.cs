using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _factor = 2f;
    private Transform _target = null;

    private void Update()
    {
        Move();
    }

    public void SetTackTransform(Transform target)
    {
        _target = target;
    }

    private void Move()
    {
        if (_target)
        {
            Vector3 offset = new Vector3(_target.position.x, transform.position.y, _target.position.z);
            transform.position = Vector3.MoveTowards(transform.position, offset, _speed * Time.deltaTime);
            transform.LookAt(_factor * transform.position - offset);
        }
    }
}
