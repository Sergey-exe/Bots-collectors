using UnityEngine;
using UnityEngine.Events;

public class FlagSetter : MonoBehaviour
{
    [SerializeField] private bool _flagInstalling;
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private CursorRaycaster _cursorRaycaster;
    [SerializeField] private InputReader _inputReader;

    public event UnityAction<Transform> IsSetFlag;

    private void Update()
    {
        if (_flagInstalling & _cursorRaycaster != null)
        {
            if (_inputReader.DownButtonSetFlag()) 
            {
                Flag flag = Instantiate(_flagPrefab, _cursorRaycaster.GetCursorPosition(), _flagPrefab.transform.rotation);
                IsSetFlag?.Invoke(flag.transform);
                _flagInstalling = false;
            }
        }
    }

    public void Init(CursorRaycaster cursorRaycaster, InputReader inputReader)
    {
        _cursorRaycaster = cursorRaycaster;
        _inputReader = inputReader;
    }

    public CursorRaycaster GetRaycaster() 
    { 
        return _cursorRaycaster; 
    }

    public InputReader GetInputReader()
    {
        return _inputReader;
    }

    public void SetFlag()
    {
        _flagInstalling = true;
    }

    public void DestroyFlag(Flag flag)
    {
        Destroy(flag.gameObject);
    }
}