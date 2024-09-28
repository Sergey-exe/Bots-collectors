using UnityEngine;
using UnityEngine.Events;

public class FlagSetter : MonoBehaviour
{
    [SerializeField] private bool _flagInstalling;
    [SerializeField] private Flag _flagPrefab;
    [SerializeField] private CursorRaycaster _cursorRaycaster;

    public event UnityAction<Transform> IsSetFlag;

    private void Update()
    {
        if (_flagInstalling & _cursorRaycaster != null)
        {
            if (Input.GetKeyUp(KeyCode.Mouse1)) 
            {
                Flag flag = Instantiate(_flagPrefab, _cursorRaycaster.hitPosition, _flagPrefab.transform.rotation);
                IsSetFlag?.Invoke(flag.transform);
                _flagInstalling = false;
                flag.GetComponent<BaseCreator>().IsCreate += DestroyFlag;
            }
        }
    }

    public void InitRaycaster(CursorRaycaster cursorRaycaster)
    {
        _cursorRaycaster = cursorRaycaster;
    }

    public void SetFlag()
    {
        _flagInstalling = true;
    }

    public void DestroyFlag(BaseCreator flag)
    {
        flag.IsCreate -= DestroyFlag;
        Destroy(flag.gameObject);
    }
}