using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _factor = 2f;
    private Transform _target = null;
    private Coroutine _moveCoroutine;
    private Unit _unit;

    public event UnityAction<Unit, Transform> Finished;

    private void Start()
    {
        _unit = GetComponent<Unit>();
    }

    private IEnumerator Move()
    {
        Vector3 targetPosition = _target.position;
        Vector3 unitPosition = transform.position;

        while (targetPosition != unitPosition)
        {
            Vector3 offset = new Vector3(_target.position.x, transform.position.y, _target.position.z);
            transform.position = Vector3.MoveTowards(transform.position, offset, _speed * Time.deltaTime);
            transform.LookAt(_factor * transform.position - offset);

            targetPosition = new Vector3(_target.position.x, 0, _target.position.z);
            unitPosition = new Vector3(transform.position.x, 0, transform.position.z);

            yield return null;
        }

        Finished?.Invoke(_unit, transform);
    }

    public void SetTackTransform(Transform target)
    {
        if (target == null)
            return;

        _target = target;

        if(_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        _moveCoroutine = null;
        _moveCoroutine = StartCoroutine(Move());
    }
}
