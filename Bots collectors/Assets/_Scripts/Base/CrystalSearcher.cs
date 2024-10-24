using System.Collections.Generic;
using UnityEngine;

public class CrystalSearcher : MonoBehaviour
{
    [SerializeField] private float _searchRadius;
    //[SerializeField] private List<Transform> _pickingObjects;

    //[ContextMenu(nameof(Start))]
    ////private void Start()
    ////{
    ////    _pickingObjects = Search();
    ////}

    //private void Update()
    //{
    //    if (_pickingObjects.Count == 0)
    //        _pickingObjects = Search();
    //}

    private List<Transform> Search()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _searchRadius);

        List<Transform> crystals = new List<Transform>();

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Crystal crystal))
            {
                crystals.Add(crystal.transform);
            }
        }

        return crystals;
    }

    public List<Transform> GetCrustals()
    {
        return Search();
    }
}