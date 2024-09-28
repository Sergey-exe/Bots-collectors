using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorRaycaster : MonoBehaviour
{
    public Vector3 hitPosition {  get; private set; }

    private void LateUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            hitPosition = hit.point;
        }
    }
}
