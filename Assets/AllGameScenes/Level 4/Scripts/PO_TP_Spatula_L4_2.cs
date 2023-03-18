using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PO_TP_Spatula_L4_2 : PO_ToolProperty_L4_2
{

    public Transform potCenterTrans;
    Vector3 potCenter;
    public float originalAngle = 15f;
    bool updatePosition = true;

    public override void toolUpdate()
    {
        //potCenter = potCenterTrans.position;
        if(updatePosition)
        base.toolUpdate();
        //Vector3 camToTarget = potCenter - Camera.main.transform.position;
        //float currentAngle = Vector3.Angle(camToTarget, transform.position - Camera.main.transform.position);
        //float anglePercentile = 1 - currentAngle / originalAngle;
        //print(anglePercentile);

        //Vector3 newPosition = getImpactPoint();
        //float distanceToCenter = Vector3.Distance(newPosition, potCenter);

        //transform.position = newPosition;
        //transform.rotation = Quaternion.Lerp(restTransform.rotation, pickUpTransform.rotation, anglePercentile * 1.2f);

    }

    public override void clickBehavior()
    {
        base.clickBehavior();
        potCenter = potCenterTrans.position;
        float distance = Vector3.Distance(potCenter, transform.position);
        if (distance < 0.3f)
        {
            if (updatePosition == false) return; 
            updatePosition = false;
            initiateRotation();
            
        }

    }
    Vector3 startPosition;
    Quaternion startRotation; 
    void initiateRotation()
    {
        startTime = Time.time;
        startPosition = transform.position;
        startRotation = transform.rotation;
        startRotation.SetLookRotation(potCenter - startPosition);

        

        StartCoroutine(stir());
    }
    float startTime;
    public float tiltAngle; 
    IEnumerator stir()
    {

        transform.RotateAround(potCenter, Vector3.up, 360 * Time.deltaTime);
        print(transform.position);
        Vector3 currentPosition = transform.position; 
        Quaternion currentRotation = new Quaternion();
        currentRotation.SetLookRotation(potCenter - currentPosition);

        transform.rotation = Quaternion.Lerp(currentRotation, pickUpTransform.rotation, tiltAngle);
        

        yield return new WaitForFixedUpdate();
        if(Time.time - startTime < 1f)
        {
            StartCoroutine(stir());
        }
        else
        {
            updatePosition = true; 
        }
    }
}
