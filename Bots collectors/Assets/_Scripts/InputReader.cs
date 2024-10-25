using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private KeyCode[] _keysSetFlag;

    private bool DownButton(KeyCode[] keyCodes)
    {
        foreach (KeyCode keyCode in keyCodes)
            if (Input.GetKeyDown(keyCode))
                return true;

        return false;
    }

    public bool DownButtonSetFlag()
    {
        return DownButton(_keysSetFlag);
    }
}
