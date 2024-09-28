using UnityEngine;
using UnityEngine.Events;

public class DataBase : MonoBehaviour
{
    public int CountCrystals { get; private set; }

    public event UnityAction<int> ChangeCrystals;

    public void AddCrystals(int count)
    {
        CountCrystals += count;
        ChangeCrystals?.Invoke(CountCrystals);
    }

    public void RemoveCrystals(int count)
    {
        CountCrystals -= count;
        ChangeCrystals?.Invoke(CountCrystals);
    }
}
