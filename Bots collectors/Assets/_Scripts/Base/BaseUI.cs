using UnityEngine;
using TMPro;

public class BaseUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _crystalText;
    [SerializeField] private Database _dataBase;

    private void Start()
    {
        _crystalText.text = "";
    }

    private void OnEnable()
    {
        _dataBase.ChangeCrystals += ShowCountCrystals;
    }

    private void OnDisable()
    {
        _dataBase.ChangeCrystals -= ShowCountCrystals;
    }

    private void ShowCountCrystals(int count)
    {
        _crystalText.text = "Crystals: " + count.ToString();
    }
}
