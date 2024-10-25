using UnityEngine.UI;
using UnityEngine;

public class BaseShop : MonoBehaviour
{
    [SerializeField] private int _basePrice;
    [SerializeField] private int _unitPrice;
    [SerializeField] private Button _spawnUnitButton;
    [SerializeField] private Button _spawnBaseButton;
    [SerializeField] private Database _dataBase;
    [SerializeField] private UnitSpawner _spawner;
    [SerializeField] private FlagSetter _flagSetter;

    private bool _creatingNewBase = false;

    private void OnEnable()
    {
        _dataBase.ChangeCrystals += Purchase;
        _spawnBaseButton.onClick.AddListener(StartCreateNewBase);
    }

    private void OnDisable()
    {
        _dataBase.ChangeCrystals -= Purchase;
        _spawnBaseButton.onClick.RemoveListener(StartCreateNewBase);
    }

    private void Purchase(int countCrystals)
    {
        int countSpawnUnits = 1;

        if (_creatingNewBase)
        {
            if(countCrystals >= _basePrice)
            {
                PurchaseBase();
                _creatingNewBase = false;
            }
        }
        else if (countCrystals >= _unitPrice)
        {
            _dataBase.RemoveCrystals(_unitPrice);
            _spawner.ArrangeSpawnObjects(countSpawnUnits);
        }
    }

    private void StartCreateNewBase()
    {
        _creatingNewBase = true;
    }

    private void PurchaseBase()
    {
        _dataBase.RemoveCrystals(_basePrice);
        _flagSetter.SetFlag();
    }
}
