using UnityEngine;

public class TaskReceiver : MonoBehaviour
{
    [SerializeField] private DataBase _dataBase;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PickerObject pickerObject))
        {
            int countCrystals = pickerObject.GiveToBase();
            _dataBase.AddCrystals(countCrystals);
        }
    }
}
