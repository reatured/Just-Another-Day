using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BearDraggingBehavior_L2 : MonoBehaviour
{
    public Transform restTransform;
    public Transform pickUpTransform;

    public Transform movingObjTrans;
    bool pickedUp = false;
    public bool PickedUp
    {
        get { return pickedUp; }
        set { pickedUp = value;
            if (coroutine != null)
            {
                StopCoroutine(coroutine);

            }

            animationDuration = animationDurationMax * journey;
            animationDuration = Mathf.Min(animationDuration, animationDurationMax);

            startTime = Time.time;
            journey = 0;
            if (pickedUp)
            {
                coroutine = lerpTransform(movingObjTrans, pickUpTransform, movingObjTrans);
            }
            else
            {
                coroutine = lerpTransform(movingObjTrans, restTransform, movingObjTrans);
            }


            StartCoroutine(coroutine);

        }
    }

    IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        lerpFinishEvent.AddListener(enableNeedleDrag);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        PickedUp = !PickedUp;
    }

    public void pickUpAndPutDown()
    {
        PickedUp = !pickedUp;
    }

    public NeedleDraggingBehavior_L2_V3 needleDragScript;
    public void enableNeedleDrag()
    {
        needleDragScript.CanDrag = true; 
    }


    //===============Helper Script=======================
    float startTime = 0;
    public float animationDurationMax;
    float animationDuration;
    public UnityEvent lerpFinishEvent;
    float journey = 1;
    IEnumerator lerpTransform(Transform start, Transform end, Transform movingTrans)
    {

        journey = (Time.time - startTime) / animationDuration;


        //print("running" + Vector3.Lerp(start.position, end.position, journey) + "\nJourney" + journey);


        while (journey < 1.1f)
        {
            movingTrans.position = Vector3.Lerp(start.position, end.position, journey);

            movingTrans.rotation = Quaternion.Slerp(start.rotation, end.rotation, journey);
            yield return new WaitForFixedUpdate();
            journey = (Time.time - startTime) / animationDuration;
        }

        print(movingTrans.position);
        movingTrans.position = end.position;
        movingTrans.rotation = end.rotation; 

        if (lerpFinishEvent != null && PickedUp == true)
        {
            lerpFinishEvent.Invoke();

        }
    }

}
