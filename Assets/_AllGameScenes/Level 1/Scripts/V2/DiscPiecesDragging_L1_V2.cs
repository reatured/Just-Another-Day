using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DiscPiecesDragging_L1_V2 : MonoBehaviour
{
    protected Ray ray;
    Vector3 offset;
    Plane movementPlane;
    public Vector3 planeNormal;

    private void OnMouseDown()
    {
        ray = getRay(Camera.main.transform, transform);
        offset = transform.position - getImpactPoint();

    }

    private void OnMouseDrag()
    {
        transform.position = getImpactPoint() /*+ offset*/;

    }
    private void OnMouseUp()
    {
        
    }


    //---Helper Scripts
    public Ray getRay(Transform obj1, Transform obj2)
    {
        return new Ray(obj1.position, obj2.position - obj1.position);
    }

    public Vector3 impactPoint;
    public virtual Vector3 getImpactPoint()
    {
        print(impactPoint);
        float enter = 0f;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (movementPlane.Raycast(ray, out enter))
        {
            impactPoint = ray.GetPoint(enter);

        }

        return impactPoint;
    }

}
