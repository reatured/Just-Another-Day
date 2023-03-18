using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodGredientPicker_L4 : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 origialPos;

    private Vector3 targetPos;
    private Vector3 camToTarget;
    private float originalAngle;
    float dist_offset;

    private bool inThePot = false; 
    private void Start()
    {
        targetPos = GameObject.Find("PotCenter").transform.position;


    }

    void OnMouseDown()
    {
        
        origialPos = transform.position;

        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        camToTarget = targetPos - Camera.main.transform.position;
        originalAngle = Vector3.Angle(camToTarget, transform.position - Camera.main.transform.position);

        float distance = screenPoint.z;

        float dist_potToCam = (targetPos - Camera.main.transform.position).magnitude;
        dist_offset = distance - dist_potToCam;


    }

    void OnMouseDrag()
    {
        camToTarget = targetPos - Camera.main.transform.position;
        float currentAngle = Vector3.Angle(camToTarget, transform.position - Camera.main.transform.position);
        float anglePercentile = 1 - currentAngle / originalAngle;

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z - dist_offset * anglePercentile);
        transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
    }

    private void OnMouseUp()
    {
        camToTarget = targetPos - Camera.main.transform.position;
        float currentAngle = Vector3.Angle(camToTarget, transform.position - Camera.main.transform.position);
        float anglePercentile = 1 - currentAngle / originalAngle;

        if(anglePercentile < 0.8f && !inThePot)
        {
            transform.position = origialPos;
        }
        else
        {
            Rigidbody rb = this.AddComponent<Rigidbody>();
            inThePot = true;
            this.gameObject.layer = 2;
        }
        
    }
}
