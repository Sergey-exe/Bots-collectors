using UnityEngine;

public class CrystalSpawner : Spawner<PickingObject> 
{
    private void OnEnable()
    {
        Spawned += SubscribeToSpawn;
    }

    private void OnDisable()
    {
        Spawned -= SubscribeToSpawn;
    }

    private void SubscribeToSpawn(PickingObject pickingObject)
    {
        pickingObject.TransferredToBase += DestroyObject;
        pickingObject.TransferredToBot += DestroyCrystal;
    }

    private void DestroyObject(GameObject pickingObject)
    {
        Destroy(pickingObject);
    }

    private void DestroyCrystal(Crystal crystal)
    {
        Destroy(crystal);
    }
}
