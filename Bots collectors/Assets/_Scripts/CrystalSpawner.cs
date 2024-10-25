public class CrystalSpawner : Spawner<PickingObject> 
{
    public override PickingObject SpawnObject()
    {
        PickingObject pickingObject = base.SpawnObject();
        pickingObject.TransferredToBase += DestroyObject;

        return pickingObject;
    }
}
