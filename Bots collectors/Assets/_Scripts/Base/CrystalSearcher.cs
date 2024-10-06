using System.Collections.Generic;
using UnityEngine;

public class CrystalSearcher : MonoBehaviour
{
    [SerializeField] private float _searchRadius;

    public List<PickingObject> Search()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _searchRadius);

        List<PickingObject> crystals = new List<PickingObject>();

        foreach (Collider hit in hits)
        {
            PickingObject crystal;

            if(crystal = hit.GetComponent<PickingObject>())
                if(crystal.IsFree)
                    crystals.Add(crystal);
        }

        return crystals;
    }
}
