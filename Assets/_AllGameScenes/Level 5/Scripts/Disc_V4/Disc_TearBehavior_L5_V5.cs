using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Disc_TearBehavior_L5_V5 : MonoBehaviour
{
    public float tearMagnitude; 

    public Transform directionTrans;
    public Vector3 moveDirection;

    public bool isTeard = false;


    private void Start()
    {
        directionTrans = transform.GetChild(0).transform; 
        moveDirection = directionTrans.position - this.transform.position;
        moveDirection = moveDirection.normalized;
        directionTrans.position = transform.position + moveDirection * tearMagnitude;
        
    }

    public void tear()
    {
        isTeard = true;
        tearMagnitude /= 10;
        moveDirection = directionTrans.position - this.transform.position;
        moveDirection = moveDirection.normalized;
        directionTrans.position = transform.position + moveDirection * tearMagnitude;
        startTime = Time.time;  
        StartCoroutine(lerpPosition(transform, directionTrans, transform));
    }

    float startTime = 0;
    public float animationDuration;
    public UnityEvent lerpEndEvent;
    public float journey = 0;
    IEnumerator lerpPosition(Transform start, Transform end, Transform movingTrans, float eventWaitTime = 0.3f)
    {
        print("start lerp");
        journey = (Time.time - startTime) / animationDuration;
        //bool activated = false;
        Vector3 startPos = start.position;
        Vector3 endPos = end.position;
        Quaternion startRotation = start.rotation;
        Quaternion endRotation = end.rotation;

        while (journey < 1.1f)
        {
            //print("running" + Vector3.Lerp(start, end, journey) + "\nJourney" + journey);
            movingTrans.position = Vector3.Lerp(startPos, endPos, journey);
            movingTrans.rotation = Quaternion.Slerp(startRotation, endRotation, journey);

            yield return new WaitForFixedUpdate();
            journey = (Time.time - startTime) / animationDuration;

        }
        if (lerpEndEvent != null)
        {
            lerpEndEvent.Invoke();
            lerpEndEvent.RemoveAllListeners();
        }

        movingTrans.position = endPos;
        movingTrans.rotation = endRotation;

    }

}
