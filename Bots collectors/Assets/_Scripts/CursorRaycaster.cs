using UnityEngine;

public class CursorRaycaster : MonoBehaviour
{
    public Vector3 GetCursorPosition()
    {
        Vector3 hitPosition = new Vector3();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hitPosition = hit.point;
        }

        return hitPosition;
    }
}
