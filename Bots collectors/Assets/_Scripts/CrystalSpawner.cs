using UnityEngine;

public class CrystalSpawner : Spawner<PickingObject> 
{
    private void OnEnable()
    {
        OnSpawn += SubscribeToSpawn;
    }

    private void OnDisable()
    {
        OnSpawn -= SubscribeToSpawn;
    }

    private void SubscribeToSpawn(PickingObject pickingObject)
    {
        pickingObject.TransferredToBase += DestroyObject;
    }

    private void DestroyObject(GameObject pickingObject)
    {
        Destroy(pickingObject);
    }
}
