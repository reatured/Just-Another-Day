using UnityEngine;
using UnityEngine.Events;

public class DragObjectScript : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    private PinOnVertex_L2_V3 pinBehavior;

    private void Start()
    {
        pinBehavior = GetComponent<PinOnVertex_L2_V3>();

    }
    void OnMouseDown()
    {
        Cursor.visible = false;

    }

    public UnityEvent dragEvent;

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 newPos = impactPoint;
        //newPos.y = 0;
        transform.position = newPos;
        if (dragEvent != null)
        {
            dragEvent.Invoke();
        }

    }

    private void OnMouseUp()
    {
        Cursor.visible = true;
    }

    //===============Helper Script=======================
    public Collider movementSurface;
    Ray ray;
    Vector3 impactPoint;
    public bool getImpactPoint(Collider ms)//movement Surface
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (ms.Raycast(ray, out hit, 100))
        {
            impactPoint = hit.point;
        }
        else
        {
            return false;
        }

        //print(impactPoint);
        return true;
    }
}
