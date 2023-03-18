using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ToolPickUp_L4 : MonoBehaviour 
{

    //For Spatula's movement only

    public Transform pickedUpTransform, restTransform;
    public Transform targetTransform;

    private Vector3 targetPos;

    public float thesholdSize;
    private Vector3 camToTarget;
    private float originalAngle;
    float dist_offset;

    public Plane spatulaMovementPlane;
    // Start is called before the first frame update
    void Start()
    {
        
        copyTransform(transform, restTransform);
        if(targetTransform == null)
        {
            targetPos = GameObject.Find("PotCenter").transform.position;
        }
        else
        {
            targetPos = targetTransform.position;   
        }
        spatulaMovementPlane = new Plane(Vector3.up, targetPos);
    }

    private Vector3 screenPoint;
    private Vector3 offset;



    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));


        Cursor.visible = false;

        camToTarget = targetPos - Camera.main.transform.position;
        originalAngle = Vector3.Angle(camToTarget, transform.position - Camera.main.transform.position);

        float distance = screenPoint.z;

        float dist_potToCam = (targetPos - Camera.main.transform.position).magnitude;
        dist_offset = distance - dist_potToCam;


    }
    Ray ray;
    RaycastHit hit;
    public Vector3 impactPoint;
    public Vector3 getImpactPoint()
    {
        float enter = 0f;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (spatulaMovementPlane.Raycast(ray, out enter))
        {
            impactPoint = ray.GetPoint(enter);
        }

        return impactPoint;
    }

    public float potSize = 2;
    void OnMouseDrag()
    {
        camToTarget = targetPos - Camera.main.transform.position;
        float currentAngle = Vector3.Angle(camToTarget, transform.position - Camera.main.transform.position);
        float anglePercentile = 1 - currentAngle / originalAngle;
        print(anglePercentile);



        Vector3 newPosition = getImpactPoint();
        float distanceToCenter = Vector3.Distance(newPosition, targetPos);
        //anglePercentile = 


        transform.position = newPosition;
        transform.rotation = Quaternion.Lerp(restTransform.rotation, pickedUpTransform.rotation, anglePercentile * 1.2f);
    }

    private void OnMouseUp()
    {
        copyTransform(restTransform, transform);
        Cursor.visible = true;
    }

    void copyTransform(Transform from, Transform to)
    {
        to.position = from.position;
        to.rotation = from.rotation;
        to.localScale = from.localScale;
    }

}
