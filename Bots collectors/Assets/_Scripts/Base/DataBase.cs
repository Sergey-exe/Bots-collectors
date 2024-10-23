using UnityEngine;
using UnityEngine.Events;

public class Database : MonoBehaviour
{
    public event UnityAction<int> ChangeCrystals;

    public int CountCrystals { get; private set; }

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
