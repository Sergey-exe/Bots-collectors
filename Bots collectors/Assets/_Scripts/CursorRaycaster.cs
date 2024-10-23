using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorRaycaster : MonoBehaviour
{
    public Vector3 HitPosition {  get; private set; }

    private void LateUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            HitPosition = hit.point;
        }
    }
}
