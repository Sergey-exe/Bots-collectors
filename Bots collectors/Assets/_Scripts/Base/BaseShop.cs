using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class BaseShop : MonoBehaviour
{
    [SerializeField] private int _basePrice;
    [SerializeField] private int _unitPrice;
    [SerializeField] private Button _spawnUnitButton;
    [SerializeField] private Button _spawnBaseButton;
    [SerializeField] private DataBase _dataBase;
    [SerializeField] private UnitSpawner _spawner;
    [SerializeField] private FlagSetter _flagSetter;

    private void OnEnable()
    {
        _spawnUnitButton.onClick.AddListener(PurchaseUnit);
        _spawnBaseButton.onClick.AddListener(PurchaseBase);
    }

    private void OnDisable()
    {
        _spawnUnitButton.onClick.RemoveListener(PurchaseUnit);
        _spawnBaseButton.onClick.RemoveListener(PurchaseBase);
    }

    private void PurchaseUnit()
    {
        int countSpawnUnits = 1;

        if(_dataBase.CountCrystals >= _unitPrice)
        {
            _dataBase.RemoveCrystals(_unitPrice);
            _spawner.ArrangeSpawnObjects(countSpawnUnits);
        }
    }

    private void PurchaseBase()
    {
        if (_dataBase.CountCrystals >= _basePrice)
        {
            _dataBase.RemoveCrystals(_basePrice);
            _flagSetter.SetFlag();
        }
    }
}
