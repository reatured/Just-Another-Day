using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BearDraggingBehavior_L2 : MonoBehaviour
{
    public Transform restTransform;
    public Transform pickUpTransform;

    public Transform movingObjTrans;
    bool pickedUp = false;
    IEnumerator coroutine;

    public LevelManager level2Manager;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (coroutine != null)
        {
            StopAllCoroutines();
            print("StopCoroutine");

        }
        animationDuration = animationDurationMax * journey;
        animationDuration = Mathf.Min(animationDuration, animationDurationMax);

        startTime = Time.time;
        journey = 0;
        if (!pickedUp)
        {

            coroutine = lerpRotation(movingObjTrans.rotation, pickUpTransform.rotation, movingObjTrans);
            StartCoroutine(coroutine);
            coroutine = lerpPosition(movingObjTrans.position, pickUpTransform.position, movingObjTrans);
            pickedUp = true;
        }
        else
        {
            
            coroutine = lerpRotation(movingObjTrans.rotation, restTransform.rotation, movingObjTrans);
            StartCoroutine(coroutine);
            coroutine = lerpPosition(movingObjTrans.position, restTransform.position, movingObjTrans);
            pickedUp = false;
        }

        StartCoroutine(coroutine);  
    }



    //===============Helper Script=======================
    float startTime = 0;
    public float animationDurationMax;
    float animationDuration;
    public UnityEvent lerpFinishEvent;
    float journey = 1;
    IEnumerator lerpPosition(Vector3 start, Vector3 end, Transform movingTrans)
    {

        journey = (Time.time - startTime) / animationDuration;


        //print("running" + Vector3.Lerp(start.position, end.position, journey) + "\nJourney" + journey);


        while (journey < 1.1f)
        {
            movingTrans.position = Vector3.Lerp(start, end, journey);
            yield return new WaitForFixedUpdate();
            journey = (Time.time - startTime) / animationDuration;
        }

        print(movingTrans.position);
        movingTrans.position = end;

        if (lerpFinishEvent != null)
        {
            lerpFinishEvent.Invoke();

        }
    }

    IEnumerator lerpRotation(Quaternion start, Quaternion end, Transform movingTrans)
    {
        journey = (Time.time - startTime) / animationDuration;


        //print("running" + Vector3.Lerp(start.position, end.position, journey) + "\nJourney" + journey);
        movingTrans.rotation = Quaternion.Slerp(start, end, journey);

        yield return new WaitForFixedUpdate();

        journey = (Time.time - startTime) / animationDuration;
        if (journey < 1)
        {
            StartCoroutine(lerpRotation(start, end, movingTrans));
        }
        else
        {
            print(movingTrans.position);
            movingTrans.rotation = end;
        }
    }

}
