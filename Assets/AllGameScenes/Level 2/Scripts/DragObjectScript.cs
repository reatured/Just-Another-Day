using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjectScript : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    public event Action onNeedleDragged; 
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 newPos = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        //newPos.y = 0;
        transform.position = newPos;
        onNeedleDragged(); 
    }
}
