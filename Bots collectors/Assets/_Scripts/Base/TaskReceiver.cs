using UnityEngine;

public class TaskReceiver : MonoBehaviour
{
    [SerializeField] private DataBase _dataBase;

    private void OnTriggerEnter(Collider other)
    {
        PickerObject pickerObject;

        if(pickerObject = other.GetComponent<PickerObject>())
        {
            int countCrystals = pickerObject.GiveToBase();
            _dataBase.AddCrystals(countCrystals);
        }
    }
}
